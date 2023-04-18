using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMS_Project.Converter
{
    public class IntConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                int intValue;

                if (int.TryParse((string)value, out intValue))
                {
                    return intValue;
                }
            }

            return null;
        }
    }
}
