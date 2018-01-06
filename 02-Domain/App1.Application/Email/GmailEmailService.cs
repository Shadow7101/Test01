using System;
using System.Net;
using System.Net.Mail;
using App1.Domain.Services;
using App1.Domain.ViewModel;
using Microsoft.Extensions.Configuration;

namespace App1.Application.Email
{
    internal class GmailEmailService : IEmailService
    {
        private readonly IConfigurationSection applicationUrl;
        private readonly SmtpConfiguration _config;


        public GmailEmailService(IConfiguration configuration)
        {
            this.applicationUrl = configuration.GetSection("emailConfiguration");
            _config = new SmtpConfiguration();
            var gmailUserName = "xxxxxx@xxxxx.com";
            var gmailPassword = "xxxxxxxxxxxxxxx";
            var gmailHost = "smtp.gmail.com";
            var gmailPort = 587;
            var gmailSsl = true;
            _config.Username = gmailUserName;
            _config.Password = gmailPassword;
            _config.Host = gmailHost;
            _config.Port = gmailPort;
            _config.Ssl = gmailSsl;

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