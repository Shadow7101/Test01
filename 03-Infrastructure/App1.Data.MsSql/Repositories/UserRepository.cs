using App1.Domain.Entities;
using App1.Domain.Repositories;
using App1.Domain.ViewModel;
using System;
using System.Linq;

namespace App1.Data.MsSql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private App1DbContext _context;

        private void Seed()
        {
            if (!_context.Role.Any())
            {
                string[] itens = new string[] { "Usuário", "Consultor de vendas", "Gerente de vendas", "Diretor", "Coordenador de vendas", "Analista de suporte", "Desenvolvedor" };
                byte id = 0;
                foreach (string item in itens)
                {
                    id += 10;
                    _context.Role.Add(new Role() { Id = id, Name = item });
                }
                _context.SaveChanges();
            }

            if (!_context.Gender.Any())
            {
                string[] itens = new string[] { "Não informar", "Masculino", "Feminino"};
                byte id = 0;
                foreach (string item in itens)
                {
                    id += 3;
                    _context.Gender.Add(new Gender() { Id = id, Name = item });
                }
                _context.SaveChanges();
            }
        }
        public UserRepository(App1DbContext context)
        {
            _context = context;
            Seed();
        }

        public void DesbloqueiaUsuario(Guid UserId, string ip)
        {
            User user = _context.User.Find(UserId);
            user.Bloqued = false;
            user.ModifyOn = DateTime.Now;
            user.ModifyBy = UserId;
            user.ModifyIp = ip;
            _context.SaveChanges();
        }

        public User FindByEmail(string email)
        {
            User user = _context.User.Where(x => x.Email == email).FirstOrDefault();

            if (user == null) return null;

            if (user.Role == null)
                user.Role = _context.Role.Find(user.RoleId);

            return user;
        }

        public Item[] Genders()
        {
            return _context.Gender.Select(x => new Item() { Id = x.Id.ToString(), Name = x.Name }).ToArray();
        }

        public User Insert(RegisterViewModel model)
        {
            Role role = _context.Role.OrderBy(x => x.Id).First();

            User user = new User();
            user.Email = model.Email;
            user.Name = model.Name;
            user.Password = model.Password;
            user.RoleId = role.Id;
            user.BirthDate = Convert.ToDateTime(model.BirthDate);
            user.GenderId = byte.Parse(model.Gender);
            user.CreatedOn = DateTime.Now;
            user.CreatedIp = model.Ip;
            user.CreatedBy = Guid.Empty;
            user.Avaliation = 0;
            user.EmailValidaded = false;

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void LastAccess(Guid UserId, string Ip)
        {
            User user = _context.User.Find(UserId);
            user.LastAccess = DateTime.Now;
            user.LastAccessIp = Ip;
            _context.SaveChanges();

        }

        public void ValidaEmail(Guid UserId, string ip)
        {
            User user = _context.User.Find(UserId);
            user.EmailValidaded = true;
            user.ModifyOn = DateTime.Now;
            user.ModifyBy = UserId;
            user.ModifyIp = ip;
            _context.SaveChanges();
        }
    }
}
