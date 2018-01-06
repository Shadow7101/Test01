using App1.Domain.ViewModel;

namespace App1.Domain.Services
{
    public interface IAuthenticateService
    {
        ResultViewModel Authenticate(LoginFormVIewModel model);        
    }
}