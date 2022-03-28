using System;
using System.Linq;
using System.Windows;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public class InventoryItem : InventoryObject
    {
        private bool _available;
        private int _quantity;

        public Item Item { get; }
        public override int DataId
        {
            get { return Item.Id; }
        }
        public override byte DataNumber
        {
            get { return (byte)Item.Number; }
        }
        [Obsolete("Item has no identifiers.", true)]
        public new int Id
        {
            get { throw new NotSupportedException("Item has no identifiers."); }
            set { throw new NotSupportedException("Item has no identifiers."); }
        }
        public bool Available
        {
            get { return _available; }
            set
            {
                if (_available != value)
                {
                    _available = value;
                    if (value == true && Quantity == 0)
                        Quantity = 1;
                    else if (value == false)
                        Quantity = 0;
                    OnPropertyChanged();
                }
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    Available = Quantity > 0;
                    OnPropertyChanged();
                }
            }
        }
        public override bool Used
        {
            get { return false; }
        }

        public InventoryItem(ItemFile file) : this(-1, -1, file) { }

        public InventoryItem(Item item, ItemFile file) : this(item.Number, item.Id, file) { }

        public InventoryItem(int number, int id, ItemFile file)
        {
            if (file != null && file.Items.Any(i => i.Id == id) == false)
            {
                throw new NullReferenceException($"There is no item with ID {id}");
            }
            if (file != null) Item = file.Items.Where(i => i.Id == id).ToList()[0];
        }
    }
}
