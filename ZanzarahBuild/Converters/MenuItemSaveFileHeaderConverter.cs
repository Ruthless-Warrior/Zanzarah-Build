using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.Converters
{
    public class MenuItemSaveFileHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string p = parameter.ToString().ToUpper();
            string path = "";
            string name = "";
            switch (p[0])
            {
                case 'S':
                    path = $"Save\\_000{p[1]}.dat";
                    break;
                case 'V':
                    path =
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\AppData\\Local\\VirtualStore\\Program Files (x86)\\Zanzarah\\Save\\_000{p[1]}.dat";
                    break;
            }
            if (File.Exists(path) == false) return AppSources.GetLabel("not found");
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
                {
                    reader.ReadBytes(reader.ReadInt32());
                    reader.ReadBytes(16);
                    reader.ReadBytes(reader.ReadInt32());
                    reader.ReadBytes(reader.ReadInt32());
                    reader.ReadBytes(8);
                    name = Encoding.GetEncoding("windows-1251").GetString(reader.ReadBytes(reader.ReadInt32()));
                }
                return $"Save _000{p[1]}.dat - {name}";
            }
            catch
            {
                return AppSources.GetLabel("file is corrupted");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException($"MenuItemSaveFileHeaderConverter - ConvertBack(value = {value}, parameter = {parameter})");
        }
    }
}
