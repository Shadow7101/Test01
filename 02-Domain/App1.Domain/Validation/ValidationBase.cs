using System.Collections.Generic;
using System.Text;

namespace App1.Domain.Validation
{
    public abstract class ValidationBase
    {
        public ValidationBase()
        {
            Validators = new List<IValidators>();
        }

        protected List<IValidators> Validators { get; set; }

        public bool IsValid
        {
            get
            {
                foreach (IValidators validator in Validators)
                    if (!validator.IsValid) return false;

                return true;
            }
        }

        public string Erros
        {
            get
            {
                StringBuilder build = new StringBuilder();

                foreach (IValidators validator in Validators)
                    if (!validator.IsValid) build.Append(validator.ValidationError + "\n");

                return build.ToString();
            }
        }
    }
}
