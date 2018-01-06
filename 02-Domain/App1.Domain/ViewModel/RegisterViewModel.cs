using App1.Domain.Validation;

namespace App1.Domain.ViewModel
{
    public class RegisterViewModel:ValidationBase
    {
        public RegisterViewModel()
        {
            this.Validators.Add(new ValidationEmail(this, "Email", false));
            this.Validators.Add(new ValidationString(this, "Name",false, false, 5, 120));
            this.Validators.Add(new ValidationString(this, "Password", false, false, 6, 30));
            this.Validators.Add(new ValidationInteger(this, "Gender"));
            this.Validators.Add(new ValidationString(this, "Ip"));
            this.Validators.Add(new NiverValidation(this, "BirthDate"));
        }
        public string Email {get;set;}
        public string Password {get;set;}
        public string Name {get;set;}
        public string BirthDate { get;set;}
        public string Gender {get;set;}
        public string Ip {get;set;}
    }
}