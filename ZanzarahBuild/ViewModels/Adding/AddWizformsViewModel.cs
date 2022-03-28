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
    public class AddWizformsViewModel : ViewModelBase
    {
        private SaveFile _file;
        private int _quantity = 1;
        private int _level = 0;
        private ObservableCollection<InventoryWizform> _buffer = new ObservableCollection<InventoryWizform>();
        private RelayCommand _addCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;
        private List<InventorySpell> _spells = new List<InventorySpell>();

        public SaveFile File
        {
            get { return _file; }
        }
        public ObservableCollection<Wizform> Wizforms
        {
            get { return File.WizformFile.Wizforms; }
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
        public int Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<InventoryWizform> Buffer
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
                        foreach (Wizform wizform in File.WizformFile.Wizforms.Where(w => w.IsSelected))
                            for (int i = 0; i < Quantity; i++)
                            {
                                InventoryWizform wiz = new InventoryWizform(wizform, File.WizformFile);
                                wiz.InitFairy(Level, AppSources.CurrentSpellFile, out InventorySpell spell);
                                Buffer.Add(wiz);
                                _spells.Add(spell);
                            }
                        Buffer = new ObservableCollection<InventoryWizform>(Buffer
                            .OrderBy(wizform => wizform.Wizform.Number)
                            .ThenByDescending(wizform => wizform.Level));
                    },
                    (parameter) => File.WizformFile.Wizforms.Where(wizform => wizform.IsSelected).Count() > 0));
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
                        var wizforms = new ObservableCollection<InventoryWizform>(Buffer.Where(w => w.IsSelected));
                        foreach (InventoryWizform wizform in wizforms) Buffer.Remove(wizform);
                    },
                    (parameter) => Buffer.Where(wizform => wizform.IsSelected).Count() > 0));
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
                        File.Wizforms = new ObservableCollection<InventoryWizform>(File.Wizforms.Concat(Buffer));
                        Buffer.Clear();
                        File.Spells = new ObservableCollection<InventorySpell>(File.Spells.Concat(_spells));
                        _spells.Clear();
                        AppSources.MainWindow_ViewModel.CurrentForm = CurrentForm.InventoryWizforms;
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
                        AppSources.MainWindow_ViewModel.CurrentForm = CurrentForm.InventoryWizforms;
                        AppSources.MainWindow_ViewModel.Menu_Visibility = Visibility.Visible;
                    },
                    (parameter) => true));
            }
            set { _cancelCommand = value; }
        }

        public string Wizforms_Label
        {
            get { return AppSources.GetLabel("Wizforms"); }
        }

        public string Quantity_Label
        {
            get { return AppSources.GetLabel("Quantity"); }
        }

        public string Level_Label
        {
            get { return AppSources.GetLabel("Level"); }
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

        public AddWizformsViewModel(SaveFile file)
        {
            _file = file;
        }
    }
}
