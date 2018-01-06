using System;
using System.Net.Mail;
using App1.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace App1.Application.Email
{
    public class SendMail: ISendMail
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _service;
        private readonly string ApplicationUrl;

        public SendMail(IConfiguration configuration, IEmailService service)
        {
            this._configuration = configuration;
            this._service = service;
            this.ApplicationUrl = configuration["SmtpConfiguration:ApplicationUrl"];
        }

        public void SendRegister(Guid id, string email, string name)
        {
            string corpo = "Ola! " + name + "\n\n";
            corpo += "Bem vindo a nossa familia feliz:\n";
            corpo += this.ApplicationUrl + "confirma-cadastro/{0}";
            corpo += "\n\n";
            corpo += "Clique no link acima para confirmar seu cadastro.\n\n";
            corpo += "Suporte\n";
            corpo += "Sistema sem nome\n";

            corpo = string.Format(corpo, id);

            MailMessage message = new MailMessage()
            {
                Body = corpo,
                IsBodyHtml = false,
                Subject = "Confirmar cadastro"
            };
            message.To.Add(email);

            _service.SendEmailMessage(message);
        }
        public void SendRemember(Guid id, string email, string name)
        {
            string corpo = "Ola! " + name + "\n\n";
            corpo += "Pelo jeito voce esqueceu sua senha de acesso, então, utilize o link abaixo e crie uma nova senha pra você:\n";
            corpo += this.ApplicationUrl + "Home/RememberEmailConfirme?Id={0}";
            corpo += "\n\n";
            corpo += "Caso não tenha sido você que solicitou este e-mail, troque sua senha assim mesmo, pois isso é estranho.\n";
            corpo += "Caso perca o acesso a sua conta em nosso sistema, entre em contato conosco pelo email suporte@sistema.com.br.\n\n";
            corpo += "Suporte\n";
            corpo += "Sistema sem nome\n";

            corpo = string.Format(corpo, id);

            MailMessage message = new MailMessage()
            {
                Body = corpo,
                IsBodyHtml = false,
                Subject = "Lembrar senha de acesso"
            };
            message.To.Add(email);

            _service.SendEmailMessage(message);
        }
    }
}