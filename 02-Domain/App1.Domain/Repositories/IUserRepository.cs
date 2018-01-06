using System;
using App1.Domain.Entities;
using App1.Domain.ViewModel;

namespace App1.Domain.Repositories
{
    public interface IUserRepository
    {
        User FindByEmail(string email);
        void LastAccess(Guid UserId, string Ip);
        User Insert(RegisterViewModel model);
        void DesbloqueiaUsuario(Guid UserId, string ip);
        void ValidaEmail(Guid UserId, string ip);
        Item[] Genders();
    }
}