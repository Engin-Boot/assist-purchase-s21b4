using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI_Customer.ValidationRules
{
    public class EmailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string CustomerEmailId = value as string;
            if(!Regex.IsMatch(CustomerEmailId, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") ||
                string.IsNullOrEmpty(CustomerEmailId))
            {
                return new ValidationResult(false, $"Enter Valid E-Mail address");
            }
            return new ValidationResult(true, "");
        }
    }
}
