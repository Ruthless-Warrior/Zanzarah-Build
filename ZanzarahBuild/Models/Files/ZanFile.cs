using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data.Files
{
    public abstract class ZanFile : ModelBase
    {
        IProgress<string> _progress;

        protected BinaryReader reader;
        protected BinaryWriter writer;

        public string FileName { get; private set; }
        public string FilePath { get; private set; }

        public abstract void Read(IProgress<string> progress);
        public abstract void Write(IProgress<string> progress);

        protected void BeginRead()
        {
            if (File.Exists(FilePath) == false)
                AppSources.ShowErrorMessage(new FileNotFoundException($"{FilePath} not found."));

            AccountWriteLine($"\n==== READING {'{'}{FileName}{'}'} FILE ====\n");
            reader = new BinaryReader(File.OpenRead(FilePath));
        }

        protected void BeginWrite()
        {
            if (File.Exists(FilePath) == false)
            {
                File.Create(FilePath).Close();
                AccountWriteLine($"\n==== {'{'}{FileName}{'}'} FILE - CREATED ====\n");
            }
            AccountWriteLine($"\n==== WRITING {'{'}{FileName}{'}'} FILE ====\n");
            writer = new BinaryWriter(File.OpenWrite(FilePath));
        }

        protected void EndRead()
        {
            if (reader != null) reader.Close();
            AccountWriteLine($"\n==== READING {'{'}{FileName}{'}'} FILE - FINISHED ====\n");
        }

        protected void EndWrite()
        {
            writer.Close();
            AccountWriteLine($"\n==== WRITING {'{'}{FileName}{'}'} FILE - FINISHED ====\n");
        }

        public T Read<T>(string property, int tabCount = 0, bool newLine = true, int bufferLength = 0)
        {
            string tab = new string('\t', tabCount);
            T value; byte[] buffer;
            if (newLine) AccountWriteLine();
            AccountWriteLine($"{tab}| Reading of: {property}");
            if (typeof(T) == typeof(string))
            {
                string nwln = $"{tab}|{new string(' ', 25)}";
                int len = Read<int>($"string length", tabCount, false);
                buffer = Read<byte[]>($"string chars", tabCount, false, len);
                value = (T)typeof(Common.BitConverter).GetMethod($"GetStringFromByteArray", new Type[] { typeof(byte[]) }).Invoke(null, new object[] { buffer });
                AccountWriteLine($"{tab}| {$"Value of String:".PadLeft(20, ' ')} => {Common.BitConverter.GetStringFromByteArray(buffer)}"
                    .Replace("\n", $"\n{nwln}".Replace("\r", $"\r{nwln}")));
            }
            else if (typeof(T) == typeof(byte[]))
            {
                value = (T)typeof(BinaryReader).GetMethod($"ReadBytes", new Type[] { typeof(int) }).Invoke(reader, new object[] { bufferLength });
                buffer = (byte[])typeof(Common.BitConverter).GetMethod("GetBytes", new Type[] { typeof(T) }).Invoke(null, new object[] { value });
                AccountWriteLine($"{tab}| {$"Read {typeof(T).Name}".PadLeft(20, ' ')} => {Common.BitConverter.HexBytes(buffer, " ")}");
                AccountWriteLine($"{tab}| {$"Content".PadLeft(20, ' ')} => {value.ToString()}");
            }
            else
            {
                value = (T)typeof(BinaryReader).GetMethod($"Read{typeof(T).Name}", new Type[] { }).Invoke(reader, null);
                buffer = (byte[])typeof(Common.BitConverter).GetMethod("GetBytes", new Type[] { typeof(T) }).Invoke(null, new object[] { value });
                AccountWriteLine($"{tab}| {$"Read {typeof(T).Name}".PadLeft(20, ' ')} => {Common.BitConverter.HexBytes(buffer, " ")}");
                AccountWriteLine($"{tab}| {$"Content".PadLeft(20, ' ')} => {value.ToString()}");
            }
            return value;
        }
        public void Write<T>(T value, string property, int tabCount = 0, bool newLine = true, int bufferLength = 0)
        {
            string tab = new string('\t', tabCount);
            byte[] buffer;
            if (newLine) AccountWriteLine();
            AccountWriteLine($"{tab}| Writing of: {property}");
            if (typeof(T) == typeof(string))
            {
                string nwln = $"{tab}|{new string(' ', 25)}";
                byte[] chars = Common.BitConverter.GetBytes((string)(object)value);
                int len = chars.Length;
                buffer = chars;
                Write(len, $"string length", tabCount, false);
                Write(chars, $"string chars", tabCount, false, len);
                AccountWriteLine($"{tab}| {$"Value of String:".PadLeft(20, ' ')} => {Common.BitConverter.GetStringFromByteArray(buffer)}"
                    .Replace("\n", $"\n{nwln}".Replace("\r", $"\r{nwln}")));
            }
            else if (typeof(T) == typeof(byte[]))
            {
                typeof(BinaryWriter).GetMethod($"Write", new Type[] { typeof(T) }).Invoke(writer, new object[] { value });
                buffer = (byte[])typeof(Common.BitConverter).GetMethod("GetBytes", new Type[] { typeof(T) }).Invoke(null, new object[] { value });
                AccountWriteLine($"{tab}| {$"Write {typeof(T).Name}".PadLeft(20, ' ')} => {Common.BitConverter.HexBytes(buffer, " ")}");
                AccountWriteLine($"{tab}| {$"Content".PadLeft(20, ' ')} => {value.ToString()}");
            }
            else
            {
                typeof(BinaryWriter).GetMethod($"Write", new Type[] { typeof(T) }).Invoke(writer, new object[] { value });
                buffer = (byte[])typeof(Common.BitConverter).GetMethod("GetBytes", new Type[] { typeof(T) }).Invoke(null, new object[] { value });
                AccountWriteLine($"{tab}| {$"Write {typeof(T).Name}".PadLeft(20, ' ')} => {Common.BitConverter.HexBytes(buffer, " ")}");
                AccountWriteLine($"{tab}| {$"Content".PadLeft(20, ' ')} => {value.ToString()}");
            }
        }

        protected void Report(string message)
        {
            _progress.Report(message);
        }
        public void ChangePath(string path)
        {
            FileName = Path.GetFileName($"{System.AppDomain.CurrentDomain.BaseDirectory}{path}");
            FilePath = path;
        }
        public ZanFile(IProgress<string> progress, string path)
        {
            ChangePath(path);
            _progress = progress;
        }
        protected void AccountWrite(string message = "")
        {
            AppSources.AccountWrite(message);
        }
        protected void AccountWriteLine(string message = "")
        {
            AppSources.AccountWriteLine(message);
        }

    }
}
