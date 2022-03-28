using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public class Magic : ModelBase
    {
        private byte _level;
        private byte _slot;
        private LevelOfMagic _levelOfMagic = new LevelOfMagic();

        public int Number { get; }
        public byte Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged();
                }
            }
        }
        public LevelOfMagic LevelOfMagic
        {
            get { return _levelOfMagic; }
            set
            {
                if (_levelOfMagic != value)
                {
                    _levelOfMagic = value;
                    OnPropertyChanged();
                }
            }
        }
        public byte X1
        {
            get { return LevelOfMagic.Element1.Number; }
            set
            {
                if (LevelOfMagic.Element1.Number != value)
                {
                    LevelOfMagic.Element1.Number = value;
                    OnPropertyChanged();
                    OnPropertyChanged("LevelOfMagic");
                }
            }
        }
        public byte X2
        {
            get { return LevelOfMagic.Element2.Number; }
            set
            {
                if (LevelOfMagic.Element2.Number != value)
                {
                    LevelOfMagic.Element2.Number = value;
                    OnPropertyChanged();
                    OnPropertyChanged("LevelOfMagic");
                }
            }
        }
        public byte X3
        {
            get { return LevelOfMagic.Element3.Number; }
            set
            {
                if (LevelOfMagic.Element3.Number != value)
                {
                    LevelOfMagic.Element3.Number = value;
                    OnPropertyChanged();
                    OnPropertyChanged("LevelOfMagic");
                }
            }
        }
        public byte Slot
        {
            get { return _slot; }
            set
            {
                if (_slot != value)
                {
                    _slot = value;
                    LevelOfMagic.IsPassive = value % 2 == 0;
                    if (value == 0) Level = 0;
                    OnPropertyChanged();
                    OnPropertyChanged("IsExist");
                    OnPropertyChanged("IsPassive");
                    OnPropertyChanged("LevelOfMagic");
                    OnPropertyChanged("X1");
                    OnPropertyChanged("X2");
                    OnPropertyChanged("X3");
                }
            }
        }

        public bool IsExist
        {
            get { return Slot == 0; }
        }
        public bool IsPassive
        {
            get { return _levelOfMagic.IsPassive; }
        }
        public int MagicValue
        {
            get
            {
                if (Slot == 0) return -1;
                string s = "00"
                    + Level.ToString("X2")
                    + (Slot - 1).ToString("X1")
                    + LevelOfMagic.Element1.Number.ToString("X1")
                    + LevelOfMagic.Element2.Number.ToString("X1")
                    + LevelOfMagic.Element3.Number.ToString("X1");
                return Convert.ToInt32(s, 16);
            }
        }

        public Magic(int num) : this(num, -1) { }

        public Magic(int num, int m)
        {
            Number = num;
            if (m == -1) Level = Slot = X1 = X2 = X3 = 0;
            else
            {
                string s = m.ToString("X8");
                Level = Convert.ToByte($"{s[2]}{s[3]}".ToString(), 16);
                Slot = (byte)(Convert.ToByte(s[4].ToString(), 16) + 1);
                X1 = Convert.ToByte(s[5].ToString(), 16);
                X2 = Convert.ToByte(s[6].ToString(), 16);
                X3 = Convert.ToByte(s[7].ToString(), 16);
            }
        }


        //----------------------------------------------------------------------------


        public string Title_Label
        {
            get { return $"{AppSources.GetLabel("Level of Magic")} - {AppSources.GetLabel("Stage")} {Number}"; }
        }
        public string Level_Label
        {
            get { return AppSources.GetLabel("Level"); }
        }
    }
}
