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
    public class TextFile : ZanFile
    {
        private ObservableCollection<Text> _texts;
        public ObservableCollection<Text> Texts
        {
            get { return _texts; }
            set
            {
                _texts = new ObservableCollection<Text>(value.OrderBy(x => x.Type).ThenBy(x => x.Content));

                if (AppSources.CurrentItemFile != null && AppSources.CurrentItemFile.Items != null)
                    foreach (var i in AppSources.CurrentItemFile.Items)
                    {
                        i.OnPropertyChanged("NameId");
                        i.OnPropertyChanged("Name");
                        i.OnPropertyChanged("DescriptionId");
                        i.OnPropertyChanged("Description");
                    }
                if (AppSources.CurrentSpellFile != null && AppSources.CurrentSpellFile.Spells != null)
                    foreach (var i in AppSources.CurrentSpellFile.Spells)
                    {
                        i.OnPropertyChanged("NameId");
                        i.OnPropertyChanged("Name");
                        i.OnPropertyChanged("DescriptionId");
                        i.OnPropertyChanged("Description");
                    }
                if (AppSources.CurrentWizformFile != null && AppSources.CurrentWizformFile.Wizforms != null)
                    foreach (var i in AppSources.CurrentWizformFile.Wizforms)
                    {
                        i.OnPropertyChanged("NameId");
                        i.OnPropertyChanged("Name");
                        i.OnPropertyChanged("DescriptionId");
                        i.OnPropertyChanged("Description");
                    }
                OnPropertyChanged();
            }
        }
        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Text> texts = new ObservableCollection<Text>();
            try
            {
                AppSources.AccountPath = "_fb0x02 reading - account.txt";
                BeginRead();
                int count = Read<int>("Text Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Texts: {i + 1} / {count}");
                    AccountWriteLine($" ========= Text: {i + 1} / {count}");

                    Text text = new Text();

                    text.FileNumber = i;

                    text.Id = Read<int>("ID", 1);

                    foreach (int x in new int[3] { 0x03, 0x00, 0x00 }) Read<int>("-", 1);

                    text.Content = Read<string>("Content", 1).CroppedString(1);

                    foreach (int x in new int[3] { 0x01, 0x01, 0x04 }) Read<int>("-", 1);

                    text.Type = Read<int>("Type", 1);

                    foreach (int x in new int[2] { 0x00, 0x02 }) Read<int>("-", 1);

                    text.Mark = Read<string>("Mark", 1).CroppedString(1);

                    texts.Add(text);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Texts = texts;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Text> texts;
            if (AppSources.Settings.DataSorting) texts = new ObservableCollection<Text>(Texts);
            else texts = new ObservableCollection<Text>(Texts.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x02 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x02.fbs");
                }
                BeginWrite();
                Write(Texts.Count, "Text Count");
                for (int i = 0; i < Texts.Count; i++)
                {
                    progress.Report($"Texts: {i + 1} / {Texts.Count}");
                    AccountWriteLine($" ========= Text: {i + 1} / {Texts.Count}");

                    Text text = texts[i];

                    Write(text.Id, "ID", 1);

                    foreach (int x in new int[3] { 0x03, 0x00, 0x00 }) Write(x, "-", 1);

                    Write(text.Content + (char)0, "Content", 1);

                    foreach (int x in new int[3] { 0x01, 0x01, 0x04 }) Write(x, "-", 1);

                    Write(text.Type, "Type", 1);

                    foreach (int x in new int[2] { 0x00, 0x02 }) Write(x, "-", 1);

                    Write(text.Mark + (char)0, "Mark", 1);
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
        public TextFile(IProgress<string> progress, string path = "Data\\_fb0x02.fbs") : base(progress, path)
        {
            Read(progress);
        }
    }
}
