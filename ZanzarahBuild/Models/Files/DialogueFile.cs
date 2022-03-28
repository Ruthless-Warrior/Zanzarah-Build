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
    public class DialogueFile : ZanFile
    {
        private ObservableCollection<Dialogue> _dialogues;
        public ObservableCollection<Dialogue> Dialogues
        {
            get { return _dialogues; }
            set
            {
                if (_dialogues != value)
                {
                    _dialogues = new ObservableCollection<Dialogue>(value.OrderBy(x => x.TopicCode));
                    OnPropertyChanged();
                }
            }
        }
        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Dialogue> dialogues = new ObservableCollection<Dialogue>();
            try
            {
                AppSources.AccountPath = "_fb0x06 reading - account.txt";
                BeginRead();
                int count = Read<int>("Dialogue Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Dialogues: {i + 1} / {count}");
                    AccountWriteLine($" ========= Dialogue: {i + 1} / {count}");

                    Dialogue dialogue = new Dialogue();

                    dialogue.FileNumber = i;

                    dialogue.Id = Read<int>("ID", 1);

                    foreach (int x in new int[3] { 0x03, 0x00, 0x00 }) Read<int>("-", 1);

                    dialogue.Message = Read<string>("Message", 1).CroppedString(1);

                    foreach (int x in new int[3] { 0x01, 0x1d, 0x04 }) Read<int>("-", 1);

                    dialogue.TopicCode = Read<int>("Topic Code", 1);

                    foreach (int x in new int[3] { 0x00, 0x1e, 0x01 }) Read<int>("-", 1);

                    Read<byte>("-", 1); // 00

                    dialogues.Add(dialogue);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Dialogues = dialogues;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Dialogue> dialogues;
            if (AppSources.Settings.DataSorting) dialogues = new ObservableCollection<Dialogue>(Dialogues);
            else dialogues = new ObservableCollection<Dialogue>(Dialogues.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x06 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x06.fbs");
                }
                BeginWrite();
                Write(Dialogues.Count, "Dialogue Count");
                for (int i = 0; i < Dialogues.Count; i++)
                {
                    progress.Report($"Dialogues: {i + 1} / {Dialogues.Count}");
                    AccountWriteLine($" ========= Dialogue: {i + 1} / {Dialogues.Count}");

                    Dialogue dialogue = dialogues[i];

                    Write(dialogue.Id, "ID", 1);

                    foreach (int x in new int[3] { 0x03, 0x00, 0x00 }) Write(x, "-", 1);

                    Write(dialogue.Message + (char)0, "Message", 1);

                    foreach (int x in new int[3] { 0x01, 0x1d, 0x04 }) Write(x, "-", 1);

                    Write(dialogue.TopicCode, "Topic Code", 1);

                    foreach (int x in new int[3] { 0x00, 0x1e, 0x01 }) Write(x, "-", 1);

                    Write<byte>(0, "-", 1); // 00
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

        public DialogueFile(IProgress<string> progress, string path = "Data\\_fb0x06.fbs") : base(progress, path)
        {
            Read(progress);
        }
    }
}
