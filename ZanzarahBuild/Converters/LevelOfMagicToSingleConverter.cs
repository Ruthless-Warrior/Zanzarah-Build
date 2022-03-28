using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{
    public class LevelOfMagicToSingleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int number = System.Convert.ToInt32(parameter);
            LevelOfMagic LevelOfMagic = value as LevelOfMagic;
            switch(number)
            {
                case 1: return new SingleLevelOfMagic(LevelOfMagic.Element1, LevelOfMagic.IsPassive);
                case 2: return new SingleLevelOfMagic(LevelOfMagic.Element2, LevelOfMagic.IsPassive);
                case 3: return new SingleLevelOfMagic(LevelOfMagic.Element3, LevelOfMagic.IsPassive);
            }
            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
