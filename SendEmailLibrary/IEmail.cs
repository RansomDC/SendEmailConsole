using System.Net.Mail;

namespace SendEmailLibrary
{
    public interface IEmail
    {
        Task<bool> SendEmail(MailMessage message);
    }
}