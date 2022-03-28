using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZanzarahBuild.Models.Data.Files
{
    public class SaveFile : ZanFile
    {
        private List<object> vs1 = new List<object>();
        private List<object> vs2 = new List<object>();

        private ObservableCollection<InventoryItem> _items = new ObservableCollection<InventoryItem>();
        private ObservableCollection<InventorySpell> _spells = new ObservableCollection<InventorySpell>();
        private ObservableCollection<InventoryWizform> _wizforms = new ObservableCollection<InventoryWizform>();
        private TextFile _textFile;
        private ItemFile _itemFile;
        private SpellFile _spellFile;
        private WizformFile _wizformFile;

        public ObservableCollection<InventoryItem> Items
        {
            get { return _items; }
            set
            {
                _items = new ObservableCollection<InventoryItem>(value
                    .OrderBy(v => v.Item.Number));
                OnPropertyChanged();
            }
        }
        public ObservableCollection<InventorySpell> ActiveSpells
        {
            get
            {
                return new ObservableCollection<InventorySpell>(Spells.Where(s => s.Spell.IsPassive == false));
            }
        }
        public ObservableCollection<InventorySpell> PassiveSpells
        {
            get
            {
                return new ObservableCollection<InventorySpell>(Spells.Where(s => s.Spell.IsPassive == true));
            }
        }
        public ObservableCollection<InventorySpell> Spells
        {
            get { return _spells; }
            set
            {
                _spells = new ObservableCollection<InventorySpell>(value
                    .OrderBy(v => v.Spell.Number));
                OnPropertyChanged();
                OnPropertyChanged("ActiveSpells");
                OnPropertyChanged("PassiveSpells");
            }
        }
        public ObservableCollection<InventoryWizform> Wizforms
        {
            get { return _wizforms; }
            set
            {
                _wizforms = new ObservableCollection<InventoryWizform>(value
                    .OrderBy(v => v.Wizform.Number)
                    .ThenByDescending(v => v.Level)
                    .ThenBy(v => v.Name));
                OnPropertyChanged();
            }
        }
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
        public ItemFile ItemFile
        {
            get { return _itemFile; }
            set
            {
                if (_itemFile != value)
                {
                    _itemFile = value;
                    OnPropertyChanged();
                }
            }
        }
        public SpellFile SpellFile
        {
            get { return _spellFile; }
            set
            {
                if (_spellFile != value)
                {
                    _spellFile = value;
                    OnPropertyChanged();
                }
            }
        }
        public WizformFile WizformFile
        {
            get { return _wizformFile; }
            set
            {
                if (_wizformFile != value)
                {
                    _wizformFile = value;
                    OnPropertyChanged();
                }
            }
        }
        public override void Read(IProgress<string> progress)
        {
            try
            {
                ObservableCollection<InventoryItem> items = new ObservableCollection<InventoryItem>();
                ObservableCollection<InventorySpell> spells = new ObservableCollection<InventorySpell>();
                ObservableCollection<InventoryWizform> wizforms = new ObservableCollection<InventoryWizform>();

                AppSources.AccountPath = "_save reading - account.txt";
                BeginRead();

                Read<string>("Developers"); // Funatics

                for (int y = 0; y < 4; y++)
                    vs1.Add(Read<int>($"Number {y + 1}"));

                vs1.Add(Read<string>("Date"));
                vs1.Add(Read<string>("Time"));
                vs1.Add(Read<int>("Year"));
                
                Read<int>("-"); // 0

                vs1.Add(Read<string>("Save Name"));
                vs1.Add(Read<int>("Game Time, sec."));



                //--------------------------------------------------------------------------------

                vs1.Add(Read<int>("Number of Locations"));
                Read<int>("-"); // 8
                vs1.Add(Read<int>("Current Location Number"));
                vs1.Add(Read<int>("Enter at"));
                int locCount = Read<int>("Location Count");
                vs1.Add(locCount);

                for (int i = 0; i < locCount; i++)
                {
                    progress.Report($"Locations: {i + 1} / {locCount}");
                    AccountWriteLine($" ========= Locations: {i + 1} / {locCount}");

                    vs1.Add(Read<string>("Location Name", 1));
                    int eventCount = Read<int>("Event Count", 1);
                    vs1.Add(eventCount);

                    for (int j = 0; j < eventCount; j++)
                    {
                        progress.Report($"Events: {j + 1} / {eventCount}");
                        AccountWriteLine($" ========= Events: {j + 1} / {eventCount}");

                        int eventType = Read<int>("Event Type", 2);
                        vs1.Add(eventType);
                        switch (eventType)
                        {
                            case 1:
                            case 3:
                            case 4:
                                vs1.Add(Read<int>("< x1 >", 3));
                                break;
                            case 2:
                            case 6:
                                vs1.Add(Read<int>("< x1 >", 3));
                                vs1.Add(Read<int>("< x2 >", 3));
                                break;
                            default: break;
                        }
                    }
                }

                //---------------------------------------------------

                Dictionary<InventoryWizform, int[]> spellsOrderNumbers
                    = new Dictionary<InventoryWizform, int[]>();

                int count = Read<int>("Inventory Object Count");
                for (int i = 0; i < count; i++)
                {
                    progress.Report($"Inventory Objects: {i + 1} / {count}");
                    AccountWriteLine($" ========= Inventory Objects: {i + 1} / {count}");

                    InventoryObject inv = null;

                    Read<byte>("-", 1); // 0
                    byte objType = Read<byte>("Object Type", 1);
                    int number = Read<byte>("Number", 1);
                    Read<byte>("-", 1); // 0

                    int orderNumber = Read<int>("Order Number", 1);
                    int dataId = Read<int>("Data ID", 1);
                    int quantity = Read<int>("Quantity", 1);
                    bool isUsed = Read<bool>("Used", 1);

                    switch (objType)
                    {
                        case 0:
                            inv = new InventoryItem(number, dataId, ItemFile);
                            InventoryItem invItem = inv as InventoryItem;
                            invItem.Quantity = quantity;

                            items.Add(invItem);
                            break;

                        case 1:
                            inv = new InventorySpell(number, dataId, SpellFile);
                            InventorySpell invSpell = inv as InventorySpell;
                            invSpell.Id = Read<int>("ID", 1);
                            invSpell.Mana = Read<int>("Mana", 1);

                            spells.Add(invSpell);
                            break;

                        case 2:
                            inv = new InventoryWizform(number, dataId, WizformFile);
                            InventoryWizform invWizform = inv as InventoryWizform;

                            invWizform.Id1 = Read<int>("ID 1", 1);
                            invWizform.Level = Read<int>("Level", 1);
                            invWizform.Id2 = Read<int>("ID 2", 1);
                            Read<int>("-", 1); // 0
                            invWizform.Id3 = Read<int>("ID 3", 1);
                            invWizform.Experience = Read<int>("Experience", 1);

                            invWizform.LevelOfMagicA1 = new LevelOfMagic
                            {
                                Element1 = new Element(Read<byte>("Active Spell - Slot 1 - Element 1", 2)),
                                Element2 = new Element(Read<byte>("Active Spell - Slot 1 - Element 2", 2)),
                                Element3 = new Element(Read<byte>("Active Spell - Slot 1 - Element 3", 2)),
                                IsPassive = false
                            };

                            invWizform.LevelOfMagicP1 = new LevelOfMagic
                            {
                                Element1 = new Element(Read<byte>("Passive Spell - Slot 1 - Element 1", 2)),
                                Element2 = new Element(Read<byte>("Passive Spell - Slot 1 - Element 2", 2)),
                                Element3 = new Element(Read<byte>("Passive Spell - Slot 1 - Element 3", 2)),
                                IsPassive = true
                            };

                            invWizform.LevelOfMagicA2 = new LevelOfMagic
                            {
                                Element1 = new Element(Read<byte>("Active Spell - Slot 2 - Element 1", 2)),
                                Element2 = new Element(Read<byte>("Active Spell - Slot 2 - Element 2", 2)),
                                Element3 = new Element(Read<byte>("Active Spell - Slot 2 - Element 3", 2)),
                                IsPassive = false
                            };

                            invWizform.LevelOfMagicP2 = new LevelOfMagic
                            {
                                Element1 = new Element(Read<byte>("Passive Spell - Slot 2 - Element 1", 2)),
                                Element2 = new Element(Read<byte>("Passive Spell - Slot 2 - Element 2", 2)),
                                Element3 = new Element(Read<byte>("Passive Spell - Slot 2 - Element 3", 2)),
                                IsPassive = true
                            };

                            // spell order numbers
                            int a1 = Read<int>("Active Spell - Slot 1 - Order Number", 2);
                            int p1 = Read<int>("Passive Spell - Slot 1 - Order Number", 2);
                            int a2 = Read<int>("Active Spell - Slot 2 - Order Number", 2);
                            int p2 = Read<int>("Passive Spell - Slot 2 - Order Number", 2);

                            spellsOrderNumbers.Add(invWizform, new int[4] { a1, p1, a2, p2 });

                            invWizform.Position = Read<int>("Position", 1);
                            invWizform.Condition = Read<int>("Condition", 1);

                            foreach (int x in new int[5] { 0, 0, 0, 0, 0 }) Read<int>("-", 1);

                            invWizform.Id4 = Read<int>("ID 4", 1);
                            invWizform.HitPoints = Read<int>("Hit Points", 1);
                            invWizform.Name = Read<string>("Name", 1); ;

                            wizforms.Add(invWizform);
                            break;
                    }

                    inv.FileNumber = i;
                    inv.OrderNumber = orderNumber;

                }

                //--------------------------------------------------------------------------------

                // (Pixie in the Bag) = (Pixie Founded) - (Lucius' Pixie)
                vs2.Add(Read<int>("Pixie in the Bag"));
                vs2.Add(Read<int>("Pixie Founded"));

                int i49 = Read<int>("-"); // 49

                for (int h = 0; h < i49; h++)
                {
                    vs2.Add(Read<long>("?", 1));
                }

                // Min Attempts to Open a Chest
                vs2.Add(Read<int>("Min Attempts to Open a Chest"));
                EndRead();

                Items = items;
                Spells = spells;
                Wizforms = wizforms;

                foreach (var wiz in spellsOrderNumbers)
                {
                    int a1 = wiz.Value[0];
                    int p1 = wiz.Value[1];
                    int a2 = wiz.Value[2];
                    int p2 = wiz.Value[3];
                    wiz.Key.InventorySpellA1 =
                        a1 == -1 || Spells.Any(s => s.OrderNumber == a1) == false
                        ? null : Spells.Where(s => s.OrderNumber == a1).ToList()[0];
                    wiz.Key.InventorySpellP1 =
                        p1 == -1 || Spells.Any(s => s.OrderNumber == p1) == false
                        ? null : Spells.Where(s => s.OrderNumber == p1).ToList()[0];
                    wiz.Key.InventorySpellA2 =
                        a2 == -1 || Spells.Any(s => s.OrderNumber == a2) == false
                        ? null : Spells.Where(s => s.OrderNumber == a2).ToList()[0];
                    wiz.Key.InventorySpellP2 =
                        p2 == -1 || Spells.Any(s => s.OrderNumber == p2) == false
                        ? null : Spells.Where(s => s.OrderNumber == p2).ToList()[0];
                }

                List<InventoryItem> missing = new List<InventoryItem>();

                foreach (Item item in ItemFile.Items)
                    if (Items.Any(i => i.Item == item) == false)
                        missing.Add(new InventoryItem(item, ItemFile));

                if (missing.Count > 0)
                    Items = new ObservableCollection<InventoryItem>(Items.Concat(missing));
            }
            catch (Exception e)
            {
                AppSources.AccountWriteLine(e.Message);
            }
        }
        public override void Write(IProgress<string> progress)
        {
            ObservableCollection<InventoryObject> objects
                 = new ObservableCollection<InventoryObject>(new List<InventoryObject>().Concat(Items.Where(item => item.Available)).Concat(ActiveSpells).Concat(PassiveSpells).Concat(Wizforms));
            if (AppSources.Settings.SaveSorting == false)
            {
                objects = new ObservableCollection<InventoryObject>(objects.OrderBy(x => x.FileNumber));
            }
            for (int o = 0; o < objects.Count; o++) objects[o].OrderNumber = o;
            try
            {
                AppSources.AccountPath = "_save writing - account.txt";
                if (AppSources.Settings.SaveBackupCreating && File.Exists(FilePath))
                {
                    DateTime dt = AppSources.Time;
                    string time = $"{dt.Year}.{string.Format("{0:00}", dt.Month)}.{string.Format("{0:00}", dt.Day)}"
                        + $" - {string.Format("{0:00}", dt.Hour)}.{string.Format("{0:00}", dt.Minute)}.{string.Format("{0:00}", dt.Second)}.{string.Format("{0:000}", dt.Millisecond)}";
                    Directory.CreateDirectory($"Backup\\{time}\\Save\\");
                    File.Copy(FilePath, $"Backup\\{time}\\Save\\{FileName}");
                }
                BeginWrite();

                Write("Funatics", "Developers"); // Funatics

                int ind = 0;
                for (; ind < 4; ind++) Write((int)vs1[ind], $"Number {ind + 1}");

                Write((string)vs1[ind++], "Date");
                Write((string)vs1[ind++], "Time");
                Write((int)vs1[ind++], "Year");

                Write(0, "-");

                Write((string)vs1[ind++], "Save Name");
                Write((int)vs1[ind++], "Game Time, sec.");



                //--------------------------------------------------------------------------------

                Write((int)vs1[ind++], "Number of Locations");
                Write(8, "-"); // 8
                Write((int)vs1[ind++], "Current Location Number");
                Write((int)vs1[ind++], "Enter at");
                int locCount = (int)vs1[ind++];
                Write(locCount, "Location Count");

                for (int i = 0; i < locCount; i++)
                {
                    progress.Report($"Locations: {i + 1} / {locCount}");
                    AccountWriteLine($" ========= Locations: {i + 1} / {locCount}");

                    Write((string)vs1[ind++], "Location Name", 1);
                    int eventCount = (int)vs1[ind++];
                    Write(eventCount, "Event Count", 1);

                    for (int j = 0; j < eventCount; j++)
                    {
                        progress.Report($"Events: {j + 1} / {eventCount}");
                        AccountWriteLine($" ========= Events: {j + 1} / {eventCount}");

                        int eventType = (int)vs1[ind++];
                        Write(eventType, "Event Type", 2);
                        switch (eventType)
                        {
                            case 1:
                            case 3:
                            case 4:
                                Write((int)vs1[ind++], "< x1 >", 3);
                                break;
                            case 2:
                            case 6:
                                Write((int)vs1[ind++], "< x1 >", 3);
                                Write((int)vs1[ind++], "< x2 >", 3);
                                break;
                            default: break;
                        }
                    }
                }

                //---------------------------------------------------

                Write(objects.Count, "Inventory Object Count");
                for (int i = 0; i < objects.Count; i++)
                {
                    progress.Report($"Inventory Objects: {i + 1} / {objects.Count}");
                    AccountWriteLine($" ========= Inventory Objects: {i + 1} / {objects.Count}");

                    InventoryObject inv = objects[i];

                    Write<byte>(0, "-", 1);
                    switch(inv)
                    {
                        case InventoryItem _: Write<byte>(0, "Object Type (Item)", 1); break;
                        case InventorySpell _: Write<byte>(1, "Object Type (Spell)", 1); break;
                        case InventoryWizform _: Write<byte>(2, "Object Type (Wizform)", 1); break;
                    }
                    Write(inv.DataNumber, "Number", 1);
                    Write<byte>(0, "-", 1);

                    Write(inv.OrderNumber, "Order Number", 1);
                    Write(inv.DataId, "Data ID", 1);
                    if (inv is InventoryItem) Write((inv as InventoryItem).Quantity, "Quantity", 1);
                    else Write(1, "Quantity", 1);
                    Write(inv.Used, "Used", 1);

                    switch (inv)
                    {
                        case InventorySpell invSpell:
                             Write(invSpell.Id, "ID", 1);
                             Write(invSpell.Mana, "Mana", 1);
                             break;

                        case InventoryWizform invWizform:
                            Write(invWizform.Id1, "ID 1", 1);
                            Write(invWizform.Level, "Level", 1);
                            Write(invWizform.Id2, "ID 2", 1);
                            Write(0, "-", 1);
                            Write(invWizform.Id3, "ID 3", 1);
                            Write(invWizform.Experience, "Experience", 1);

                            Write(invWizform.LevelOfMagicA1.Element1.Number, "Active Spell - Slot 1 - Element 1", 2);
                            Write(invWizform.LevelOfMagicA1.Element2.Number, "Active Spell - Slot 1 - Element 2", 2);
                            Write(invWizform.LevelOfMagicA1.Element3.Number, "Active Spell - Slot 1 - Element 3", 2);

                            Write(invWizform.LevelOfMagicP1.Element1.Number, "Passive Spell - Slot 1 - Element 1", 2);
                            Write(invWizform.LevelOfMagicP1.Element2.Number, "Passive Spell - Slot 1 - Element 2", 2);
                            Write(invWizform.LevelOfMagicP1.Element3.Number, "Passive Spell - Slot 1 - Element 3", 2);

                            Write(invWizform.LevelOfMagicA2.Element1.Number, "Active Spell - Slot 2 - Element 1", 2);
                            Write(invWizform.LevelOfMagicA2.Element2.Number, "Active Spell - Slot 2 - Element 2", 2);
                            Write(invWizform.LevelOfMagicA2.Element3.Number, "Active Spell - Slot 2 - Element 3", 2);

                            Write(invWizform.LevelOfMagicP2.Element1.Number, "Passive Spell - Slot 2 - Element 1", 2);
                            Write(invWizform.LevelOfMagicP2.Element2.Number, "Passive Spell - Slot 2 - Element 2", 2);
                            Write(invWizform.LevelOfMagicP2.Element3.Number, "Passive Spell - Slot 2 - Element 3", 2);

                            // spell order numbers
                            if (invWizform.InventorySpellA1 == null) Write(-1, "Active Spell - Slot 1 - Order Number (null)", 2);
                            else Write(invWizform.InventorySpellA1.OrderNumber, "Active Spell - Slot 1 - Order Number", 2);
                            if (invWizform.InventorySpellP1 == null) Write(-1, "Passive Spell - Slot 1 - Order Number (null)", 2);
                            else Write(invWizform.InventorySpellP1.OrderNumber, "Passive Spell - Slot 1 - Order Number", 2);
                            if (invWizform.InventorySpellA2 == null) Write(-1, "Active Spell - Slot 2 - Order Number (null)", 2);
                            else Write(invWizform.InventorySpellA2.OrderNumber, "Active Spell - Slot 2 - Order Number", 2);
                            if (invWizform.InventorySpellP2 == null) Write(-1, "Passive Spell - Slot 2 - Order Number (null)", 2);
                            else Write(invWizform.InventorySpellP2.OrderNumber, "Passive Spell - Slot 2 - Order Number", 2);

                            Write(invWizform.Position, "Position", 1);
                            Write(invWizform.Condition, "Condition", 1);

                            foreach (int x in new int[5] { 0, 0, 0, 0, 0 }) Write(x, "-", 1);

                            Write(invWizform.Id4, "ID 4", 1);
                            Write(invWizform.HitPoints, "Hit Points", 1);
                            Write(invWizform.Name, "Name", 1);

                            break;
                    }
                }

                //--------------------------------------------------------------------------------

                ind = 0;

                // (Pixie in the Bag) = (Pixie Founded) - (Lucius' Pixie)
                Write((int)vs2[ind++], "Pixie in the Bag");
                Write((int)vs2[ind++], "Pixie Founded");

                Write(49, "-");

                for (int h = 0; h < 49; h++)
                    Write((long)vs2[ind++], "?", 1);

                // Min Attempts to Open a Chest
                Write((int)vs2[ind++], "Min Attempts to Open a Chest");
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
        public SaveFile(IProgress<string> progress, ItemFile itemFile, SpellFile spellFile, WizformFile wizformFile, string path) : base(progress, path)
        {
            ItemFile = itemFile;
            SpellFile = spellFile;
            WizformFile = wizformFile;
            Read(progress);
        }
    }
}
