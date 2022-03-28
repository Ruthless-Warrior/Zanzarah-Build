using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public class Script : Entity
    {
        private int _code1;
        private int _code2;
        private string _string1 = "";
        private string _string2 = "";
        private string _string3 = "";
        private string _string4 = "";
        private string _string5 = "";
        private string _string6 = "";

        public int Number
        {
            get { return FileNumber + 1; }
        }
        public int Code1
        {
            get { return _code1; }
            set
            {
                if (_code1 != value)
                {
                    _code1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Code2
        {
            get { return _code2; }
            set
            {
                if (_code2 != value)
                {
                    _code2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string String1
        {
            get { return _string1; }
            set
            {
                if (_string1 != value)
                {
                    _string1 = value.Replace("\r", "");
                    if (_string1 != "" && _string1.Last() != '\n')
                        _string1 += "\n";
                    OnPropertyChanged();
                }
            }
        }
        public string String2
        {
            get { return _string2; }
            set
            {
                if (_string2 != value)
                {
                    _string2 = value.Replace("\r", "");
                    if (_string2 != "" && _string2.Last() != '\n')
                        _string2 += "\n";
                    OnPropertyChanged();
                }
            }
        }
        public string String3
        {
            get { return _string3; }
            set
            {
                if (_string3 != value)
                {
                    _string3 = value.Replace("\r", "");
                    if (_string3 != "" && _string3.Last() != '\n')
                        _string3 += "\n";
                    OnPropertyChanged();
                }
            }
        }
        public string String4
        {
            get { return _string4; }
            set
            {
                if (_string4 != value)
                {
                    _string4 = value.Replace("\r", "");
                    if (_string4 != "" && _string4.Last() != '\n')
                        _string4 += "\n";
                    OnPropertyChanged();
                }
            }
        }
        public string String5
        {
            get { return _string5; }
            set
            {
                if (_string5 != value)
                {
                    _string5 = value.Replace("\r", "");
                    if (_string5 != "" && _string5.Last() != '\n')
                        _string5 += "\n";
                    OnPropertyChanged();
                }
            }
        }
        public string String6
        {
            get { return _string6; }
            set
            {
                if (_string6 != value)
                {
                    _string6 = value.Replace("\r", "");
                    OnPropertyChanged();
                }
            }
        }
    }
}
