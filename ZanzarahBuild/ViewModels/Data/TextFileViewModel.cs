using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class TextFileViewModel : ViewModelBase
    {
        private Text _selected;
        private TextFile _file;

        public Text Selected
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
                new Int32Rect(41, 2, 38, 38));
                AppSources.Title = AppSources.GetLabel("Text");
            }
        }

        public TextFile File
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



        public Dictionary<int, string> TextTypesDictionary
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 0x0, AppSources.GetLabel("Wizform Name") },
                    { 0x1, AppSources.GetLabel("Spell Name") },
                    { 0x2, AppSources.GetLabel("Item Name") },
                    { 0x3, AppSources.GetLabel("Credits") },
                    { 0x4, AppSources.GetLabel("Service") },
                    { 0x5, AppSources.GetLabel("Object or NPC") },
                    { 0x7, AppSources.GetLabel("Location") },
                    { 0x8, AppSources.GetLabel("Road Sign") },
                    { 0x9, AppSources.GetLabel("Item Description") },
                    { 0xA, AppSources.GetLabel("Spell Description") },
                    { 0xB, AppSources.GetLabel("Wizform Description") },
                    { 0xC, AppSources.GetLabel("Category") },
                    { 0xE, AppSources.GetLabel("Launcher") },
                    { 0xF, AppSources.GetLabel("Element") },
                };
            }
        }



        public string Type_Label
        {
            get { return AppSources.GetLabel("Type"); }
        }
        public string Mark_Label
        {
            get { return AppSources.GetLabel("Mark"); }
        }
        public string Content_Label
        {
            get { return AppSources.GetLabel("Content"); }
        }
    }
}
