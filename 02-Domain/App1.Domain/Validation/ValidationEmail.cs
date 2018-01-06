namespace App1.Domain.Validation
{
    public sealed class ValidationEmail : IValidators
    {
        private object data;
        private string property;
        private bool canBeNull;

        public ValidationEmail(object Data, string Property, bool CanBeNull)
        {
            data = Data;
            property = Property;
            canBeNull = CanBeNull;
        }

        private string Value
        {
            get
            {
                object value = data.GetType().GetProperty(property).GetValue(data, null);
                return (string)value;
            }
        }

        private bool EmailIsValid
        {
            get
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Value);
                    return addr.Address == Value;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Value) && !canBeNull) return false;

                return EmailIsValid;
            }
        }

        public string ValidationError
        {
            get
            {
                if (this.IsValid) return null;

                if (string.IsNullOrEmpty(Value) && !canBeNull) return $"O campo \"{property}\" não pode ficar em branco";

                if (!EmailIsValid) return "O formato do email fornecido não é válido!";

                return null;
            }
        }
    }
}
