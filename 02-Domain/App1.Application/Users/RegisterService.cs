using System;
using App1.Domain.Entities;
using App1.Domain.Repositories;
using App1.Domain.Services;
using App1.Domain.ViewModel;

namespace App1.Application.Users
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly ISendMail sendMail;
        private readonly ICryptography cryptography;
        private readonly ILogRepository logRepository;

        public RegisterService(IUserRepository uRepository, ITokenRepository trepository, ISendMail mail, ICryptography Cryptography, ILogRepository LogRepository)
        {
            this.userRepository = uRepository;
            this.tokenRepository = trepository;
            this.sendMail = mail;
            this.cryptography = Cryptography;
            this.logRepository = LogRepository;
        }

        public Item[] Genders()
        {
            return userRepository.Genders();
        }

        ResultViewModel IRegisterService.ConfirmEmail(Guid id, string ip)
        {
            Token token = tokenRepository.Get(id);

            //checando se o token existe
            if (token == null)
            {
                return new ResultViewModel()
                {
                    Data = 1,
                    Message = "O token fornecido não é válido!",
                    Success = false
                };
            }

            //remove o token
            tokenRepository.Delete(id);

            //checando se o token é válido
            if (token.CreatedOn.AddMinutes(token.Validate) < DateTime.Now)
            {
                return new ResultViewModel()
                {
                    Data = 2,
                    Message = "O token fornecido não é válido!",
                    Success = false
                };
            }
            //desbloqueia usuario
            userRepository.DesbloqueiaUsuario(token.CreatedBy, ip);

            //valida e-mail do usuário
            userRepository.ValidaEmail(token.CreatedBy, ip);

            return new ResultViewModel()
            {
                Data = 2,
                Message = "Cadastro validado com exito!",
                Success = true
            };
        }

        ResultViewModel IRegisterService.Register(RegisterViewModel model)
        {
            //checando se o modelo é válido
            if (!model.IsValid)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = model.Erros,
                    Data = 0
                };
            }

            //checando se o usuário ja foi cadastrado
            User user = userRepository.FindByEmail(model.Email);
            if (user != null)
            {
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Usuário já cadastrado!",
                    Data = 1
                };
            }

            //criptografando senha
            model.Password = cryptography.Encrypt(model.Password);

            //registrando usuario 
            user = userRepository.Insert(model);
            logRepository.UserCreatedWithSuccess(user.Id, model.Ip);


            //criando token para confirmar e-mail
            Guid token = tokenRepository.InsertConfirmEmailToken(user.Id, model.Ip, 120);

            //enviando e-mail de confirmação de senha
            try
            {
                sendMail.SendRegister(token, user.Email, user.Name);
            }
            catch (Exception ex)
            {
                logRepository.UnexpectedError(user.Id, model.Ip, "Erro ao enviar e-mail:\n" + ex.Message);
            }

            //retornando informação para a camada de front-end
            return new ResultViewModel() { Data = true, Message = "Usuario criado com exito!", Success = true };
        }
    }
}