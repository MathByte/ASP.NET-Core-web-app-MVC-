using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JellyFish.CustomValidation
{
    public class PastDateValidation : ValidationAttribute
    {
        public PastDateValidation()
        {
            ErrorMessage = "Event date cannot be in past!!";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string s = value.ToString();
                DateTime today = DateTime.Now;
                DateTime date = DateTime.Parse(s);
                if (date < today)
                {
                    return new ValidationResult(string.Format(ErrorMessage,validationContext.DisplayName));
                }
            }       
                 return ValidationResult.Success;
        }
    }
}
