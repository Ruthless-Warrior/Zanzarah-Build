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
        public static string AccountPath { get; set; }
        public static string AccountMessage { get; set; }

        public static void AccountWrite(string message = "")
        {
            if (Settings.AccountEnabled == false) return;
            Directory.CreateDirectory($"Account\\");
            using (StreamWriter sw = File.AppendText($"Account\\{AccountPath}"))
            {
                sw.Write(message);
            }
        }

        public static void AccountWriteLine(string message = "")
        {
            if (Settings.AccountEnabled == false) return;
            Directory.CreateDirectory($"Account\\");
            using (StreamWriter sw = File.AppendText($"Account\\{AccountPath}"))
            {
                sw.WriteLine(message);
            }
        }
    }
}
