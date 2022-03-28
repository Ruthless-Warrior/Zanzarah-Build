using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        public static ObservableCollection<SingleLevelOfMagic> _levelOfMagicActiveList;
        public static ObservableCollection<SingleLevelOfMagic> LevelOfMagicActiveList
        {
            get
            {
                if (_levelOfMagicActiveList != null) return _levelOfMagicActiveList;
                _levelOfMagicActiveList = new ObservableCollection<SingleLevelOfMagic>();
                for (byte b = 0x0; b < 0xE; b++) _levelOfMagicActiveList.Add(new SingleLevelOfMagic(new Element(b), false));
                return _levelOfMagicActiveList;
            }
        }
        public static ObservableCollection<SingleLevelOfMagic> _levelOfMagicPassiveList;
        public static ObservableCollection<SingleLevelOfMagic> LevelOfMagicPassiveList
        {
            get
            {
                if (_levelOfMagicPassiveList != null) return _levelOfMagicPassiveList;
                _levelOfMagicPassiveList = new ObservableCollection<SingleLevelOfMagic>();
                for (byte b = 0x0; b < 0xE; b++) _levelOfMagicPassiveList.Add(new SingleLevelOfMagic(new Element(b), true));
                return _levelOfMagicPassiveList;
            }
        }
    }
}
