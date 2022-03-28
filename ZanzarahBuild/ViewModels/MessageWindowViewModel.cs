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
    public partial class MessageWindowViewModel : WindowViewModelBase
    {

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

        public string Message { get; set; }

        internal void OnWindowClosing(object sender, CancelEventArgs e)
        {
            
        }

        public MessageWindowViewModel()
        {
            
        }
    }
}
