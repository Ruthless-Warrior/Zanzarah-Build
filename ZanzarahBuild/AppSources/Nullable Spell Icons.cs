using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        // Files
        public static CroppedBitmap GetNullableSpellIcon(bool passive)
        {
            if (passive)
                return new CroppedBitmap(
                new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/DEC000T.PNG" )),
                new Int32Rect(40, 1, 38, 38));
            else
                return new CroppedBitmap(
                new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/DEC000T.PNG")),
                new Int32Rect(1, 1, 38, 38));
        }
    }
}
