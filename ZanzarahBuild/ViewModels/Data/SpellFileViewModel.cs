using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class SpellFileViewModel : ViewModelBase
    {
        private Spell _selected;
        private SpellFile _file;
        private ObservableCollection<SingleLevelOfMagic> _activeLevelOfMagicList;
        private ObservableCollection<SingleLevelOfMagic> _passiveLevelOfMagicList;
        // --------------------------------------------------------------------------------
        private RelayCommand _nameChangedCommand;
        // --------------------------------------------------------------------------------



        public Spell Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    ChangeHeader();
                    OnPropertyChanged();
                    OnPropertyChanged("LevelOfMagicList");
                }
            }
        }
        public void ChangeHeader()
        {
            if (Selected == null)
            {
                AppSources.Icon = null;
                AppSources.Title = null;
            }
            else
            {
                AppSources.Icon = Selected.Icon;
                AppSources.Title = Selected.Name;
            }
        }

        public SpellFile File
        {
            get { return _file; }
            set
            {
                if (_file != value)
                {
                    _file = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand NameChangedCommand
        {
            get
            {
                return _nameChangedCommand ??
                    (_nameChangedCommand = new RelayCommand(parameter =>
                    {
                        AppSources.Title = Selected.Name;
                    },
                    (parameter) => true));
            }
        }

        public ObservableCollection<SingleLevelOfMagic> ActiveLevelOfMagicList
        {
            get
            {
                if (_activeLevelOfMagicList == null)
                {
                    _activeLevelOfMagicList = new ObservableCollection<SingleLevelOfMagic>();
                    for (byte b = 0x0; b < 0xE; b++)
                        _activeLevelOfMagicList.Add(new SingleLevelOfMagic(new Element(b), false));
                }
                return _activeLevelOfMagicList;
            }
        }

        public ObservableCollection<SingleLevelOfMagic> PassiveLevelOfMagicList
        {
            get
            {
                if (_passiveLevelOfMagicList == null)
                {
                    _passiveLevelOfMagicList = new ObservableCollection<SingleLevelOfMagic>();
                    for (byte b = 0x0; b < 0xE; b++)
                        _passiveLevelOfMagicList.Add(new SingleLevelOfMagic(new Element(b), true));
                }
                return _passiveLevelOfMagicList;
            }
        }

        public string Number_Label
        {
            get { return AppSources.GetLabel("Number"); }
        }
        public string Name_Label
        {
            get { return AppSources.GetLabel("Name"); }
        }
        public string Description_Label
        {
            get { return AppSources.GetLabel("Description"); }
        }
        public string Type_Label
        {
            get { return AppSources.GetLabel("Type"); }
        }
        public string Active_Label
        {
            get { return AppSources.GetLabel("Active"); }
        }
        public string Passive_Label
        {
            get { return AppSources.GetLabel("Passive"); }
        }
        public string Mana_Label
        {
            get { return AppSources.GetLabel("Mana"); }
        }
        public string Damage_Label
        {
            get { return AppSources.GetLabel("Damage"); }
        }
        public string FireRate_Label
        {
            get { return AppSources.GetLabel("Fire Rate"); }
        }
        public string Level_Label
        {
            get { return AppSources.GetLabel("Level"); }
        }
        public string Level1_Label
        {
            get { return AppSources.GetLabel("Level") + " 1"; }
        }
        public string Level2_Label
        {
            get { return AppSources.GetLabel("Level") + " 2"; }
        }
        public string Level3_Label
        {
            get { return AppSources.GetLabel("Level") + " 3"; }
        }
    }
}
