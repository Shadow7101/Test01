using App1.Domain.Entities;
using App1.Domain.Repositories;
using System;
using System.Linq;

namespace App1.Data.MsSql.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly App1DbContext _context;

        public TokenRepository(App1DbContext context)
        {
            this._context = context;
        }

        public void Delete(Guid id)
        {
            Token token = _context.Token.Find(id);
            _context.Token.Remove(token);
            _context.SaveChanges();
        }

        public Token Get(Guid id)
        {
            return _context.Token.Where(x => x.Id == id).FirstOrDefault();
        }

        public Guid InsertAccess(Guid UserId, string TokenValue, string Ip, int Validate)
        {
            return Insert(UserId, Ip, TokenValue, "Acesso permitido!", Validate);
        }

        public Guid InsertConfirmEmailToken(Guid UserId, string Ip, int Validate)
        {
            return Insert(UserId, Ip, null, "Confirmar Email", Validate);
        }
        private Guid Insert(Guid UserId, string Ip, string TokenValue, string typeName, int Validate)
        {
            TokenType type = GetTokenType(typeName);

            Token token = new Token()
            {
                CreatedBy = UserId,
                CreatedOn = DateTime.Now,
                CreatedIp = Ip,
                TypeId = type.Id,
                Validate = Validate
            };


            if (!string.IsNullOrEmpty(TokenValue))
                token.TokenData = TokenValue;

            _context.Token.Add(token);
            _context.SaveChanges();

            return token.Id;
        }
        private TokenType GetTokenType(string name)
        {
            //identificando tipo
            TokenType tipo = _context.TokenType.Where(x => x.Name == name).FirstOrDefault();
            if (tipo != null) return tipo;

            //gerando numeração
            byte id = 0;
            if (_context.TokenType.Any()) id = _context.TokenType.Max(x => x.Id);
            id++;

            //cadastrando um tipo ainda não cadastrado
            tipo = new TokenType() { Id = id, Name = name };
            _context.TokenType.Add(tipo);
            _context.SaveChanges();
            return tipo;
        }
    }
}
