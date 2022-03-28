using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZanzarahBuild.ViewModels
{
    public partial class MainWindowViewModel : WindowViewModelBase
    {
        private WizformFileViewModel _wizformFile_ViewModel;
        private TextFileViewModel _textFile_ViewModel;
        private SpellFileViewModel _spellFile_ViewModel;
        private ItemFileViewModel _itemFile_ViewModel;
        private ScriptFileViewModel _scriptFile_ViewModel;
        private DialogueFileViewModel _dialogueFile_ViewModel;
        private SaveFileItemsViewModel _saveFileItems_ViewModel;
        private SaveFileSpellsViewModel _saveFileSpells_ViewModel;
        private SaveFileWizformsViewModel _saveFileWizforms_ViewModel;
        private AddSpellsViewModel _addSpells_ViewModel;
        private AddWizformsViewModel _addWizforms_ViewModel;

        public WizformFileViewModel WizformFile_ViewModel
        {
            get
            {
                return _wizformFile_ViewModel;
            }
            set
            {
                if (_wizformFile_ViewModel != value)
                {
                    _wizformFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public TextFileViewModel TextFile_ViewModel
        {
            get
            {
                return _textFile_ViewModel;
            }
            set
            {
                if (_textFile_ViewModel != value)
                {
                    _textFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public SpellFileViewModel SpellFile_ViewModel
        {
            get
            {
                return _spellFile_ViewModel;
            }
            set
            {
                if (_spellFile_ViewModel != value)
                {
                    _spellFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public ItemFileViewModel ItemFile_ViewModel
        {
            get
            {
                return _itemFile_ViewModel;
            }
            set
            {
                if (_itemFile_ViewModel != value)
                {
                    _itemFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public ScriptFileViewModel ScriptFile_ViewModel
        {
            get
            {
                return _scriptFile_ViewModel;
            }
            set
            {
                if (_scriptFile_ViewModel != value)
                {
                    _scriptFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public DialogueFileViewModel DialogueFile_ViewModel
        {
            get
            {
                return _dialogueFile_ViewModel;
            }
            set
            {
                if (_dialogueFile_ViewModel != value)
                {
                    _dialogueFile_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public SaveFileItemsViewModel SaveFileItems_ViewModel
        {
            get
            {
                return _saveFileItems_ViewModel;
            }
            set
            {
                if (_saveFileItems_ViewModel != value)
                {
                    _saveFileItems_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public SaveFileSpellsViewModel SaveFileSpells_ViewModel
        {
            get
            {
                return _saveFileSpells_ViewModel;
            }
            set
            {
                if (_saveFileSpells_ViewModel != value)
                {
                    _saveFileSpells_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public SaveFileWizformsViewModel SaveFileWizforms_ViewModel
        {
            get
            {
                return _saveFileWizforms_ViewModel;
            }
            set
            {
                if (_saveFileWizforms_ViewModel != value)
                {
                    _saveFileWizforms_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public AddSpellsViewModel AddSpells_ViewModel
        {
            get
            {
                return _addSpells_ViewModel;
            }
            set
            {
                if (_addSpells_ViewModel != value)
                {
                    _addSpells_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public AddWizformsViewModel AddWizforms_ViewModel
        {
            get
            {
                return _addWizforms_ViewModel;
            }
            set
            {
                if (_addWizforms_ViewModel != value)
                {
                    _addWizforms_ViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
