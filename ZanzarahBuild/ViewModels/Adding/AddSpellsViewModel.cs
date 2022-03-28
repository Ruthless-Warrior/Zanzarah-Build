using Common.Wpf.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class AddSpellsViewModel : ViewModelBase
    {
        private SaveFile _file;
        private int _quantity = 1;
        private ObservableCollection<InventorySpell> _buffer = new ObservableCollection<InventorySpell>();
        private RelayCommand _addCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;

        public SaveFile File
        {
            get { return _file; }
        }
        public ObservableCollection<Spell> Spells
        {
            get { return File.SpellFile.Spells; }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<InventorySpell> Buffer
        {
            get { return _buffer; }
            set
            {
                if (_buffer != value)
                {
                    _buffer = value;
                    OnPropertyChanged();
                }
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(parameter =>
                    {
                        foreach (Spell spell in File.SpellFile.Spells.Where(s => s.IsSelected))
                            for (int i = 0; i < Quantity; i++)
                            {
                                InventorySpell sp = new InventorySpell(spell, File.SpellFile);
                                sp.InitSpell();
                                Buffer.Add(sp);
                            }

                        Buffer = new ObservableCollection<InventorySpell>(Buffer
                            .OrderBy(spell => spell.Spell.Number));
                    },
                    (parameter) => File.SpellFile.Spells.Where(spell => spell.IsSelected).Count() > 0));
            }
            set { _addCommand = value; }
        }
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(parameter =>
                    {
                        var spells = new ObservableCollection<InventorySpell>(Buffer.Where(s => s.IsSelected));
                        foreach (InventorySpell spell in spells) Buffer.Remove(spell);
                    },
                    (parameter) => Buffer.Where(spell => spell.IsSelected).Count() > 0));
            }
            set { _removeCommand = value; }
        }
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??
                    (_okCommand = new RelayCommand(parameter =>
                    {
                        File.Spells = new ObservableCollection<InventorySpell>(File.Spells.Concat(Buffer));
                        Buffer.Clear();
                        AppSources.MainWindow_ViewModel.CurrentForm = CurrentForm.InventorySpells;
                        AppSources.MainWindow_ViewModel.Menu_Visibility = Visibility.Visible;
                    },
                    (parameter) => Buffer.Count > 0));
            }
            set { _okCommand = value; }
        }
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(parameter =>
                    {
                        Buffer.Clear();
                        AppSources.MainWindow_ViewModel.CurrentForm = CurrentForm.InventorySpells;
                        AppSources.MainWindow_ViewModel.Menu_Visibility = Visibility.Visible;
                    },
                    (parameter) => true));
            }
            set { _cancelCommand = value; }
        }

        public string Spells_Label
        {
            get { return AppSources.GetLabel("Spells"); }
        }

        public string Quantity_Label
        {
            get { return AppSources.GetLabel("Quantity"); }
        }

        public string Add_Label
        {
            get { return AppSources.GetLabel("Add"); }
        }

        public string Remove_Label
        {
            get { return AppSources.GetLabel("Remove"); }
        }

        public string Ok_Label
        {
            get { return AppSources.GetLabel("Ok"); }
        }

        public string Cancel_Label
        {
            get { return AppSources.GetLabel("Cancel"); }
        }

        public AddSpellsViewModel(SaveFile file)
        {
            _file = file;
        }
    }
}
