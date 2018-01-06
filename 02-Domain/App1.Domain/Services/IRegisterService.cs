using System;
using App1.Domain.ViewModel;

namespace App1.Domain.Services
{
    public interface IRegisterService
    {
        ResultViewModel Register(RegisterViewModel model);

        ResultViewModel ConfirmEmail(Guid id, string ip);

        Item[] Genders();
    }
}