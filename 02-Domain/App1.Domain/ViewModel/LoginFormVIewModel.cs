using App1.Domain.Validation;

namespace App1.Domain.ViewModel
{
    public class LoginFormVIewModel:ValidationBase
    {
        public LoginFormVIewModel()
        {
            this.Validators.Add(new ValidationEmail(this, "Email", false));
            this.Validators.Add(new ValidationString(this, "Password", false, false, 6, 30));
            this.Validators.Add(new ValidationString(this, "Ip"));
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Ip {get;set;}
    }
}