using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
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
        private Visibility _wizformFile_Visibility;
        private Visibility _textFile_Visibility;
        private Visibility _spellFile_Visibility;
        private Visibility _itemFile_Visibility;
        private Visibility _scriptFile_Visibility;
        private Visibility _dialogueFile_Visibility;
        private Visibility _saveFileItems_Visibility;
        private Visibility _saveFileSpells_Visibility;
        private Visibility _saveFileWizforms_Visibility;
        private Visibility _addSpells_Visibility;
        private Visibility _addWizforms_Visibility;


        private Visibility _menu_Visibility;

        public Visibility WizformFile_Visibility
        {
            get { return _wizformFile_Visibility; }
            set
            {
                if (_wizformFile_Visibility != value)
                {
                    _wizformFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility TextFile_Visibility
        {
            get { return _textFile_Visibility; }
            set
            {
                if (_textFile_Visibility != value)
                {
                    _textFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility SpellFile_Visibility
        {
            get { return _spellFile_Visibility; }
            set
            {
                if (_spellFile_Visibility != value)
                {
                    _spellFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility ItemFile_Visibility
        {
            get { return _itemFile_Visibility; }
            set
            {
                if (_itemFile_Visibility != value)
                {
                    _itemFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility ScriptFile_Visibility
        {
            get { return _scriptFile_Visibility; }
            set
            {
                if (_scriptFile_Visibility != value)
                {
                    _scriptFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility DialogueFile_Visibility
        {
            get { return _dialogueFile_Visibility; }
            set
            {
                if (_dialogueFile_Visibility != value)
                {
                    _dialogueFile_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility SaveFileItems_Visibility
        {
            get { return _saveFileItems_Visibility; }
            set
            {
                if (_saveFileItems_Visibility != value)
                {
                    _saveFileItems_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility SaveFileSpells_Visibility
        {
            get { return _saveFileSpells_Visibility; }
            set
            {
                if (_saveFileSpells_Visibility != value)
                {
                    _saveFileSpells_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility SaveFileWizforms_Visibility
        {
            get { return _saveFileWizforms_Visibility; }
            set
            {
                if (_saveFileWizforms_Visibility != value)
                {
                    _saveFileWizforms_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility AddSpells_Visibility
        {
            get { return _addSpells_Visibility; }
            set
            {
                if (_addSpells_Visibility != value)
                {
                    _addSpells_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility AddWizforms_Visibility
        {
            get { return _addWizforms_Visibility; }
            set
            {
                if (_addWizforms_Visibility != value)
                {
                    _addWizforms_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility Menu_Visibility
        {
            get { return _menu_Visibility; }
            set
            {
                if (_menu_Visibility != value)
                {
                    _menu_Visibility = value;
                    OnPropertyChanged();
                }
            }
        }

        //   === === === === === === === === === === === === === === === === === ===
        public void CollapseAllViews()
        {
            WizformFile_Visibility = Visibility.Collapsed;
            TextFile_Visibility = Visibility.Collapsed;
            SpellFile_Visibility = Visibility.Collapsed;
            ItemFile_Visibility = Visibility.Collapsed;
            ScriptFile_Visibility = Visibility.Collapsed;
            DialogueFile_Visibility = Visibility.Collapsed;
            SaveFileItems_Visibility = Visibility.Collapsed;
            SaveFileSpells_Visibility = Visibility.Collapsed;
            SaveFileWizforms_Visibility = Visibility.Collapsed;
            AddSpells_Visibility = Visibility.Collapsed;
            AddWizforms_Visibility = Visibility.Collapsed;
        }
    }
}
