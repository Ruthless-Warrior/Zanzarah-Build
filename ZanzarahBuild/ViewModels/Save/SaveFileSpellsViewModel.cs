using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class SaveFileSpellsViewModel : ViewModelBase
    {
        private InventorySpell _selected;
        private SaveFile _file;

        public InventorySpell Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    ChangeHeader();
                    OnPropertyChanged();
                }
            }
        }
        public void ChangeHeader()
        {
            if (Selected == null || Selected.Spell == null)
            {
                AppSources.Icon = null;
                AppSources.Title = null;
            }
            else
            {
                AppSources.Icon = Selected.Spell.Icon;
                AppSources.Title = Selected.Spell.Name;
            }
        }
        public SaveFile File
        {
            get
            {
                return _file;
            }
        }


        public string Owner_Label
        {
            get { return AppSources.GetLabel("Owner"); }
        }
        public string Name_Label
        {
            get { return AppSources.GetLabel("Name"); }
        }
        public string Mana_Label
        {
            get { return AppSources.GetLabel("Mana"); }
        }

        public void RemoveSpellsFromInventory()
        {
            if (File.Spells.Where(s => s.IsSelected).Count() <= 0) return;
            var selected = File.Spells.Where(s => s.IsSelected).ToList();
            foreach (var sel in selected)
            {
                if (sel.Owner != null && sel.Owner.InventorySpellA1 == sel) sel.Owner.InventorySpellA1 = null;
                if (sel.Owner != null && sel.Owner.InventorySpellP1 == sel) sel.Owner.InventorySpellP1 = null;
                if (sel.Owner != null && sel.Owner.InventorySpellA2 == sel) sel.Owner.InventorySpellA2 = null;
                if (sel.Owner != null && sel.Owner.InventorySpellP2 == sel) sel.Owner.InventorySpellP2 = null;
                sel.Owner = null;
            }
            File.Spells = new ObservableCollection<InventorySpell>(File.Spells.Where(s => s.IsSelected == false).ToList());
        }

        public SaveFileSpellsViewModel(SaveFile file)
        {
            _file = file;
            OnPropertyChanged("File");
        }
    }
}
