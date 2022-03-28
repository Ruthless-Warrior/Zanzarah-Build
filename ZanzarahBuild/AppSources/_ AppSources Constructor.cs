using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        static AppSources()
        {
            labels = new Dictionary<Language, Dictionary<string, string>>();
            string line; int i = 0;
            List<Language> langs = new List<Language>();
            StreamReader reader = new StreamReader(@"Labels\labels.txt", Encoding.GetEncoding("windows-1251"));
            while ((line = reader.ReadLine()) != "//--------------------------------")
            {
                Language al = (Language)Enum.Parse(typeof(Language), line);
                labels.Add(al, new Dictionary<string, string>());
                langs.Add(al);
            }
            string name = "";
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (i == 0) name = line;
                Language al = langs[i];
                labels[al].Add(name, line);
                i++;
                if (i == langs.Count) i = 0;
            }
        }
    }
}
