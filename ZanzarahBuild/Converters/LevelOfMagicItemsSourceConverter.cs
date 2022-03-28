using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{


    public class LevelOfMagicItemsSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isPassive = value == DependencyProperty.UnsetValue ? false : (bool)value;
            ObservableCollection<SingleLevelOfMagic> singles = isPassive ? AppSources.LevelOfMagicPassiveList : AppSources.LevelOfMagicActiveList;
            foreach (var s in singles) s.IsPassive = isPassive;
            return singles;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
