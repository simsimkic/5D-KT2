using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SIMS_Project.Validation
{
    public class LocationValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                List<string> stringValueList = stringValue.Split(",").ToList();

                if (stringValueList.Count == 2 && !string.IsNullOrWhiteSpace(stringValueList[0]) && !string.IsNullOrWhiteSpace(stringValueList[1]))
                {
                    return ValidationResult.ValidResult;
                }

                return new ValidationResult(false, "Format must be: 'city, country'");
            }

            return ValidationResult.ValidResult;
        }
    }
}
