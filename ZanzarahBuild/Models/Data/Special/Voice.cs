using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ZanzarahBuild.Models.Data
{
    public class Voice : ModelBase
    {
        static List<Voice> VoiceList { get; set; }
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
            get { return $"{AppSources.GetLabel("Voice")} {Number}"; }
        }
        public static CroppedBitmap GetIcon(int number)
        {
            return new CroppedBitmap(
            new BitmapImage(new Uri(@"Bitmap\VOICES.BMP", UriKind.Relative)),
            new Int32Rect(1 + 40 * number, 2, 38, 38));
        }
        public static List<Voice> GetVoiceList()
        {
            if (VoiceList == null)
            {
                VoiceList = new List<Voice>();
                for (int i = 0; i < AppSources.Settings.VoiceNumber; i++)
                    VoiceList.Add(new Voice(i));
            }
            return VoiceList;
        }
        public Voice(int number)
        {
            Number = number;
        }
    }
}
