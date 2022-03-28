using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        public const string LABEL = "Zanzarah Build";
        public static Progress<string> Progress { get; set; }
        public static ImageSource Icon
        {
            get
            {
                return MainWindow_ViewModel.Icon;
            }
            set
            {
                MainWindow_ViewModel.Icon = value;
            }
        }
        public static string Title
        {
            get
            {
                var title = MainWindow_ViewModel.Title;
                int x = $"{LABEL} - ".Length;
                if (title.Trim() == LABEL || title.Trim() == $"{LABEL} -") return "";
                if (title.Length >= x && title.Substring(0, 17) == $"{LABEL} - ")
                    return title.Substring(x);
                return title;
            }
            set
            {
                if (value == string.Empty || value == null)
                    MainWindow_ViewModel.Title = "Zanzarah Build";
                else
                    MainWindow_ViewModel.Title = "Zanzarah Build - " + value;
            }
        }
        public static string FullTitle
        {
            get
            {
                return MainWindow_ViewModel.Title;
            }
            set
            {
                MainWindow_ViewModel.Title = value;
            }
        }
    }
}
