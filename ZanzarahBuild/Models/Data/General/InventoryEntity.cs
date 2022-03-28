using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public abstract class InventoryEntity : Entity
    {
        private TextFile textFile;

        private int _number;
        private int _nameId;
        private int _descriptionId;

        public int Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Icon");
                }
            }
        }
        public int NameId
        {
            get { return _nameId; }
            set
            {
                if (_nameId != value)
                {
                    _nameId = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Name");
                }
            }
        }
        public int DescriptionId
        {
            get { return _descriptionId; }
            set
            {
                if (_descriptionId != value)
                {
                    _descriptionId = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Description");
                }
            }
        }

        public string Name
        {
            get
            {
                if (Number == -1) return AppSources.GetLabel("None");

                var txt = textFile.Texts.Where(t => t.Id == NameId).ToArray();
                if (txt.Length == 0) return AppSources.GetLabel("{ no matches found }");
                return txt[0].Content;
            }
            set
            {
                Text text = textFile.Texts.Where(t => t.Id == NameId).ToArray()[0];
                if (text.Content != value)
                {
                    text.Content = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get
            {
                var txt = textFile.Texts.Where(t => t.Id == DescriptionId).ToArray();
                if (txt.Length == 0) return AppSources.GetLabel("{ no matches found }");
                return txt[0].Content;
            }
            set
            {
                Text text = textFile.Texts.Where(t => t.Id == DescriptionId).ToArray()[0];
                if (text.Content != value)
                {
                    text.Content = value;
                    OnPropertyChanged();
                }
            }
        }
        public abstract CroppedBitmap Icon { get; }

        public InventoryEntity(TextFile textFile)
        {
            this.textFile = textFile;
        }
    }
}
