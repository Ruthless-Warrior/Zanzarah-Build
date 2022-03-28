using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        public static class Settings
        {
            public static Language Language { get; set; }
            public static bool AccountEnabled { get; set; }


            public static bool DataSorting { get; set; }
            public static bool SaveSorting { get; set; }
            public static bool DataBackupCreating { get; set; }
            public static bool SaveBackupCreating { get; set; }

            public static int VoiceNumber { get; set; }
        }
    }
}
