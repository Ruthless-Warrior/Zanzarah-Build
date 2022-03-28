using System;
using System.Linq;
using System.Windows;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public class InventoryWizform : InventoryObject
    {
        public const int XP_MAX = 15_000;
        private int _id1;
        private int _id2;
        private int _id3;
        private int _id4;
        private int _level;
        private int _experience;
        private int _condition;
        private int _hitPoints;
        private string _name;
        private int _position;
        private LevelOfMagic _levelOfMagicA1;
        private LevelOfMagic _levelOfMagicP1;
        private LevelOfMagic _levelOfMagicA2;
        private LevelOfMagic _levelOfMagicP2;
        private InventorySpell _inventorySpellA1;
        private InventorySpell _inventorySpellP1;
        private InventorySpell _inventorySpellA2;
        private InventorySpell _inventorySpellP2;

        public Wizform Wizform { get; }
        public override int DataId
        {
            get { return Wizform.Id; }
        }
        public override byte DataNumber
        {
            get { return (byte)Wizform.Number; }
        }
        [Obsolete("Wizform has its own identifiers.", true)]
        public new int Id
        {
            get { throw new NotSupportedException("Wizform has its own identifiers."); }
            set { throw new NotSupportedException("Wizform has its own identifiers."); }
        }
        public int Id1
        {
            get { return _id1; }
            set
            {
                if (_id1 != value)
                {
                    _id1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id2
        {
            get { return _id2; }
            set
            {
                if (_id2 != value)
                {
                    _id2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id3
        {
            get { return _id3; }
            set
            {
                if (_id3 != value)
                {
                    _id3 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id4
        {
            get { return _id4; }
            set
            {
                if (_id4 != value)
                {
                    _id4 = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    int hmax = MaxHitPoints;
                    _level = value;
                    OnPropertyChanged();
                    OnPropertyChanged("MaxHitPoints");
                    int h = HitPoints;
                    HitPoints = 0;
                    HitPoints = 100000;
                    if (hmax == h) HitPoints = MaxHitPoints;
                    else HitPoints = h;
                    OnPropertyChanged("HitPoints");
                    OnPropertyChanged("XpLevelUp");
                    OnPropertyChanged("XpMin");
                    OnPropertyChanged("XpMax");
                    int x = Experience;
                    Experience = 0;
                    Experience = XP_MAX;
                    Experience = x;
                    OnPropertyChanged("Experience");
                }
            }
        }
        public int Experience
        {
            get { return _experience; }
            set
            {
                if (_experience != value)
                {
                    _experience = value;
                    if (_experience < XpMin) _experience = XpMin;
                    else if (_experience > XpMax) _experience = XpMax;

                    OnPropertyChanged();
                }
            }
        }
        public int XpLevelUp
        {
            get
            {
                if (Wizform == null) return 0;
                if (Level == 60) return XP_MAX;
                return XpMax + 1;
            }
        }
        public int XpMin
        {
            get
            {
                if (Wizform == null)
                {
                    Experience = 0;
                    return 0;
                }
                if (Level == 60)
                {
                    Experience = XP_MAX;
                    return XP_MAX;
                }
                int c = Wizform.ExperienceModifier;
                int lvl = Level;
                return GetMaxExp(lvl, c);
            }
        }
        public int XpMax
        {
            get
            {
                if (Wizform == null)
                {
                    Experience = 0;
                    return 0;
                }
                if (Level == 60)
                {
                    Experience = XP_MAX;
                    return XP_MAX;
                }
                int c = Wizform.ExperienceModifier;
                int lvl = Level + 1;
                return GetMaxExp(lvl, c) - 1;
            }
        }
        public int Condition
        {
            get { return _condition; }
            set
            {
                if (_condition != value)
                {
                    _condition = value;
                    OnPropertyChanged();
                }
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                if (_hitPoints != value)
                {
                    _hitPoints = value;
                    if (_hitPoints > MaxHitPoints)
                        _hitPoints = MaxHitPoints;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxHitPoints
        {
            get
            {
                if (Wizform == null) return 0;
                int hp = Wizform.HitPoints;
                int lvl = Level;
                int h = hp / 10;
                return 3 * lvl * hp / 200 + h;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }
        public LevelOfMagic LevelOfMagicA1
        {
            get { return _levelOfMagicA1; }
            set
            {
                if (_levelOfMagicA1 != value)
                {
                    _levelOfMagicA1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public LevelOfMagic LevelOfMagicP1
        {
            get { return _levelOfMagicP1; }
            set
            {
                if (_levelOfMagicP1 != value)
                {
                    _levelOfMagicP1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public LevelOfMagic LevelOfMagicA2
        {
            get { return _levelOfMagicA2; }
            set
            {
                if (_levelOfMagicA2 != value)
                {
                    _levelOfMagicA2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public LevelOfMagic LevelOfMagicP2
        {
            get { return _levelOfMagicP2; }
            set
            {
                if (_levelOfMagicP2 != value)
                {
                    _levelOfMagicP2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public InventorySpell InventorySpellA1
        {
            get { return _inventorySpellA1; }
            set
            {
                if (_inventorySpellA1 != value)
                {
                    var spell = value;
                    if (spell != null && spell.Owner != null)
                    {
                        if (spell.Owner.InventorySpellA1 == spell) spell.Owner.InventorySpellA1 = null;
                        if (spell.Owner.InventorySpellP1 == spell) spell.Owner.InventorySpellP1 = null;
                        if (spell.Owner.InventorySpellA2 == spell) spell.Owner.InventorySpellA2 = null;
                        if (spell.Owner.InventorySpellP2 == spell) spell.Owner.InventorySpellP2 = null;
                    }
                    if (InventorySpellA1 != null) InventorySpellA1.Owner = null;
                    _inventorySpellA1 = spell;
                    if (InventorySpellA1 != null) InventorySpellA1.Owner = this;
                    OnPropertyChanged();
                }
            }
        }
        public InventorySpell InventorySpellP1
        {
            get { return _inventorySpellP1; }
            set
            {
                if (_inventorySpellP1 != value)
                {
                    var spell = value;
                    if (spell != null && spell.Owner != null)
                    {
                        if (spell.Owner.InventorySpellA1 == spell) spell.Owner.InventorySpellA1 = null;
                        if (spell.Owner.InventorySpellP1 == spell) spell.Owner.InventorySpellP1 = null;
                        if (spell.Owner.InventorySpellA2 == spell) spell.Owner.InventorySpellA2 = null;
                        if (spell.Owner.InventorySpellP2 == spell) spell.Owner.InventorySpellP2 = null;
                    }
                    if (InventorySpellP1 != null) InventorySpellP1.Owner = null;
                    _inventorySpellP1 = spell;
                    if (InventorySpellP1 != null) InventorySpellP1.Owner = this;
                    OnPropertyChanged();
                }
            }
        }
        public InventorySpell InventorySpellA2
        {
            get { return _inventorySpellA2; }
            set
            {
                if (_inventorySpellA2 != value)
                {
                    var spell = value;
                    if (spell != null && spell.Owner != null)
                    {
                        if (spell.Owner.InventorySpellA1 == spell) spell.Owner.InventorySpellA1 = null;
                        if (spell.Owner.InventorySpellP1 == spell) spell.Owner.InventorySpellP1 = null;
                        if (spell.Owner.InventorySpellA2 == spell) spell.Owner.InventorySpellA2 = null;
                        if (spell.Owner.InventorySpellP2 == spell) spell.Owner.InventorySpellP2 = null;
                    }
                    if (InventorySpellA2 != null) InventorySpellA2.Owner = null;
                    _inventorySpellA2 = spell;
                    if (InventorySpellA2 != null) InventorySpellA2.Owner = this;
                    OnPropertyChanged();
                }
            }
        }
        public InventorySpell InventorySpellP2
        {
            get { return _inventorySpellP2; }
            set
            {
                if (_inventorySpellP2 != value)
                {
                    var spell = value;
                    if (spell != null && spell.Owner != null)
                    {
                        if (spell.Owner.InventorySpellA1 == spell) spell.Owner.InventorySpellA1 = null;
                        if (spell.Owner.InventorySpellP1 == spell) spell.Owner.InventorySpellP1 = null;
                        if (spell.Owner.InventorySpellA2 == spell) spell.Owner.InventorySpellA2 = null;
                        if (spell.Owner.InventorySpellP2 == spell) spell.Owner.InventorySpellP2 = null;
                    }
                    if (InventorySpellP2 != null) InventorySpellP2.Owner = null;
                    _inventorySpellP2 = spell;
                    if (InventorySpellP2 != null) InventorySpellP2.Owner = this;
                    OnPropertyChanged();
                }
            }
        }
        public override bool Used
        {
            get { return Position != -1; }
        }

        public int GetMaxExp(int lvl, int c)
        {
            return (int)Math.Ceiling(XP_MAX * Math.Pow((double)lvl / 60, 1000.0 / c));
        }

        public void InitFairy(int level, SpellFile spellFile, out InventorySpell spell)
        {
            Random rnd = new Random();
            Id1 = rnd.Next(1, short.MaxValue + 1);
            Id2 = rnd.Next(1, short.MaxValue + 1);
            Id3 = rnd.Next(1, short.MaxValue + 1);
            Id4 = rnd.Next(1, short.MaxValue + 1);
            Level = level;
            LevelOfMagic a1, p1, a2, p2;
            Wizform.GetMagic(Level, out a1, out p1, out a2, out p2);
            LevelOfMagicA1 = a1;
            LevelOfMagicP1 = p1;
            LevelOfMagicA2 = a2;
            LevelOfMagicP2 = p2;
            HitPoints = MaxHitPoints;
            Experience = XpMax;
            Name = Wizform.Name;
            Position = -1;
            Condition = 0;

            spell = InventorySpell.GetSomeSpell(Wizform.Element.Element, spellFile);

            spell.Owner = this;
            InventorySpellA1 = spell;
        }

        public InventoryWizform(Wizform wizform, WizformFile file)
            : this(wizform.Number, wizform.Id, file) { }

        public InventoryWizform(int number, int id, WizformFile file)
        {
            if (file != null && file.Wizforms.Any(w => w.Id == id) == false)
            {
                throw new NullReferenceException($"There is no wizform with ID {id}");
            }
            if (file != null) Wizform = file.Wizforms.Where(w => w.Id == id).ToList()[0];
        }
    }
}
