using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{
    public class ElementImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte number = values[0] == DependencyProperty.UnsetValue ? (byte)0 : (byte)values[0];
            bool isPassive = values[1] == DependencyProperty.UnsetValue ? false : (bool)values[1];
            string path = isPassive ? @"pack://application:,,,/Resources/BitmapSources/CLS000T.png" : @"pack://application:,,,/Resources/BitmapSources/CLS001T.png";
            return new CroppedBitmap(new BitmapImage(new Uri(path)), Element.GetRect(number));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
