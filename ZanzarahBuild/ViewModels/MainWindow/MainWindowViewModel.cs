using Common.Wpf.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public partial class MainWindowViewModel : WindowViewModelBase
    {
        //private static string _consoleMessage;


        //public string ConsoleMessage
        //{
        //    get { return _consoleMessage; }
        //    set
        //    {
        //        _consoleMessage = value;
        //        OnPropertyChanged();
        //    }
        //}


        //public ImageSource Icon
        //{
        //    get
        //    {
        //        if (_icon == null)
        //            return new CroppedBitmap(
        //        new BitmapImage(new Uri(@"D:\Misc\Res\ZAN000T.BMP")),
        //        new Int32Rect(1, 2, 38, 38));

        //        return _icon;
        //    }
        //    set
        //    {
        //        if (_icon != value)
        //        {
        //            _icon = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public string Version { get; set; } = "2.0.2";

        public override ImageSource Icon
        {
            get
            {
                return base.Icon == null ? new BitmapImage(new Uri(@"pack://application:,,,/Resources/zb.PNG")) : base.Icon;
            }
            set
            {
                base.Icon = value;
            }
        }


        internal void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["WindowState"].Value = WindowState.ToString();
            if (WindowState == WindowState.Normal)
            {
                config.AppSettings.Settings["WindowWidth"].Value = Width.ToString();
                config.AppSettings.Settings["WindowHeight"].Value = Height.ToString();
            }
            ConfigurationManager.RefreshSection("appSettings");
            config.Save();
        }

        public MainWindowViewModel()
        {
            WindowState = (WindowState)Enum.Parse(typeof(WindowState), ConfigurationManager.AppSettings["WindowState"], true);
            Width = int.Parse(ConfigurationManager.AppSettings["WindowWidth"]);
            Height = int.Parse(ConfigurationManager.AppSettings["WindowHeight"]);

            AppSources.Settings.Language =
                (Language)Enum.Parse(typeof(Language), ConfigurationManager.AppSettings["Language"], true);

            AppSources.Settings.AccountEnabled = bool.Parse(ConfigurationManager.AppSettings["AccountEnabled"]);

            AppSources.Settings.DataSorting = bool.Parse(ConfigurationManager.AppSettings["DataSorting"]);
            AppSources.Settings.SaveSorting = bool.Parse(ConfigurationManager.AppSettings["SaveSorting"]);
            AppSources.Settings.DataBackupCreating = bool.Parse(ConfigurationManager.AppSettings["DataBackupCreating"]);
            AppSources.Settings.SaveBackupCreating = bool.Parse(ConfigurationManager.AppSettings["SaveBackupCreating"]);


            Menu_Visibility = Visibility.Hidden;
            CollapseAllViews();
            AppSources.MainWindow_ViewModel = this;
            Title = "Zanzarah Build";
        }
    }
}
