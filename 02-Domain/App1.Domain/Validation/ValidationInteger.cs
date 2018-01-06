using System;

namespace App1.Domain.Validation
{
    public sealed class ValidationInteger : IValidators
    {
        private readonly object data;
        private readonly string property;

        public ValidationInteger(object _data, string _property)
        {
            data = _data;
            property = _property;
        }

        private int Value
        {
            get
            {
                object value = data.GetType().GetProperty(property).GetValue(data, null);
                return Convert.ToInt32(value);
            }
        }


        public bool IsValid 
        { 
            get
            {
                return this.Value > 0;
            }
        }

        public string ValidationError
        { 
            get
            {
                if(this.Value<=0)
                {
                    return $"O valor do campo {property} não é válido!";           
                }
                return null;
            }
        }
    }
}