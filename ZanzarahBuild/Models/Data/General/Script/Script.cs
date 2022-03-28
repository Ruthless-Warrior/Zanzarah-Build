using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public class Deck : ModelBase
    {
        private WizformFile _wizformFile;


        public Script Script { get; set; }
        public WizformFile WizformFile
        {
            get { return _wizformFile; }
            set
            {
                if (_wizformFile != value)
                {
                    _wizformFile = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
