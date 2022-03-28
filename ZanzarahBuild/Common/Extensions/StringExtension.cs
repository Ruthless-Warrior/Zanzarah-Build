using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringExtension
    {
        public static string CroppedString(this string str, int count)
        {
            return str.Substring(0, str.Length - count);
        }
    }
}
