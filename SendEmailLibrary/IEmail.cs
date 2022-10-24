using System.Net.Mail;

namespace SendEmailLibrary
{
    public interface IEmail
    {
        Task SendEmail(string sender, string recipient, string subject, string body, int attempts = 1);
    }
}