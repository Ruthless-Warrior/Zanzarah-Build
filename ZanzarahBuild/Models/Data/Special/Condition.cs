using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ZanzarahBuild.Models.Data
{
    public class Condition : ModelBase
    {
        static List<Condition> ConditionList { get; set; }
        public int Number { get; private set; }

        public CroppedBitmap Icon
        {
            get
            {
                return GetIcon(Number);
            }
        }
        public string Label
        {
            get
            {
                switch(Number)
                {
                    case 0: return AppSources.GetLabel("Normal");
                    case 1: return AppSources.GetLabel("Poisoned");
                    case 2: return AppSources.GetLabel("Burnt");
                    case 3: return AppSources.GetLabel("Bewitched");
                    case 4: return AppSources.GetLabel("Frozen");
                    case 5: return AppSources.GetLabel("Muted");
                }
                throw new ArgumentOutOfRangeException($"Wizform Condition Label is {Number}; Range is 0-5");
            }
        }
        public static CroppedBitmap GetIcon(int number)
        {
            int x = 0;
            switch (number)
            {
                case 0: x = 85; break;
                case 1: x = 145; break;
                case 2: x = 158; break;
                case 3: x = 210; break;
                case 4: x = 197; break;
                case 5: x = 171; break;
            }

            return new CroppedBitmap(
            new BitmapImage(new Uri(@"pack://application:,,,/Resources/BitmapSources/INF000T.png")),
            new Int32Rect(x, 1, 13, 15));
        }
        public static List<Condition> GetConditionList()
        {
            if (ConditionList == null)
            {
                ConditionList = new List<Condition>();
                for (int i = 0; i < 6; i++)
                    ConditionList.Add(new Condition(i));
            }
            return ConditionList;
        }
        public Condition(int number)
        {
            Number = number;
        }
    }
}
