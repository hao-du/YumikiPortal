using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yumiki.Web.WellCovered.Validations
{
    public class IsRequiredBasedOnFieldAttribute : ValidationAttribute
    {
        public string Field { get; private set; }
        public object Value { get; private set; }

        public IsRequiredBasedOnFieldAttribute(string field, object value)
        {
            this.Field = field;
            this.Value = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object fieldInstance = validationContext.ObjectType.GetProperty(Field).GetValue(validationContext.ObjectInstance);

            if (fieldInstance.GetType() == Value.GetType() && fieldInstance.ToString() == Value.ToString())
            {
                if(value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;            
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The {0} field is required.", name);
        }
    }
}