using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data.Files
{
    public class SpellFile : ZanFile
    {
        private TextFile _textFile;
        private ObservableCollection<Spell> _spells;

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
        public ObservableCollection<Spell> Spells
        {
            get { return _spells; }
            set
            {
                _spells = new ObservableCollection<Spell>(value.OrderBy(x => x.Number));
                OnPropertyChanged();
            }
        }
        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Spell> spells = new ObservableCollection<Spell>();
            try
            {
                AppSources.AccountPath = "_fb0x03 reading - account.txt";
                BeginRead();
                int count = Read<int>("Spell Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Spells: {i + 1} / {count}");
                    AccountWriteLine($" ========= Spell: {i + 1} / {count}");

                    Spell spell = new Spell(_textFile);

                    spell.FileNumber = i;

                    spell.Id = Read<int>("ID", 1);

                    foreach (int x in new int[4] { 0x0E, 0x03, 0x03, 0x08 }) Read<int>("-", 1);

                    spell.NameId = Read<int>("Name ID", 1);

                    spell.Litter0 = Read<int>("<-- Litter 0 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter1 = Read<int>("<-- Litter 1 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.LevelOfMagic.IsPassive = Convert.ToBoolean(Read<int>("Passive", 1));

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Read<int>("-", 1);

                    Read<byte>("-", 1); // 00
                    Read<byte>("-", 1); // 01
                    spell.Number = Read<byte>("Number", 1);
                    Read<byte>("-", 1); // 00

                    foreach (int x in new int[3] { 0x04, 0x15, 0x01 }) Read<int>("-", 1);

                    spell.LevelOfMagic.Element1 = new Element(Read<byte>("Element 1", 1));

                    foreach (int x in new int[3] { 0x04, 0x16, 0x01 }) Read<int>("-", 1);

                    spell.LevelOfMagic.Element2 = new Element(Read<byte>("Element 2", 1));

                    foreach (int x in new int[3] { 0x04, 0x17, 0x01 }) Read<int>("-", 1);

                    spell.LevelOfMagic.Element3 = new Element(Read<byte>("Element 3", 1));

                    foreach (int x in new int[3] { 0x03, 0x05, 0x08 }) Read<int>("-", 1);

                    spell.DescriptionId = Read<int>("Description ID", 1);

                    spell.Litter2 = Read<int>("<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter3 = Read<int>("<-- Litter 3 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.Mana = Read<int>("Mana", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter4 = Read<int>("<-- Litter 4 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.FireRate = Read<int>("Fire Rate", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter5 = Read<int>("<-- Litter 5 -->", 1);

                    foreach (int x in new int[3] { 0x04, 0x00, 0x01 }) Read<int>("-", 1);

                    spell.Litter6 = Read<int>("<-- Litter 6 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.Value1 = Read<int>("Value 1", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter7 = Read<int>("<-- Litter 7 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.Value2 = Read<int>("Value 2", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter8 = Read<int>("<-- Litter 8 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.Damage = Read<int>("Damage", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    spell.Litter9 = Read<int>("<-- Litter 9 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    spell.Effect = Read<int>("Effect", 1);

                    spells.Add(spell);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Spells = spells;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Spell> spells;
            if (AppSources.Settings.DataSorting) spells = new ObservableCollection<Spell>(Spells);
            else spells = new ObservableCollection<Spell>(Spells.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x03 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x03.fbs");
                }
                BeginWrite();
                Write(Spells.Count, "Spell Count");
                for (int i = 0; i < Spells.Count; i++)
                {
                    progress.Report($"Spells: {i + 1} / {Spells.Count}");
                    AccountWriteLine($" ========= Spell: {i + 1} / {Spells.Count}");

                    Spell spell = spells[i];

                    Write(spell.Id, "ID", 1);

                    foreach (int x in new int[4] { 0x0E, 0x03, 0x03, 0x08 }) Write(x, "-", 1);

                    Write(spell.NameId, "Name ID", 1);

                    Write(spell.Litter0, "<-- Litter 0 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter1, "<-- Litter 1 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(Convert.ToInt32(spell.LevelOfMagic.IsPassive), "Passive", 1);

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Write(x, "-", 1);

                    Write<byte>(0, "-", 1); // 00
                    Write<byte>(1, "-", 1); // 01
                    Write<byte>((byte)spell.Number, "Number", 1);
                    Write<byte>(0, "-", 1); // 00

                    foreach (int x in new int[3] { 0x04, 0x15, 0x01 }) Write(x, "-", 1);

                    Write<byte>(spell.LevelOfMagic.Element1.Number, "Element 1", 1);

                    foreach (int x in new int[3] { 0x04, 0x16, 0x01 }) Write(x, "-", 1);

                    Write<byte>(spell.LevelOfMagic.Element2.Number, "Element 2", 1);

                    foreach (int x in new int[3] { 0x04, 0x17, 0x01 }) Write(x, "-", 1);

                    Write<byte>(spell.LevelOfMagic.Element3.Number, "Element 3", 1);

                    foreach (int x in new int[3] { 0x03, 0x05, 0x08 }) Write(x, "-", 1);

                    Write(spell.DescriptionId, "Description ID", 1);

                    Write(spell.Litter2, "<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter3, "<-- Litter 3 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.Mana, "Mana", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter4, "<-- Litter 4 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.FireRate, "Fire Rate", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter5, "<-- Litter 5 -->", 1);

                    foreach (int x in new int[3] { 0x04, 0x00, 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter6, "<-- Litter 6 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.Value1, "Value 1", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter7, "<-- Litter 7 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.Value2, "Value 2", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter8, "<-- Litter 8 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.Damage, "Damage", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(spell.Litter9, "<-- Litter 9 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(spell.Effect, "Effect", 1);
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
        public SpellFile(IProgress<string> progress, TextFile textFile, string path = "Data\\_fb0x03.fbs") : base(progress, path)
        {
            TextFile = textFile;
            Read(progress);
        }
    }
}

