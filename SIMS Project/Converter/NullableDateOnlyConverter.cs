using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SIMS_Project.Converter
{
    public class NullableDateOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dateTime = (DateTime?)value;

            return dateTime.HasValue ? (DateOnly?)DateOnly.FromDateTime(dateTime.Value) : null;
        }
    }
}
