using Common.Wpf.Mvvm;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ZanzarahBuild.Models.Data
{
    public static class Slot
    {
        public static CroppedBitmap GetSlotIcon(int number)
        {
            switch (number)
            {
                case 0:
                    return new CroppedBitmap(
                    new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/DEC000T.PNG")),
                    new Int32Rect(78, 0, 40, 40));
                case 1:
                case 3:
                    return new CroppedBitmap(
                    new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/DEC000T.PNG")),
                    new Int32Rect(0, 0, 40, 40));
                case 2:
                case 4:
                    return new CroppedBitmap(
                    new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/DEC000T.PNG")),
                    new Int32Rect(39, 0, 40, 40));
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
