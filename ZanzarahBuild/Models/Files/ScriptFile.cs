using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data.Files
{
    public class ScriptFile : ZanFile
    {
        private ObservableCollection<Script> _scripts;
        public ObservableCollection<Script> Scripts
        {
            get { return _scripts; }
            set
            {
                if (_scripts != value)
                {
                    _scripts = value;
                    OnPropertyChanged();
                }
            }
        }
        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Script> scripts = new ObservableCollection<Script>();
            try
            {
                AppSources.AccountPath = "_fb0x05 reading - account.txt";
                BeginRead();
                int count = Read<int>("Script Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Scripts: {i + 1} / {count}");
                    AccountWriteLine($" ========= Script: {i + 1} / {count}");

                    Script script = new Script();

                    script.FileNumber = i;

                    script.Id = Read<int>("ID", 1);

                    foreach (int x in new int[4] { 0x07, 0x03, 0x03, 0x08 }) Read<int>("-", 1);

                    script.Code1 = Read<int>("Code 1", 1);
                    script.Code2 = Read<int>("Code 2", 1);

                    foreach (int x in new int[2] { 0x00, 0x18 }) Read<int>("-", 1);
                    script.String1 = Read<string>("String 1", 1).CroppedString(1);

                    foreach (int x in new int[2] { 0x00, 0x19 }) Read<int>("-", 1);
                    script.String2 = Read<string>("String 2", 1).CroppedString(1);

                    foreach (int x in new int[2] { 0x00, 0x1A }) Read<int>("-", 1);
                    script.String3 = Read<string>("String 3", 1).CroppedString(1);

                    foreach (int x in new int[2] { 0x00, 0x1B }) Read<int>("-", 1);
                    script.String4 = Read<string>("String 4", 1).CroppedString(1);

                    foreach (int x in new int[2] { 0x00, 0x1C }) Read<int>("-", 1);
                    script.String5 = Read<string>("String 5", 1).CroppedString(1);

                    foreach (int x in new int[2] { 0x00, 0x13 }) Read<int>("-", 1);
                    script.String6 = Read<string>("String 6", 1).CroppedString(1);

                    scripts.Add(script);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Scripts = scripts;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Script> scripts;
            if (AppSources.Settings.DataSorting) scripts = new ObservableCollection<Script>(Scripts);
            else scripts = new ObservableCollection<Script>(Scripts.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x05 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x05.fbs");
                }
                BeginWrite();
                Write(Scripts.Count, "Script Count");
                for (int i = 0; i < Scripts.Count; i++)
                {
                    progress.Report($"Scripts: {i + 1} / {Scripts.Count}");
                    AccountWriteLine($" ========= Script: {i + 1} / {Scripts.Count}");

                    Script script = scripts[i];

                    Write(script.Id, "ID", 1);

                    foreach (int x in new int[4] { 0x07, 0x03, 0x03, 0x08 }) Write(x, "-", 1);

                    Write(script.Code1, "Code 1", 1);
                    Write(script.Code2, "Code 2", 1);

                    foreach (int x in new int[2] { 0x00, 0x18 }) Write(x, "-", 1);
                    Write(script.String1 + (char)0, "String 1", 1);

                    foreach (int x in new int[2] { 0x00, 0x19 }) Write(x, "-", 1);
                    Write(script.String2 + (char)0, "String 2", 1);

                    foreach (int x in new int[2] { 0x00, 0x1A }) Write(x, "-", 1);
                    Write(script.String3 + (char)0, "String 3", 1);

                    foreach (int x in new int[2] { 0x00, 0x1B }) Write(x, "-", 1);
                    Write(script.String4 + (char)0, "String 4", 1);

                    foreach (int x in new int[2] { 0x00, 0x1C }) Write(x, "-", 1);
                    Write(script.String5 + (char)0, "String 5", 1);

                    foreach (int x in new int[2] { 0x00, 0x13 }) Write(x, "-", 1);
                    Write(script.String6 + (char)0, "String 6", 1);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndWrite();
            }
        }
        public ScriptFile(IProgress<string> progress, string path = "Data\\_fb0x05.fbs") : base(progress, path)
        {
            Read(progress);
        }
    }
}
