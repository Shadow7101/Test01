using System;

namespace App1.Domain.Repositories
{
    public interface ILogRepository
    {
        void InsertWrongUser(string email, string ip);
        void InsertWrongPassword(Guid UserId, string ip);
        void InsertBloquedUser(Guid UserId, string ip);
        void InsertSuccessLogin(Guid UserId, string ip);
        void ValidateEmailError(Guid UserId, string ip);
    }
}