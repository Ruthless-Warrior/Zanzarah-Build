using System;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{


    public class ActiveSpellIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            InventorySpell invSpell = value as InventorySpell;
            if (invSpell == null || invSpell.Spell == null) return AppSources.GetNullableSpellIcon(false);
            else return invSpell.Spell.Icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
