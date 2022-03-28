using Common.Wpf.Mvvm;
using System.Windows;

namespace ZanzarahBuild.ViewModels
{
    public enum CurrentForm
    {
        WizformData,
        TextData,
        SpellData,
        ItemData,
        ScriptData,
        DialogueData,
        InventoryItems,
        InventorySpells,
        InventoryWizforms,
        AddSpells,
        AddWizforms
    }

    public partial class MainWindowViewModel : WindowViewModelBase
    {
        private CurrentForm _currentForm;
        public CurrentForm CurrentForm
        {
            get
            {
                return _currentForm;
            }
            set
            {
                switch (_currentForm)
                {
                    case CurrentForm.WizformData: WizformFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.TextData: TextFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.SpellData: SpellFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.ItemData: ItemFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.ScriptData: ScriptFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.DialogueData: DialogueFile_Visibility = Visibility.Hidden; break;
                    case CurrentForm.InventoryItems: SaveFileItems_Visibility = Visibility.Hidden; break;
                    case CurrentForm.InventorySpells: SaveFileSpells_Visibility = Visibility.Hidden; break;
                    case CurrentForm.InventoryWizforms: SaveFileWizforms_Visibility = Visibility.Hidden; break;
                    case CurrentForm.AddSpells: AddSpells_Visibility = Visibility.Hidden; break;
                    case CurrentForm.AddWizforms: AddWizforms_Visibility = Visibility.Hidden; break;
                }
                _currentForm = value;
                switch (_currentForm)
                {
                    case CurrentForm.WizformData:
                        WizformFile_Visibility = Visibility.Visible;
                        WizformFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.TextData:
                        TextFile_Visibility = Visibility.Visible;
                        TextFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.SpellData:
                        SpellFile_Visibility = Visibility.Visible;
                        SpellFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.ItemData:
                        ItemFile_Visibility = Visibility.Visible;
                        ItemFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.ScriptData:
                        ScriptFile_Visibility = Visibility.Visible;
                        ScriptFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.DialogueData:
                        DialogueFile_Visibility = Visibility.Visible;
                        DialogueFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.InventoryItems:
                        SaveFileItems_Visibility = Visibility.Visible;
                        SaveFileItems_ViewModel.ChangeHeader(); break;
                    case CurrentForm.InventorySpells:
                        SaveFileSpells_Visibility = Visibility.Visible;
                        SaveFileSpells_ViewModel.ChangeHeader(); break;
                    case CurrentForm.InventoryWizforms:
                        SaveFileWizforms_Visibility = Visibility.Visible;
                        SaveFileWizforms_ViewModel.ChangeHeader(); break;
                    case CurrentForm.AddSpells:
                        AddSpells_Visibility = Visibility.Visible;
                        SpellFile_ViewModel.ChangeHeader(); break;
                    case CurrentForm.AddWizforms:
                        AddWizforms_Visibility = Visibility.Visible;
                        WizformFile_ViewModel.ChangeHeader(); break;
                }
            }
        }
    }
}
