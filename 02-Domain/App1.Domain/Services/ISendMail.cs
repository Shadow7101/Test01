using System;

namespace App1.Domain.Services
{
    public interface ISendMail
    {
        void SendRegister(Guid id, string email, string name);

        void SendRemember(Guid id, string email, string name);
    }
}