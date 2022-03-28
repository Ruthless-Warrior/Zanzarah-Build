using Common;
using Common.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data.Files
{
    public class WizformFile : ZanFile
    {
        private TextFile _textFile;
        private ObservableCollection<Wizform> _wizforms;
        private ObservableCollection<Wizform> _evoWizforms;


        public TextFile TextFile
        {
            get { return _textFile; }
            set
            {
                if (_textFile != value)
                {
                    _textFile = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Wizform> Wizforms
        {
            get { return _wizforms; }
            set
            {
                _wizforms = new ObservableCollection<Wizform>(value.OrderBy(x => x.Number));
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Wizform> WizformEvoList
        {
            get
            {
                if (Wizforms == null) Wizforms = new ObservableCollection<Wizform>();
                if (_evoWizforms == null)
                {
                    _evoWizforms = new ObservableCollection<Wizform>(
                        new List<Wizform>
                        {
                            new Wizform(TextFile, this) { Number = -1 }
                        }.Concat(Wizforms)
                    );
                }
                return _evoWizforms;
            }
        }

        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Wizform> wizforms = new ObservableCollection<Wizform>();
            try
            {
                AppSources.AccountPath = "_fb0x01 reading - account.txt";
                BeginRead();
                int count = Read<int>("Wizform Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Wizforms: {i + 1} / {count}");
                    AccountWriteLine($" ========= Wizform: {i + 1} / {count}");

                    Wizform wizform = new Wizform(_textFile, this);

                    wizform.FileNumber = i;

                    wizform.Id = Read<int>("ID", 1);

                    foreach (int x in new int[3] { 0x1B, 0x00, 0x06 }) Read<int>("-", 1);

                    wizform.Model = Read<string>("Model", 1).CroppedString(1);

                    foreach (int x in new int[3] { 0x03, 0x03, 0x08 }) Read<int>("-", 1);

                    wizform.NameId = Read<int>("Name ID", 1);

                    foreach (int x in new int[2] { 0x0012BA08, 0x01 }) Read<int>("-", 1);

                    wizform.Litter0 = Read<int>("<-- Litter 0 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Element = new SingleLevelOfMagic(new Element((byte)Read<int>("Element", 1)), true);

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Read<int>("-", 1);

                    Read<byte>("-", 1); // 00
                    Read<byte>("-", 1); // 02
                    wizform.Number = Read<byte>("Number", 1);
                    Read<byte>("-", 1); // 00

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter1 = Read<int>("<-- Litter 1 -->", 1);

                    foreach (int x in new int[2] { 0x04, 0x00 }) Read<int>("-", 1);

                    int t = 0x09;
                    for (int ind = 0; ind < 10; ind++)
                    {
                        foreach (int x in new int[3] { 0x01, t, 0x04 }) Read<int>("-", 1);
                        wizform.Magics.Add(new Magic(ind, Read<int>($"Magic {ind + 1}", 1)));
                        t++;
                    }

                    foreach (int x in new int[1] { 0x03 }) Read<int>("-", 1);

                    wizform.Litter2 = Read<int>("<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x08 }) Read<int>("-", 1);

                    wizform.DescriptionId = Read<int>("Description ID", 1);

                    foreach (int x in new int[2] { 0x0012BA08, 0x01 }) Read<int>("-", 1);

                    wizform.Litter3 = Read<int>("<-- Litter 3 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.HitPoints = Read<int>("Hit Points", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter4 = Read<int>("<-- Litter 4 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.EvolutionWizformNumber = Read<int>("Evolution Wizform Number", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter5 = Read<int>("<-- Litter 5 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.EvolutionLevel = Read<int>("Evolution Level", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter6 = Read<int>("<-- Litter 6 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Dexterity = Read<int>("Dexterity", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter7 = Read<int>("<-- Litter 7 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.JumpAbility = Read<int>("Jump Ability", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter8 = Read<int>("<-- Litter 8 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Special = Read<int>("Special", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter9 = Read<int>("<-- Litter 9 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Voice = Read<int>("Voice", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter10 = Read<int>("<-- Litter 10 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Litter11 = Read<int>("<-- Litter 11 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    wizform.Litter12 = Read<int>("<-- Litter 12 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    wizform.Litter13 = Read<int>("<-- Litter 13 -->", 1);

                    foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Read<int>("-", 1);

                    wizform.ExperienceModifier = Read<int>("Xp Coefficient", 1);

                    foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Read<int>("-", 1);

                    wizform.Litter14 = Read<int>("<-- Litter 14 -->", 1);

                    wizforms.Add(wizform);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Wizforms = wizforms;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Wizform> wizforms;
            if (AppSources.Settings.DataSorting) wizforms = new ObservableCollection<Wizform>(Wizforms);
            else wizforms = new ObservableCollection<Wizform>(Wizforms.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x01 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x01.fbs");
                }
                BeginWrite();
                Write(Wizforms.Count, "Wizform Count");
                for (int i = 0; i < Wizforms.Count; i++)
                {
                    progress.Report($"Wizforms: {i + 1} / {Wizforms.Count}");
                    AccountWriteLine($" ========= Wizform: {i + 1} / {Wizforms.Count}");

                    Wizform wizform = wizforms[i];

                    Write(wizform.Id, "ID", 1);

                    foreach (int x in new int[3] { 0x1B, 0x00, 0x06 }) Write(x, "-", 1);

                    Write(wizform.Model + (char)0, "Model", 1);

                    foreach (int x in new int[3] { 0x03, 0x03, 0x08 }) Write(x, "-", 1);

                    Write(wizform.NameId, "Name ID", 1);

                    foreach (int x in new int[2] { 0x0012BA08, 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter0, "<-- Litter 0 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write<int>(wizform.Element.Element.Number, "Element", 1);

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Write(x, "-", 1);

                    Write<byte>(0, "-", 1); // 00
                    Write<byte>(2, "-", 1); // 02
                    Write((byte)wizform.Number, "Number", 1);
                    Write<byte>(0, "-", 1); // 00

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter1, "<-- Litter 1 -->", 1);

                    foreach (int x in new int[2] { 0x04, 0x00 }) Write(x, "-", 1);

                    int t = 0x09;
                    for (int ind = 0; ind < 10; ind++)
                    {
                        foreach (int x in new int[3] { 0x01, t, 0x04 }) Write(x, "-", 1);
                        Write(wizform.Magics[ind].MagicValue, $"Magic {ind + 1}", 1);
                        t++;
                    }

                    foreach (int x in new int[1] { 0x03 }) Write(x, "-", 1);

                    Write(wizform.Litter2, "<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x08 }) Write(x, "-", 1);

                    Write(wizform.DescriptionId, "Description ID", 1);

                    foreach (int x in new int[2] { 0x0012BA08, 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter3, "<-- Litter 3 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.HitPoints, "Hit Points", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter4, "<-- Litter 4 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.EvolutionWizformNumber, "Evolution Wizform Number", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter5, "<-- Litter 5 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.EvolutionLevel, "Evolution Level", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter6, "<-- Litter 6 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.Dexterity, "Dexterity", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter7, "<-- Litter 7 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.JumpAbility, "Jump Ability", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter8, "<-- Litter 8 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.Special, "Special", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter9, "<-- Litter 9 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.Voice, "Voice", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter10, "<-- Litter 10 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.Litter11, "<-- Litter 11 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(wizform.Litter12, "<-- Litter 12 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(wizform.Litter13, "<-- Litter 13 -->", 1);

                    foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Write(x, "-", 1);

                    Write(wizform.ExperienceModifier, "Xp Coefficient", 1);

                    foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Write(x, "-", 1);

                    Write(wizform.Litter14, "<-- Litter 14 -->", 1);
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
        public WizformFile(IProgress<string> progress, TextFile textFile, string path = "Data\\_fb0x01.fbs") : base(progress, path)
        {
            TextFile = textFile;
            Read(progress);
        }
    }
}

