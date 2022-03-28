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
        private RelayCommand _gotoCommand;
        public RelayCommand GotoCommand
        {
            get
            {
                return _gotoCommand ??
                    (_gotoCommand = new RelayCommand(parameter =>
                    {
                        string p = parameter == null ? "" : parameter.ToString();
                        switch (p)
                        {
                            case "WizformsData": CurrentForm = CurrentForm.WizformData; break;
                            case "TextsData": CurrentForm = CurrentForm.TextData; break;
                            case "SpellsData": CurrentForm = CurrentForm.SpellData; break;
                            case "ItemsData": CurrentForm = CurrentForm.ItemData; break;
                            case "ScriptsData": CurrentForm = CurrentForm.ScriptData; break;
                            case "DialoguesData": CurrentForm = CurrentForm.DialogueData; break;

                            case "InventoryItems": CurrentForm = CurrentForm.InventoryItems; break;
                            case "InventorySpells": CurrentForm = CurrentForm.InventorySpells; break;
                            case "InventoryWizforms": CurrentForm = CurrentForm.InventoryWizforms; break;

                            case "AddSpellsToInventory": CurrentForm = CurrentForm.AddSpells; goto case "<add>";
                            case "AddWizformsToInventory": CurrentForm = CurrentForm.AddWizforms; goto case "<add>";

                            case "<add>": AppSources.MainWindow_ViewModel.Menu_Visibility = Visibility.Hidden; break;
                        }
                    },
                    (parameter) =>
                    {
                        string p = parameter == null ? "" : parameter.ToString();
                        switch(p)
                        {
                            case "WizformsData": return CurrentForm != CurrentForm.WizformData;
                            case "TextsData": return CurrentForm != CurrentForm.TextData;
                            case "SpellsData": return CurrentForm != CurrentForm.SpellData;
                            case "ItemsData": return CurrentForm != CurrentForm.ItemData;
                            case "ScriptsData": return CurrentForm != CurrentForm.ScriptData;
                            case "DialoguesData": return CurrentForm != CurrentForm.DialogueData;

                            case "InventoryItems": return CurrentForm != CurrentForm.InventoryItems && AppSources.CurrentSaveFile != null;
                            case "InventorySpells": return CurrentForm != CurrentForm.InventorySpells && AppSources.CurrentSaveFile != null;
                            case "InventoryWizforms": return CurrentForm != CurrentForm.InventoryWizforms && AppSources.CurrentSaveFile != null;

                            case "AddSpellsToInventory": return CurrentForm != CurrentForm.AddSpells;
                            case "AddWizformsToInventory": return CurrentForm != CurrentForm.AddWizforms;
                        }
                        return true;
                    }));
            }
            set { _gotoCommand = value; }
        }
    }
}
