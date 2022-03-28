using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZanzarahBuild.Converters
{
    public class TextTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((int)value)
            {
                case 0x0: return AppSources.GetLabel("Wizform Name");
                case 0x1: return AppSources.GetLabel("Spell Name");
                case 0x2: return AppSources.GetLabel("Item Name");
                case 0x3: return AppSources.GetLabel("Credits");
                case 0x4: return AppSources.GetLabel("Service");
                case 0x5: return AppSources.GetLabel("Object or NPC");
                case 0x7: return AppSources.GetLabel("Location");
                case 0x8: return AppSources.GetLabel("Road Sign");
                case 0x9: return AppSources.GetLabel("Item Description");
                case 0xA: return AppSources.GetLabel("Spell Description");
                case 0xB: return AppSources.GetLabel("Wizform Description");
                case 0xC: return AppSources.GetLabel("Category");
                case 0xE: return AppSources.GetLabel("Launcher");
                case 0xF: return AppSources.GetLabel("Element");
            }
            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
