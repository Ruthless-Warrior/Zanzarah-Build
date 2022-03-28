using System.Collections.ObjectModel;

namespace ZanzarahBuild.Models.Data
{
    public abstract class InventoryObject : Entity
    {
        public int OrderNumber { get; set; }
        public abstract int DataId { get; }
        public abstract byte DataNumber { get; }
        public abstract bool Used { get; }
    }
}
