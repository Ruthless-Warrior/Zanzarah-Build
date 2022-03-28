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
    public class ItemFile : ZanFile
    {
        private TextFile _textFile;
        private ObservableCollection<Item> _items;

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
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = new ObservableCollection<Item>(value.OrderBy(x => x.Number));
                OnPropertyChanged();
            }
        }
        public override void Read(IProgress<string> progress)
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            try
            {
                AppSources.AccountPath = "_fb0x04 reading - account.txt";
                BeginRead();
                int count = Read<int>("Item Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Items: {i + 1} / {count}");
                    AccountWriteLine($" ========= Item: {i + 1} / {count}");

                    Item item = new Item(TextFile);

                    item.FileNumber = i;

                    item.Id = Read<int>("", 1);

                    item.AdditionalInfo = Read<int>("Additional Info", 1);

                    foreach (int x in new int[3] { 0x03, 0x03, 0x08 }) Read<int>("-", 1);

                    item.NameId = Read<int>("Name ID", 1);

                    item.Litter0 = Read<int>("<-- Litter 0 -->", 1);

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Read<int>("-", 1);

                    Read<byte>("-", 1); // 00
                    Read<byte>("-", 1); // 00
                    item.Number = Read<byte>("Number", 1);
                    Read<byte>("-", 1); // 00

                    foreach (int x in new int[3] { 0x03, 0x05, 0x08 }) Read<int>("-", 1);

                    item.DescriptionId = Read<int>("Description ID", 1);

                    item.Litter1 = Read<int>("<-- Litter 1 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Read<int>("-", 1);

                    item.Litter2 = Read<int>("<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Read<int>("-", 1);

                    item.IsNotMoney = Read<bool>("Is not Money", 1);

                    foreach (int x in new int[1] { 0x00 }) Read<int>("-", 1);

                    item.Litter3 = Read<int>("<-- Litter 3 -->", 1);

                    item.Script = Read<string>("Script", 1).CroppedString(1);

                    if (item.HasAdditionalInfo)
                    {
                        foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Read<int>("-", 1);

                        item.Type = Read<int>("Type", 1);
                    }

                    items.Add(item);
                }
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
            finally
            {
                EndRead();
                Items = items;
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<Item> items;
            if (AppSources.Settings.DataSorting) items = new ObservableCollection<Item>(Items);
            else items = new ObservableCollection<Item>(Items.OrderBy(x => x.FileNumber));
            try
            {
                AppSources.AccountPath = "_fb0x04 writing - account.txt";
                if (AppSources.Settings.DataBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Data\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Data\\_fb0x04.fbs");
                }
                BeginWrite();
                Write(Items.Count, "Item Count");
                for (int i = 0; i < Items.Count; i++)
                {
                    progress.Report($"Items: {i + 1} / {Items.Count}");
                    AccountWriteLine($" ========= Item: {i + 1} / {Items.Count}");

                    Item item = items[i];

                    Write(item.Id, "", 1);

                    Write(item.AdditionalInfo, "Additional Info", 1);

                    foreach (int x in new int[3] { 0x03, 0x03, 0x08 }) Write(x, "-", 1);

                    Write(item.NameId, "Name ID", 1);

                    Write(item.Litter0, "<-- Litter 0 -->", 1);

                    foreach (int x in new int[3] { 0x01, 0x04, 0x04 }) Write(x, "-", 1);

                    Write<byte>(0, "-", 1); // 00
                    Write<byte>(0, "-", 1); // 00
                    Write((byte)item.Number, "Number", 1);
                    Write<byte>(0, "-", 1); // 00

                    foreach (int x in new int[3] { 0x03, 0x05, 0x08 }) Write(x, "-", 1);

                    Write(item.DescriptionId, "Description ID", 1);

                    Write(item.Litter1, "<-- Litter 1 -->", 1);

                    foreach (int x in new int[1] { 0x04 }) Write(x, "-", 1);

                    Write(item.Litter2, "<-- Litter 2 -->", 1);

                    foreach (int x in new int[1] { 0x01 }) Write(x, "-", 1);

                    Write(item.IsNotMoney, "Is not Money", 1);

                    foreach (int x in new int[1] { 0x00 }) Write(x, "-", 1);

                    Write(item.Litter3, "<-- Litter 3 -->", 1);

                    Write(item.Script + (char)0, "Script", 1);

                    if (item.HasAdditionalInfo)
                    {
                        foreach (int x in new int[3] { 0x01, 0x13, 0x04 }) Write(x, "-", 1);

                        Write(item.Type, "Type", 1);
                    }
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
        public ItemFile(IProgress<string> progress, TextFile textFile, string path = "Data\\_fb0x04.fbs") : base(progress, path)
        {
            TextFile = textFile;
            Read(progress);
        }
    }
}
