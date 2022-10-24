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

        // This method can be called to use generate an email
        public async Task<bool> SendEmail(MailMessage message)
        {

            try
            {

                // Opens connection to SMTP client which will relay the message
                using (var smtpClient = new SmtpClient(_config.GetValue<string>("SMTPServer"), _config.GetValue<int>("SMTPPort")))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_config.GetValue<string>("User"), _config.GetValue<string>("Password"));
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    throw new Exception();

                    // Sends the compiled message asynchronously
                    await smtpClient.SendMailAsync(message);
                }

                return true;
            }
            catch (SmtpException)
            {
                // Usually results from a bad password being input
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
