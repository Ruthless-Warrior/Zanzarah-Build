using Common.Wpf.Mvvm;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public partial class MainWindowViewModel : WindowViewModelBase
    {
        private RelayCommand _removeSpellsCommand;
        private RelayCommand _removeWizformsCommand;

        public RelayCommand RemoveSpellsCommand
        {
            get
            {
                return _removeSpellsCommand ??
                    (_removeSpellsCommand = new RelayCommand(parameter =>
                    {
                        SaveFileSpells_ViewModel.RemoveSpellsFromInventory();
                    },
                    (parameter) => true));
            }
            set { _removeSpellsCommand = value; }
        }

        public RelayCommand RemoveWizformsCommand
        {
            get
            {
                return _removeWizformsCommand ??
                    (_removeWizformsCommand = new RelayCommand(parameter =>
                    {
                        SaveFileWizforms_ViewModel.RemoveWizformsFromInventory();
                    },
                    (parameter) => true));
            }
            set { _removeWizformsCommand = value; }
        }
    }
}
