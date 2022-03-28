using Common.Wpf.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private WindowState _windowState;
        private int _width;
        private int _height;

        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                if (_windowState != value)
                {
                    _windowState = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
