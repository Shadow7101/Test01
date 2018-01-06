using App1.Domain.Entities;
using App1.Domain.Repositories;
using System;
using System.Linq;

namespace App1.Data.MsSql.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly App1DbContext _context;

        public LogRepository(App1DbContext context)
        {
            _context = context;
        }
        public void InsertBloquedUser(Guid UserId, string ip)
        {
            Insert(UserId, ip, null, "Usuario bloqueado tentando acessar o sistema");
        }

        public void InsertSuccessLogin(Guid UserId, string ip)
        {
            Insert(UserId, ip, null, "Executou o login com sucesso!");
        }

        public void InsertWrongPassword(Guid UserId, string ip)
        {
            Insert(UserId, ip, null, "Errou a senha de acesso!");
        }

        public void InsertWrongUser(string email, string ip)
        {
            Insert(Guid.Empty, ip, email, "Usuário não encontrado!");
        }

        public void ValidateEmailError(Guid UserId, string ip)
        {
            Insert(UserId, ip, null, "Validação de e-mail pendente!");
        }

        public void UserCreatedWithSuccess(Guid UserId, string ip)
        {
            Insert(UserId, ip, null, "Usuário criado com exito!");
        }
        public void UnexpectedError(Guid UserId, string ip, string message)
        {
            Insert(UserId, ip, message, "Ocorreu um erro inesperado!");
        }
        #region | Métodos privados
        private void Insert(Guid UserId, string ip, string Data, string typeName)
        {
            LogType tipo = LogType(typeName);
            Log log = new Log()
            {
                CreatedOn = DateTime.Now,
                CreatedIp = ip,
                LogTypeId = tipo.Id
            };

            if (!Guid.Empty.Equals(UserId))
                log.CreatedBy = UserId;
            if (!string.IsNullOrEmpty(Data))
                log.Data = Data;

            _context.Log.Add(log);
            _context.SaveChanges();
        }
        private LogType LogType(string name)
        {
            //identificando o tipo
            LogType tipo = _context.LogType.Where(x => x.Name == name).FirstOrDefault();
            if (tipo != null) return tipo;

            //gerando a numeracao
            byte id = 0;
            if (_context.LogType.Any()) id = _context.LogType.Max(x => x.Id);
            id++;

            //cadastrando um tipo ainda não cadastrado
            tipo = new LogType() { Id = id, Name = name };
            _context.LogType.Add(tipo);
            _context.SaveChanges();
            return tipo;
        }
        #endregion
    }
}
