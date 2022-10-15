using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailLibrary
{
    public static class Email
    {

        // This method can be called to use generate an email
        public static async Task SendEmail(string sender, string recipient, string subject, string body, int sendAttempt = 1)
        {

            try
            {

                var message = new MailMessage();

                // These declare the context of the message
                message.From = new MailAddress(sender);
                message.To.Add(recipient);
                message.Subject = subject;
                message.Body = body;

                // Opens connection to SMTP client which will relay the message
                using (var smtpClient = new SmtpClient("smtp-relay.sendinblue.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("david.r.cadorette@gmail.com", "C2FjdyzBcZJ1LUOY");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Sends the compiled message asynchronously
                    await smtpClient.SendMailAsync(message);
                }

            }
            catch (System.Net.Mail.SmtpException)
            {
                // Usually results from a bad password being input
                Console.WriteLine("The SMTP server requires a secure connection or the client was not authenticated.");
                Console.WriteLine("This could be due to the credentials being incorrect, check the password and login found in appsettings.json");
            }
            catch (System.FormatException)
            {
                //Either the sender address or the recipient address is formatted incorrectly
                Console.WriteLine($"Sender: {sender}");
                Console.WriteLine($"recipient: {recipient}");
                Console.WriteLine("One of the email addresses you entered is not formatted correctly.");
                Console.WriteLine("Be sure to use the format ' example@mail.com ' ");
            }
            catch (Exception ex)
            {
                // If send fails for any other reason. The application will re-attempt 3 times.

                int timesSent = sendAttempt + 1;
                if (timesSent <= 3)
                {
                    _ = Email.SendEmail(sender, recipient, subject, body, timesSent);
                    Console.WriteLine($"SendEmail attempt number {timesSent} failed.");
                }
                
                Console.WriteLine(ex);

            }

        }

    }
}
