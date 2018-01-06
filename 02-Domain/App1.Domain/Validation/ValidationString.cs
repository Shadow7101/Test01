using App1.Domain.ViewModel;

namespace App1.Domain.Validation
{
    public sealed class ValidationString : IValidators
    {
        private object data;
        private string property;
        private bool canBeNull;
        private bool canBeEmpty;
        private int minSize;
        private int maxSize;

        public ValidationString(object Data, string Property, bool CanBeNull, bool CanBeEmpty, int MinSize, int MaxSize)
        {
            this.data = Data;
            this.property = Property;
            this.canBeNull = CanBeNull;
            this.canBeEmpty = CanBeEmpty;
            this.minSize = MinSize;
            this.maxSize = MaxSize;
        }

        public ValidationString(object Data, string Property)
        {
            this.data = Data;
            this.property = Property;
            this.canBeNull = false;
            this.canBeEmpty = false;
            this.minSize = 0;
            this.maxSize = int.MaxValue;
        }

        private string Value
        {
            get
            {
                object value = data.GetType().GetProperty(property).GetValue(data, null);
                return (string)value;
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Value) && (!canBeEmpty || !canBeNull)) return false;

                int size = Value.Length;

                if (size < minSize || size > maxSize) return false;

                return true;
            }
        }

        public string ValidationError
        {
            get
            {
                if (this.IsValid) return null;

                if (string.IsNullOrEmpty(Value) && (!canBeEmpty || !canBeNull)) return $"O campo \"{property}\" não pode ficar em branco";

                int size = Value.Length;

                if (size < minSize || size > maxSize) return $"O campo \"{property}\" deve ter no minimo {minSize} e no máximo {maxSize}.";

                return null;
            }
        }
    }
}
