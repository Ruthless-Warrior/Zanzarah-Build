using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        public static void ShowErrorMessage(Exception exc)
        {
            var errorMessage = $"{exc.Message}";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", exc.StackTrace);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
