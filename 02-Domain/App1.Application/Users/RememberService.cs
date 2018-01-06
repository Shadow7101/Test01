using System;
using App1.Domain.Services;
using App1.Domain.ViewModel;

namespace App1.Application.Users
{
    public class RememberService: IRememberService
    {
        public ResultViewModel Remember(RememberViewModel model)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel Remember(Guid id, string ip)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel Remember(RememberPasswordViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}