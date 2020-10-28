using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI_Customer.ValidationRules
{
    public class ContactNumberValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string CustomerContact = value as string;
            if (string.IsNullOrEmpty(CustomerContact))
            {
                return new ValidationResult(false, $"Contact Number Requires Value");
            }
            else
            {
                return CheckValidity(CustomerContact);
            }

        }
        public bool checkDigits(string contactNumb)
        {
            return contactNumb.Length == 10 && contactNumb.All(char.IsDigit);
        }
        public ValidationResult CheckValidity(string contactNumber)
        {
            if (checkDigits(contactNumber))
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Contact Number must contain digits only and must be of 10 digits");
            }

        }
    }
}
