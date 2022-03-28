using Common.Wpf.Mvvm;
using System.Collections.Generic;

namespace ZanzarahBuild.Models.Data
{
    public class LevelOfMagic : ModelBase
    {
        private SingleLevelOfMagic _sel1 = new SingleLevelOfMagic();
        private SingleLevelOfMagic _sel2 = new SingleLevelOfMagic();
        private SingleLevelOfMagic _sel3 = new SingleLevelOfMagic();

        public Element Element1
        {
            get { return _sel1.Element; }
            set
            {
                if (_sel1.Element != value)
                {
                    _sel1.Element = value;
                    OnPropertyChanged();
                }
            }
        }
        public Element Element2
        {
            get { return _sel2.Element; }
            set
            {
                if (_sel2.Element != value)
                {
                    _sel2.Element = value;
                    OnPropertyChanged();
                }
            }
        }
        public Element Element3
        {
            get { return _sel3.Element; }
            set
            {
                if (_sel3.Element != value)
                {
                    _sel3.Element = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsPassive
        {
            get { return _sel1.IsPassive; }
            set
            {
                if (_sel1.IsPassive != value)
                {
                    _sel1.IsPassive =
                        _sel2.IsPassive =
                        _sel3.IsPassive = value;
                    OnPropertyChanged();
                }
            }
        }

        public LevelOfMagic(Element element1 = null, Element element2 = null, Element element3 = null, bool passive = false)
        {
            Element1 = element1 == null ? new Element() : element1;
            Element2 = element2 == null ? new Element() : element2;
            Element3 = element3 == null ? new Element() : element3;
            IsPassive = passive;
        }

        public override bool Equals(object obj)
        {
            return obj is LevelOfMagic magic &&
                   EqualityComparer<Element>.Default.Equals(Element1, magic.Element1) &&
                   EqualityComparer<Element>.Default.Equals(Element2, magic.Element2) &&
                   EqualityComparer<Element>.Default.Equals(Element3, magic.Element3) &&
                   IsPassive == magic.IsPassive;
        }

        public override int GetHashCode()
        {
            var hashCode = 2116835403;
            hashCode = hashCode * -1521134295 + EqualityComparer<Element>.Default.GetHashCode(Element1);
            hashCode = hashCode * -1521134295 + EqualityComparer<Element>.Default.GetHashCode(Element2);
            hashCode = hashCode * -1521134295 + EqualityComparer<Element>.Default.GetHashCode(Element3);
            hashCode = hashCode * -1521134295 + IsPassive.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(LevelOfMagic left, LevelOfMagic right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(LevelOfMagic left, LevelOfMagic right)
        {
            return !(left == right);
        }
    }
}
