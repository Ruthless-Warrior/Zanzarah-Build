using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class BitConverter
    {
        public static byte[] GetBytes(bool value)
        {
            return System.BitConverter.GetBytes(value);
        }

        public static byte[] GetBytes(short value)
        {
            return System.BitConverter.GetBytes(value);
        }

        public static byte[] GetBytes(int value)
        {
            return System.BitConverter.GetBytes(value);
        }

        public static byte[] GetBytes(long value)
        {
            return System.BitConverter.GetBytes(value);
        }

        public static byte[] GetBytes(byte value)
        {
            return new byte[] { value };
        }

        public static byte[] GetBytes(string value)
        {
            return Encoding.GetEncoding(1251).GetBytes(value);
        }

        public static byte[] GetBytes(byte[] value)
        {
            return value;
        }

        public static string GetStringFromByteArray(byte[] buffer)
        {
            return Encoding.GetEncoding(1251).GetString(buffer, 0, buffer.Length);
        }

        public static string HexBytes(byte[] buffer, string separator = "")
        {
            string str = "";
            foreach (byte b in buffer)
            {
                str += b.ToString("X2") + separator;
            }
            return str;
        }
    }
}
