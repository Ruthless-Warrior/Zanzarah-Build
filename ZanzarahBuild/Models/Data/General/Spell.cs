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
    public class Spell : InventoryEntity
    {
        private LevelOfMagic _LevelOfMagic = new LevelOfMagic();
        private int _mana;
        private int _fireRate;
        private int _damage;

        public int Litter0 { get; set; }
        public int Litter1 { get; set; }
        public int Litter2 { get; set; }
        public int Litter3 { get; set; }
        public int Litter4 { get; set; }
        public int Litter5 { get; set; }
        public int Litter6 { get; set; }
        public int Litter7 { get; set; }
        public int Litter8 { get; set; }
        public int Litter9 { get; set; }


        public LevelOfMagic LevelOfMagic
        {
            get { return _LevelOfMagic; }
            set
            {
                if (_LevelOfMagic != value)
                {
                    _LevelOfMagic = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsPassive
        {
            get { return _LevelOfMagic.IsPassive; }
            set
            {
                if (_LevelOfMagic.IsPassive != value)
                {
                    _LevelOfMagic.IsPassive = value;
                    OnPropertyChanged();
                    OnPropertyChanged("LevelOfMagic");
                    OnPropertyChanged("Type");
                }
            }
        }
        public int Mana
        {
            get { return _mana; }
            set
            {
                if (_mana != value)
                {
                    _mana = value;
                    OnPropertyChanged();
                }
            }
        }
        public int FireRate
        {
            get { return _fireRate; }
            set
            {
                if (_fireRate != value)
                {
                    _fireRate = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Damage
        {
            get { return _damage; }
            set
            {
                if (_damage != value)
                {
                    _damage = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Effect { get; set; }

        public string Type
        {
            get { return IsPassive ? AppSources.GetLabel("Passive") : AppSources.GetLabel("Active"); }
        }
        public override CroppedBitmap Icon
        {
            get
            {
                if (Number < 0 || Number > 129)
                    return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\ZAN000T.BMP", UriKind.Relative)),
                new Int32Rect(1, 2, 38, 38));

                return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\Origin\SPL000T.BMP", UriKind.Relative)),
                new Int32Rect(38 * Number, 1, 38, 38));
            }
        }

        public static int ConvertMana(int x)
        {
            switch(x)
            {
                case 0: return 5;
                case 1: return 15;
                case 2: return 30;
                case 3: return 40;
                case 4: return 55;
                case 5: return 1000;
            }
            throw new ArgumentOutOfRangeException();
        }

        public Spell(TextFile textFile) : base(textFile) { }
    }
}
