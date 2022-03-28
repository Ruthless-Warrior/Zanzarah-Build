using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{


    public class WizformSpellConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            InventorySpell spell = value as InventorySpell;
            if (spell == null) return InventorySpell.NullableSpell;
            return spell;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == InventorySpell.NullableSpell) return null;
            return value;
        }
    }


}
