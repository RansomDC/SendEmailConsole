using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SendEmailLibrary
{
    public class Email : IEmail
    {

        private readonly IConfiguration _config;

        // Constructor for the Email class
        public Email(IConfiguration config)
        {
            _config = config;
        }

        // This method can be called to generate and send an email asynchronously
        public async Task SendEmail(string sender, string recipient, string subject, string body, int attempts = 1)
        {
            //creates the MailMessage which will be sent via smtpClient.SendMailAsync()
            var message = new MailMessage();

            // These declare the context of the message
            message.From = new MailAddress(sender);
            message.To.Add(recipient);
            message.Subject = subject;
            message.Body = body;

            // The try/catch blocks here act as the method for re-sending the email if initial sending fails. If there is any exception in the try area the catch block adds to the number
            // of attempts that have been made and re-calls the the method recursively. If the method has been called three times it ceases its attempts and logs to the database a failed attempt
            try
            {
                // Opens connection to SMTP client which will relay the message
                using (var smtpClient = new SmtpClient(_config.GetValue<string>("SMTPServer"), _config.GetValue<int>("SMTPPort")))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_config.GetValue<string>("User"), _config.GetValue<string>("Password"));
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Sends the compiled message asynchronously
                    await smtpClient.SendMailAsync(message);
                    _ = writeToDB(sender, recipient, subject, body, true);
                }
            }
            catch (Exception)
            {
                if (attempts == 3)
                {
                    _ = writeToDB(sender, recipient, subject, body, false);
                }
                else
                {
                    _ = SendEmail(sender, recipient, subject, body, attempts + 1);
                }
 
            }

        }


        // A method to write the email data to the database asynchronously.
        static async Task writeToDB(string sender, string recipient, string subject, string body, bool sendSuccessful)
        {
            using (var context = new EmailContext())
            {
                // Creates an email entity which will be populated with the user data
                var em = new EmailEntity()
                {
                    senderAddress = sender,
                    recipeintAddress = recipient,
                    subject = subject,
                    sendSuccessful = sendSuccessful,
                    sendTime = DateTime.Now,
                    body = body
                };

                // Entity Framework adds em to the context (which connects to the database) and then saves the changes asynchronously.
                context.Emails.Add(em);
                await context.SaveChangesAsync();
            }

        }

    }

}
