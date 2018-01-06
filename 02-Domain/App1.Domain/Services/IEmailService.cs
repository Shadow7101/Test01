using System.Net.Mail;

namespace App1.Domain.Services
{
    public interface IEmailService
    {
         void SendEmailMessage(MailMessage message);
    }
}