using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMS_Project.Converter
{
    public partial class LocationConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                List<string> stringValueList = stringValue.Split(",").Select(p => p.Trim()).ToList();

                return new Location(0, stringValueList[0], stringValueList[1]);
            }

            return null;
        }
    }
}
