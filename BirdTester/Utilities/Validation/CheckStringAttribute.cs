using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.Utilities.Validation
{
    public class CheckStringAttribute : ValidationAttribute
    {
        public string AllowableValue { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValue.Equals(value.ToString())) 
            {
                return ValidationResult.Success;
            }
            var msg = "Locatie valt buiten de gemeente 's-Hertogenbosch!";
            return new ValidationResult(msg);
        }
    }
}
