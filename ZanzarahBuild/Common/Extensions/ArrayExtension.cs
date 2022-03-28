using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ArrayExtension
    {
        public static void Populate<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
    }
}
