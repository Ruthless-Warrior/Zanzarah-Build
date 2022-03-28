using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZanzarahBuild.Models.Data
{
    public class Element : ModelBase
    {
        private byte _number;

        public byte Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get
            {
                return GetText(_number);
            }
        }
        public Element(byte number = 0)
        {
            Number = number;
        }
        public static string GetText(byte number)
        {
            switch (number)
            {
                case 0x0: return AppSources.GetLabel("None");
                case 0x1: return AppSources.GetLabel("Nature");
                case 0x2: return AppSources.GetLabel("Air");
                case 0x3: return AppSources.GetLabel("Water");
                case 0x4: return AppSources.GetLabel("Light");
                case 0x5: return AppSources.GetLabel("Energy");
                case 0x6: return AppSources.GetLabel("Psi");
                case 0x7: return AppSources.GetLabel("Stone");
                case 0x8: return AppSources.GetLabel("Ice");
                case 0x9: return AppSources.GetLabel("Fire");
                case 0xA: return AppSources.GetLabel("Dark");
                case 0xB: return AppSources.GetLabel("Chaos");
                case 0xC: return AppSources.GetLabel("Metal");
                case 0xD: return AppSources.GetLabel("Joker");
            }
            throw new ArgumentOutOfRangeException();
        }
        public static Int32Rect GetRect(byte number)
        {
            return new Int32Rect(number * 12, 1, 12, 12);
        }

        // right click => Quick Actions and Refactoring... => Generate Equals and GetHashCode...
        public override bool Equals(object obj)
        {
            return obj is Element element &&
                   Number == element.Number;
        }

        public override int GetHashCode()
        {
            return 187193536 + Number.GetHashCode();
        }

        public static bool operator ==(Element left, Element right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(Element left, Element right)
        {
            return !(left == right);
        }
    }
}
