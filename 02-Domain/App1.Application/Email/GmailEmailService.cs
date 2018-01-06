using System;
using System.Net;
using System.Net.Mail;
using App1.Domain.Services;
using App1.Domain.ViewModel;
using Microsoft.Extensions.Configuration;

namespace App1.Application.Email
{
    public class GmailEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpConfiguration _config;

        public GmailEmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._config = new SmtpConfiguration
            {
                ApplicationUrl = configuration["SmtpConfiguration:ApplicationUrl"],
                Username = configuration["SmtpConfiguration:Username"],
                Password = configuration["SmtpConfiguration:Password"],
                Host = configuration["SmtpConfiguration:Host"],
                Port = int.Parse(configuration["SmtpConfiguration:Port"]),
                Ssl = bool.Parse(configuration["SmtpConfiguration:Ssl"])
            };
        }

        public void SendEmailMessage(MailMessage message)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = _config.Host,
                    Port = _config.Port,
                    EnableSsl = _config.Ssl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_config.Username, _config.Password)
                };

                using (var smtpMessage = new MailMessage())
                {
                    smtpMessage.From = new MailAddress(_config.Username);
                    foreach (var To in message.To)
                        smtpMessage.To.Add(To);
                    smtpMessage.Subject = message.Subject;
                    smtpMessage.Body = message.Body;
                    smtpMessage.IsBodyHtml = false;
                    smtp.Send(smtpMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}