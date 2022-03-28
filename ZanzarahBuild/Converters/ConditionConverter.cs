using System;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{
    public class ConditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Condition((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
