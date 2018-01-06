using System;
using App1.Domain.ViewModel;

namespace App1.Domain.Services
{
    public interface IRememberService
    {
        ResultViewModel Remember(RememberViewModel model);    

        ResultViewModel Remember(Guid id, string ip);

        ResultViewModel Remember(RememberPasswordViewModel model);
    }
}