using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class ScriptFileViewModel : ViewModelBase
    {
        private Script _selected;
        private string _hexCode;
        private ScriptFile _file;

        public Script Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    ChangeHeader();
                    OnPropertyChanged();
                }
            }
        }
        public void ChangeHeader()
        {
            if (Selected == null)
            {
                AppSources.Icon = null;
                AppSources.Title = null;
            }
            else
            {
                AppSources.Icon = new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\ZAN000T.BMP", UriKind.Relative)),
                new Int32Rect(81, 2, 38, 38));
                AppSources.Title = AppSources.GetLabel("Script");
            }
        }

        public ScriptFile File
        {
            get { return _file; }
            set
            {
                if (_file != value)
                {
                    _file = value;
                    OnPropertyChanged();
                }
            }
        }
        public string HexCode
        {
            get { return _hexCode; }
            set
            {
                if (_hexCode != value)
                {
                    _hexCode = value;
                    OnPropertyChanged();
                }
            }
        }



        private RelayCommand _changeHexCommand;
        public RelayCommand ChangeHexCommand
        {
            get
            {
                return _changeHexCommand ??
                    (_changeHexCommand = new RelayCommand(parameter =>
                    {
                        if (parameter == null) return;
                        string text = parameter.ToString();
                        string s = "";
                        foreach (char a in text)
                            s += ((byte)a).ToString("X2") + " ";
                        HexCode = s;
                    },
                    (parameter) => true));
            }
            set { _changeHexCommand = value; }
        }



        public string Number_Label
        {
            get { return AppSources.GetLabel("Number"); }
        }
        public string Code1_Label
        {
            get { return AppSources.GetLabel("Code") + " 1"; }
        }
        public string Code2_Label
        {
            get { return AppSources.GetLabel("Code") + " 2"; }
        }
        public string String1_Label
        {
            get { return AppSources.GetLabel("String") + " 1"; }
        }
        public string String2_Label
        {
            get { return AppSources.GetLabel("String") + " 2"; }
        }
        public string String3_Label
        {
            get { return AppSources.GetLabel("String") + " 3"; }
        }
        public string String4_Label
        {
            get { return AppSources.GetLabel("String") + " 4"; }
        }
        public string String5_Label
        {
            get { return AppSources.GetLabel("String") + " 5"; }
        }
        public string String6_Label
        {
            get { return AppSources.GetLabel("String") + " 6"; }
        }
    }
}
