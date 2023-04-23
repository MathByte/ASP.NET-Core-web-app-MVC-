using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JellyFish.CustomValidation
{
    public class ExpiryDateAttribute:ValidationAttribute
    {
        public ExpiryDateAttribute()
        {
            ErrorMessage = "Expiry date cannot be in past!!";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                DateTime today = DateTime.Now;
                int year = int.Parse(today.ToString("yy"));
                int month = int.Parse(today.ToString("MM"));
                string ex = value.ToString();
                string expYear= ex.Substring(3);
                string expMonth = ex.Substring(0, 2);
                int expiryYear = int.Parse(expYear);
                int expiryMonth = int.Parse(expMonth);
                if (expiryYear < year)
                {
                    return new ValidationResult(string.Format(ErrorMessage,validationContext.DisplayName));
                }
                if (expiryYear == year && expiryMonth < month)
                {
                    return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
                }
            }       
                 return ValidationResult.Success;
        }
    }
}
