using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public abstract class Entity: ModelBase
    {
        private int _fileNumber = int.MaxValue;
        private int _id;

        public int FileNumber
        {
            get { return _fileNumber; }
            set
            {
                if (_fileNumber != value)
                {
                    _fileNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
