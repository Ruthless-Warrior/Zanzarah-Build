using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ZanzarahBuild.Converters
{
    public class HexRepresentationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Common.BitConverter.HexBytes(Common.BitConverter.GetBytes((int)value));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bar = new List<byte>();
            for (int i = 0; i < 4; i++)
            {
                byte b = System.Convert.ToByte(value.ToString().Substring(i * 2, 2), 16);
                bar.Add(b);
            }
            return BitConverter.ToInt32(bar.ToArray(), 0);
        }
    }
}
