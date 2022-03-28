using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild
{
    public static partial class AppSources
    {
        private static Dictionary<Language, Dictionary<string, string>> labels;
        public static string GetLabel(string name)
        {
            return labels[Settings.Language][name];
        }
    }
}
