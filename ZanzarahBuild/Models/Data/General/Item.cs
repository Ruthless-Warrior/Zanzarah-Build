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
    public class Item : InventoryEntity
    {
        private int _additionalInfo;
        private bool _isNotMoney;
        private string _functionalParameters = "";
        private int _type;

        public int Litter0 { get; set; }
        public int Litter1 { get; set; }
        public int Litter2 { get; set; }
        public int Litter3 { get; set; }

        public int AdditionalInfo
        {
            get { return _additionalInfo; }
            set
            {
                if (_additionalInfo != value)
                {
                    _additionalInfo = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasAdditionalInfo
        {
            //Has Type
            get { return AdditionalInfo == 6; }
        }
        public bool IsNotMoney
        {
            get { return _isNotMoney; }
            set
            {
                if (_isNotMoney != value)
                {
                    _isNotMoney = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Script
        {
            get { return _functionalParameters; }
            set
            {
                if (_functionalParameters != value)
                {
                    _functionalParameters = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }
        public override CroppedBitmap Icon
        {
            get
            {
                if (Number < 0 || Number > 73)
                    return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\ZAN000T.BMP", UriKind.Relative)),
                new Int32Rect(1, 2, 38, 38));

                return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\Origin\ITM000T.BMP", UriKind.Relative)),
                new Int32Rect(1 + 40 * Number, 2, 38, 38));
            }
        }

        public Item(TextFile textFile) : base(textFile) { }
    }
}
