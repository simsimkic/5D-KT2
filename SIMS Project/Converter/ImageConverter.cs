using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace SIMS_Project.Converter
{
    public class ImageConverter: MarkupExtension, IMultiValueConverter
    {
        public string BasePath { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string id = values[0] as string;
            string image = values[1] as string;

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(image))
            {
                string executablePath = System.AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = string.Format("{0}{1}/{2}", BasePath, id, image);
                Uri uri = new Uri(executablePath + relativePath);
                return new BitmapImage(uri);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
