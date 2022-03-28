using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Common.Wpf.Mvvm
{
    public abstract class WindowViewModelBase : ViewModelBase
    {
        private string _progressText;
        private ImageSource _icon;
        private string _title;

        private ViewModelBase _presenterViewModel;

        private RelayCommand _dragWindowCommand;
        private RelayCommand _minimizeWindowCommand;
        private RelayCommand _maximizeWindowCommand;
        private RelayCommand _closeWindowCommand;


        public string ProgressText
        {
            get { return _progressText; }
            set
            {
                if (_progressText != value)
                {
                    _progressText = value;
                    OnPropertyChanged();
                }
            }
        }
        public virtual ImageSource Icon
        {
            get { return _icon; }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged();
                }
            }
        }
        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewModelBase PresenterViewModel
        {
            get { return _presenterViewModel; }
            set
            {
                if (_presenterViewModel != value)
                {
                    _presenterViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand DragWindowCommand
        {
            get
            {
                return _dragWindowCommand ??
                    (_dragWindowCommand = new RelayCommand(parameter =>
                    {
                        var window = parameter as Window;
                        //if (window.mo == WindowState.Maximized) window.WindowState = WindowState.Normal;
                        (parameter as Window).DragMove();
                    },
                    (parameter) => parameter != null && parameter is Window));
            }
        }
        public RelayCommand MinimizeWindowCommand
        {
            get
            {
                return _minimizeWindowCommand ??
                    (_minimizeWindowCommand = new RelayCommand(parameter =>
                    {
                        (parameter as Window).WindowState = WindowState.Minimized;
                    },
                    (parameter) => parameter != null && parameter is Window));
            }
        }
        public RelayCommand MaximizeWindowCommand
        {
            get
            {
                return _maximizeWindowCommand ??
                    (_maximizeWindowCommand = new RelayCommand(parameter =>
                    {
                        var window = parameter as Window;
                        window.WindowState = window.WindowState == WindowState.Normal
                        ? WindowState.Maximized
                        : WindowState.Normal;
                    },
                    (parameter) => parameter != null && parameter is Window));
            }
        }
        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ??
                    (_closeWindowCommand = new RelayCommand(parameter =>
                    {
                        (parameter as Window).Close();
                    },
                    (parameter) => parameter != null && parameter is Window));
            }
        }
    }
}
