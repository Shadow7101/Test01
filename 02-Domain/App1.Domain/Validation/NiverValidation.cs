using System;

namespace App1.Domain.Validation
{
    public sealed class NiverValidation: IValidators
    {
        private readonly object data;    
        private readonly string property;
        public NiverValidation(object _data, string _property)
        {
            data = _data;
            property = _property;
        }

        private DateTime Value
        {
            get
            {
                object value = data.GetType().GetProperty(property).GetValue(data, null);
                return Convert.ToDateTime(value);
            }
        }

        public int Age
        {
            get
            {
                DateTime Now = DateTime.Now;  
                int Years = new DateTime(DateTime.Now.Subtract(this.Value).Ticks).Year - 1;  
                DateTime PastYearDate = this.Value.AddYears(Years);  
                int Months = 0;  
                for (int i = 1; i <= 12; i++) {  
                    if (PastYearDate.AddMonths(i) == Now) {  
                        Months = i;  
                        break;  
                    } else if (PastYearDate.AddMonths(i) >= Now) {  
                        Months = i - 1;  
                        break;  
                    }  
                }  
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;  
                int Hours = Now.Subtract(PastYearDate).Hours;  
                int Minutes = Now.Subtract(PastYearDate).Minutes;  
                int Seconds = Now.Subtract(PastYearDate).Seconds;  
                //Console.WriteLine( String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",  Years, Months, Days, Hours, Seconds));
                return Years;
            }
        }

        public bool IsValid 
        { 
            get{ return (this.Age>=18 || this.Age<=25); }
        }
        public string ValidationError
        { 
            get
            {
                  if(this.Age<18) 
                    return "O usuário é jovem demais!";

                  if(this.Age>125) 
                    return "Data de nascimento inválida!";

                return null;
            }
        }
    }
}