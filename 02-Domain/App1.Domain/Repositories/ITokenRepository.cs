using System;
using App1.Domain.Entities;

namespace App1.Domain.Repositories
{
    public interface ITokenRepository
    {
        Guid InsertAccess(Guid UserId, string TokenValue, string Ip, int Validate);

        Guid InsertConfirmEmailToken(Guid UserId, string Ip, int Validate);

        Token Get(Guid id);

        void Delete(Guid id);
    }
}