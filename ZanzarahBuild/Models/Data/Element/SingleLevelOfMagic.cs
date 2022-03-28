using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data
{
    public class SingleLevelOfMagic : ModelBase
    {
        private Element _element = new Element();
        private bool _isPassive;

        public Element Element
        {
            get { return _element; }
            set
            {
                if (_element != value)
                {
                    _element = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsPassive
        {
            get { return _isPassive; }
            set
            {
                if (_isPassive != value)
                {
                    _isPassive = value;
                    OnPropertyChanged();
                }
            }
        }
        public SingleLevelOfMagic(Element element = null, bool passive = false)
        {
            Element = element == null ? new Element(0) : element;
            IsPassive = passive;
        }

        public override bool Equals(object obj)
        {
            return obj is SingleLevelOfMagic magic &&
                   EqualityComparer<Element>.Default.Equals(Element, magic.Element) &&
                   IsPassive == magic.IsPassive;
        }

        public override int GetHashCode()
        {
            var hashCode = 1764268029;
            hashCode = hashCode * -1521134295 + EqualityComparer<Element>.Default.GetHashCode(Element);
            hashCode = hashCode * -1521134295 + IsPassive.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(SingleLevelOfMagic left, SingleLevelOfMagic right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(SingleLevelOfMagic left, SingleLevelOfMagic right)
        {
            return !(left == right);
        }
    }
}
