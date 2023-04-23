using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JellyFish.CustomValidation
{
    public class FutureDateValidation : ValidationAttribute
    {
        public FutureDateValidation()
        {
            ErrorMessage = "Date of birth cannot be in future!!";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                DateTime today = DateTime.Now;
                int year = int.Parse(today.ToString("yy"));
                int month = int.Parse(today.ToString("MM"));
                int date= int.Parse(today.ToString("dd"));
                string ex = value.ToString();
                string expYear= ex.Substring(6,2);
                string expMonth = ex.Substring(0, 2);
                string expDay = ex.Substring(3,2);

                int expiryYear = int.Parse(expYear);
                int expiryMonth = int.Parse(expMonth);
                int expiryDay = int.Parse(expDay);
                if (expiryYear > year)
                {
                    return new ValidationResult(string.Format(ErrorMessage,validationContext.DisplayName));
                }
                if ((expiryYear == year && expiryMonth > month) || ((expiryYear == year && expiryMonth == month || expiryDay > date)) )
                {
                    return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
                }
            }       
                 return ValidationResult.Success;
        }
    }
}
