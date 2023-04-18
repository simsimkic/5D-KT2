using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Project.Validation
{
    public class PositiveIntValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                int intValue;

                if (int.TryParse(stringValue, out intValue) && intValue > 0)
                {
                    return ValidationResult.ValidResult;
                }

                return new ValidationResult(false, "Format must be a positive number'");
            }

            return ValidationResult.ValidResult;
        }
    }
}

