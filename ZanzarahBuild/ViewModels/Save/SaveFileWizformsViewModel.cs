using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class SaveFileWizformsViewModel : ViewModelBase
    {
        private InventoryWizform _selected;
        private SaveFile _file;
        // --------------------------------------------------------------------------------
        private RelayCommand _nameChangedCommand;
        private RelayCommand _refreshSpellsCommand;
        // --------------------------------------------------------------------------------


        public InventoryWizform Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    ChangeHeader();
                    OnPropertyChanged();
                    OnPropertyChanged("ActiveSpells1");
                    OnPropertyChanged("PassiveSpells1");
                    OnPropertyChanged("ActiveSpells2");
                    OnPropertyChanged("PassiveSpells2");
                }
            }
        }
        public void ChangeHeader()
        {
            if (Selected == null || Selected.Wizform == null)
            {
                AppSources.Icon = null;
                AppSources.Title = null;
            }
            else
            {
                AppSources.Icon = Selected.Wizform.Icon;
                AppSources.Title = Selected.Name;
            }
        }

        public SaveFile File
        {
            get
            {
                return _file;
            }
        }

        public RelayCommand NameChangedCommand
        {
            get
            {
                return _nameChangedCommand ??
                    (_nameChangedCommand = new RelayCommand(parameter =>
                    {
                        if (Selected != null) AppSources.Title = Selected.Name;
                    },
                    (parameter) => true));
            }
        }

        public ObservableCollection<InventorySpell> ActiveSpells1
        {
            get
            {
                return new ObservableCollection<InventorySpell>(new List<InventorySpell> { InventorySpell.NullableSpell }
                .Concat(File.ActiveSpells.Where(s => s == Selected?.InventorySpellA1 || s.Used == false)));
            }
        }

        public ObservableCollection<InventorySpell> PassiveSpells1
        {
            get
            {
                return new ObservableCollection<InventorySpell>(new List<InventorySpell> { InventorySpell.NullableSpell }
                .Concat(File.PassiveSpells.Where(s => s == Selected?.InventorySpellP1 || s.Used == false)));
            }
        }

        public ObservableCollection<InventorySpell> ActiveSpells2
        {
            get
            {
                return new ObservableCollection<InventorySpell>(new List<InventorySpell> { InventorySpell.NullableSpell }
                .Concat(File.ActiveSpells.Where(s => s == Selected?.InventorySpellA2 || s.Used == false)));
            }
        }

        public ObservableCollection<InventorySpell> PassiveSpells2
        {
            get
            {
                return new ObservableCollection<InventorySpell>(new List<InventorySpell> { InventorySpell.NullableSpell }
                .Concat(File.PassiveSpells.Where(s => s == Selected?.InventorySpellP2 || s.Used == false)));
            }
        }

        public CroppedBitmap NullableActiveSpellIcon
        {
            get
            {
                return AppSources.GetNullableSpellIcon(false);
            }
        }

        public CroppedBitmap NullablePassiveSpellIcon
        {
            get
            {
                return AppSources.GetNullableSpellIcon(true);
            }
        }

        public List<Condition> ConditionList
        {
            get
            {
                return Condition.GetConditionList();
            }
        }

        public RelayCommand RefreshSpellsCommand
        {
            get
            {
                return _refreshSpellsCommand ??
                    (_refreshSpellsCommand = new RelayCommand(parameter =>
                    {
                        OnPropertyChanged("ActiveSpells1");
                        OnPropertyChanged("PassiveSpells1");
                        OnPropertyChanged("ActiveSpells2");
                        OnPropertyChanged("PassiveSpells2");
                        if (Selected != null) Selected.OnPropertyChanged("Selected");
                    },
                    (parameter) => true));
            }
            set { _refreshSpellsCommand = value; }
        }

        public string Spells_Label
        {
            get { return AppSources.GetLabel("Spells"); }
        }
        public string LevelOfMagicA1_Label
        {
            get { return $"{AppSources.GetLabel("Level of Magic")} - {AppSources.GetLabel("Active")} 1"; }
        }
        public string LevelOfMagicP1_Label
        {
            get { return $"{AppSources.GetLabel("Level of Magic")} - {AppSources.GetLabel("Passive")} 1"; }
        }
        public string LevelOfMagicA2_Label
        {
            get { return $"{AppSources.GetLabel("Level of Magic")} - {AppSources.GetLabel("Active")} 2"; }
        }
        public string LevelOfMagicP2_Label
        {
            get { return $"{AppSources.GetLabel("Level of Magic")} - {AppSources.GetLabel("Passive")} 2"; }
        }
        public string Name_Label
        {
            get { return AppSources.GetLabel("Fairy Name"); }
        }
        public string Condition_Label
        {
            get { return AppSources.GetLabel("Condition"); }
        }
        public string Level_Label
        {
            get { return AppSources.GetLabel("Level"); }
        }
        public string Experience_Label
        {
            get { return AppSources.GetLabel("Experience"); }
        }
        public string HitPoints_Label
        {
            get { return AppSources.GetLabel("Hit Points"); }
        }

        public void RemoveWizformsFromInventory()
        {
            if (File.Wizforms.Where(w => w.IsSelected).Count() <= 0) return;
            var selected = File.Wizforms.Where(w => w.IsSelected).ToList();
            foreach (var sel in selected)
            {
                sel.InventorySpellA1 = null;
                sel.InventorySpellP1 = null;
                sel.InventorySpellA2 = null;
                sel.InventorySpellP2 = null;
            }
            File.Wizforms = new ObservableCollection<InventoryWizform>(File.Wizforms.Where(w => w.IsSelected == false).ToList());
        }

        public SaveFileWizformsViewModel(SaveFile file)
        {
            _file = file;
            OnPropertyChanged("File");
        }
    }
}
