using System;
using App1.Application.JWT;
using App1.Domain.Entities;
using App1.Domain.Repositories;
using App1.Domain.Services;
using App1.Domain.ViewModel;

namespace App1.Application.Users
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogRepository logRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly ICryptography cryptography;

        public AuthenticateService(IUserRepository uRepository, ILogRepository lrepository, ITokenRepository trepository, ICryptography crypto)
        {
            this.userRepository = uRepository;
            this.logRepository = lrepository;
            this.tokenRepository = trepository;
            this.cryptography = crypto;
        }

        public ResultViewModel Authenticate(LoginFormVIewModel model)
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

            //criptografando senha
            model.Password = cryptography.Encrypt(model.Password);

            //recuperando dados do usuário
            User user = userRepository.FindByEmail(model.Email);

            //checando se o usuário foi encontrado
            if (user == null)
            {
                //registra log
                logRepository.InsertWrongUser(model.Email, model.Ip);
                //retornado resultado para a camada superior    
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Nome de usuário ou senha incorretos!",
                    Data = 0
                };
            }

            //checando se a senha esta correta
            if (!model.Password.Equals(user.Password))
            {
                //registra log
                logRepository.InsertWrongPassword(user.Id, model.Ip);
                //retornado resultado para a camada superior    
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Nome de usuário ou senha incorretos!",
                    Data = 1
                };
            }

            //checando se o e-mail do usuário foi validado
            if (!user.EmailValidaded)
            {
                logRepository.ValidateEmailError(user.Id, model.Ip);
                //retornado resultado para a camada superior    
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Seu endereço de e-mail ainda não foi validado, cheque seu e-mail, encontre a mensagem que enviamos e clique no link!",
                    Data = 2
                };
            }

            //usuário bloqueado
            if (user.Deleted || user.Bloqued || user.Banned)
            {
                //registra log
                logRepository.InsertBloquedUser(user.Id, model.Ip);
                //retornado resultado para a camada superior    
                return new ResultViewModel()
                {
                    Success = false,
                    Message = "Há um problema com seu cadastro, envie um e-mail para suporte@pontogp.com.br solicitando a regularização do seu cadastro!",
                    Data = 3
                };
            }

            //criando token de autenticação
            string tokenValue = Jwt_GenerateToken(user.Email, user.Id, user.Role.Name);
            Guid TokenId = tokenRepository.InsertAccess(UserId: user.Id, TokenValue: tokenValue, Ip: model.Ip, Validate: 60);
            LoginResultViewModel loginResult = new LoginResultViewModel()
            {
                TokenId = TokenId,
                TokenValue = tokenValue,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.Name
            };

            //registrando o último acesso do usuario
            userRepository.LastAccess(user.Id, model.Ip);
            logRepository.InsertSuccessLogin(user.Id, model.Ip);

            return new ResultViewModel()
            {
                Success = true,
                Message = "Login com sucesso!",
                Data = loginResult
            };

        }
        private string Jwt_GenerateToken(string email, Guid userId, string role)
        {
            var token = new TokenJWTBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(secret: "Secrete_Key-Is-Other?123456789"))
                .AddSubject(email)
                .AddIssuer("App1.Valid.Issuer")
                .AddAudience("App1.Valid.Audience")
                .AddClaims("UsuarioApiNumero", userId.ToString())
                .AddClaims("UserRole", role)
                .AddExpiryInMinutes(30)
                .Builder();

            return token.value;
        }
    }
}