using System;
using System.Linq;
using System.Windows;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public class InventorySpell : InventoryObject
    {
        private InventoryWizform _owner;
        private int _id;
        private int _mana;
        private static InventorySpell _nullableSpell;

        public Spell Spell { get; }
        public override int DataId
        {
            get { return Spell.Id; }
        }
        public override byte DataNumber
        {
            get { return (byte)Spell.Number; }
        }
        public InventoryWizform Owner
        {
            get { return _owner; }
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }
        public new int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Mana
        {
            get { return _mana; }
            set
            {
                if (_mana != value)
                {
                    _mana = value;
                    OnPropertyChanged();
                }
            }
        }
        public override bool Used
        {
            get { return Owner != null; }
        }

        public static InventorySpell NullableSpell
        {
            get
            {
                if (_nullableSpell == null)
                    _nullableSpell = new InventorySpell(null);
                return _nullableSpell;
            }
        }

        public static InventorySpell GetSomeSpell(Element element, SpellFile spellFile)
        {
            foreach(var spell in spellFile.Spells)
            {
                if (spell.LevelOfMagic.Element1 == element && spell.IsPassive == false)
                {
                    var sp = new InventorySpell(spell, spellFile);
                    sp.InitSpell();
                    return sp;
                }
            }
            return null;
        }

        public void InitSpell()
        {
            Id = new Random().Next(1, short.MaxValue + 1);
            Mana = Spell.ConvertMana(Spell.Mana);
        }

        public InventorySpell(SpellFile file) : this(-1, -1, file) { }

        public InventorySpell(Spell spell, SpellFile file) : this(spell.Number, spell.Id, file) { }

        public InventorySpell(int number, int id, SpellFile file)
        {
            if (file != null && file.Spells.Any(s => s.Id == id) == false)
            {
                throw new NullReferenceException($"There is no spell with ID {id}");
            }
            if (file != null) Spell = file.Spells.Where(s => s.Id == id).ToList()[0];
        }
    }
}
