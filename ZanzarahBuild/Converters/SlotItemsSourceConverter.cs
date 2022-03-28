using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{
    public class SlotItemsSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<byte> numbers = value as IEnumerable<byte>;
            var slots = new[]
            {
                new { Number = (byte)0, Image = Slot.GetSlotIcon(0), SlotNumber = "", SlotTitle = $"{AppSources.GetLabel("None")}" },
                new { Number = (byte)1, Image = Slot.GetSlotIcon(1), SlotNumber = "1", SlotTitle = $"{AppSources.GetLabel("Slot")} 1 - {AppSources.GetLabel("Active")}" },
                new { Number = (byte)2, Image = Slot.GetSlotIcon(2), SlotNumber = "1", SlotTitle = $"{AppSources.GetLabel("Slot")} 1 - {AppSources.GetLabel("Passive")}" },
                new { Number = (byte)3, Image = Slot.GetSlotIcon(3), SlotNumber = "2", SlotTitle = $"{AppSources.GetLabel("Slot")} 2 - {AppSources.GetLabel("Active")}" },
                new { Number = (byte)4, Image = Slot.GetSlotIcon(4), SlotNumber = "2", SlotTitle = $"{AppSources.GetLabel("Slot")} 2 - {AppSources.GetLabel("Passive")}" }
            };
            return slots;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
