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
    public class DialogueFileViewModel : ViewModelBase
    {
        private Dialogue _selected;
        private DialogueFile _file;
        //--------------------------------------------------------------------------
        private RelayCommand _highlightCommand;
        private RelayCommand _cancelHighlightCommand;


        public Dialogue Selected
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
                new Int32Rect(121, 2, 38, 38));
                AppSources.Title = AppSources.GetLabel("Dialogue");
            }
        }

        public DialogueFile File
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

        public RelayCommand HighlightCommand
        {
            get
            {
                return _highlightCommand ??
                    (_highlightCommand = new RelayCommand(parameter =>
                    {
                        var rtb = parameter as RichTextBox;
                        var tb = rtb.FindResource("TextBrush");
                        rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, tb);
                    }));
            }
        }

        public RelayCommand CancelHighlightCommand
        {
            get
            {
                return _cancelHighlightCommand ??
                    (_cancelHighlightCommand = new RelayCommand(parameter =>
                    {
                        var rtb = parameter as RichTextBox;
                        var db = rtb.FindResource("DialogueBrush");
                        rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, db);
                    }));
            }
        }


        public string TopicCode_Label
        {
            get { return AppSources.GetLabel("Topic Code"); }
        }
        public string Message_Label
        {
            get { return AppSources.GetLabel("Message"); }
        }
        public string Highlight_Label
        {
            get { return AppSources.GetLabel("Highlight"); }
        }
        public string RemoveHighlighting_Label
        {
            get { return AppSources.GetLabel("Remove Highlighting"); }
        }
    }
}
