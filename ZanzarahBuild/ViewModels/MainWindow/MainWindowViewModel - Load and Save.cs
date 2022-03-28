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
        private AsyncCommand _loadDataCommand;
        private AsyncCommand _saveDataCommand;
        private AsyncCommand _loadFileSaveCommand;
        private AsyncCommand _saveFileSaveCommand;

        private AsyncCommand _windowLoadedCommand;


        public AsyncCommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ??
                    (_loadDataCommand = new AsyncCommand(_reset =>
                    {
                        bool reset = _reset.ToString().ToLower() == "true";

                        if (reset)
                        {
                            AppSources.CurrentTextFile = new TextFile(AppSources.Progress);
                            AppSources.CurrentDialogueFile = new DialogueFile(AppSources.Progress);
                            AppSources.CurrentScriptFile = new ScriptFile(AppSources.Progress);
                            AppSources.CurrentItemFile = new ItemFile(AppSources.Progress, AppSources.CurrentTextFile);
                            AppSources.CurrentSpellFile = new SpellFile(AppSources.Progress, AppSources.CurrentTextFile);
                            AppSources.CurrentWizformFile = new WizformFile(AppSources.Progress, AppSources.CurrentTextFile);
                        }
                        else
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "FBS file (*.dat)|*.FBS";
                            openFileDialog.Multiselect = true;
                            if (openFileDialog.ShowDialog() == false) return;
                            else
                            {
                                List<string> fs = new List<string>();
                                List<string> strs = openFileDialog.FileNames.Where(s =>
                                {
                                    string x = s.Substring(s.LastIndexOf('\\') + 1).ToLower();
                                    for (int i = 1; i <= 6; i++)
                                        if (x == $"_fb0x0{i}.fbs")
                                        {
                                            fs.Add(x);
                                            return true;
                                        }
                                    return false;
                                }).ToList();

                                if (fs.Contains("_fb0x02.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x02.fbs").ToList()[0];
                                    AppSources.CurrentTextFile = new TextFile(AppSources.Progress, path);
                                    AppSources.CurrentTextFile.ChangePath("Data\\_fb0x02.fbs");
                                }
                                if (fs.Contains("_fb0x06.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x06.fbs").ToList()[0];
                                    AppSources.CurrentDialogueFile = new DialogueFile(AppSources.Progress, path);
                                    AppSources.CurrentDialogueFile.ChangePath("Data\\_fb0x06.fbs");
                                }
                                if (fs.Contains("_fb0x05.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x05.fbs").ToList()[0];
                                    AppSources.CurrentScriptFile = new ScriptFile(AppSources.Progress, path);
                                    AppSources.CurrentScriptFile.ChangePath("Data\\_fb0x05.fbs");
                                }
                                if (fs.Contains("_fb0x04.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x04.fbs").ToList()[0];
                                    AppSources.CurrentItemFile = new ItemFile(AppSources.Progress, AppSources.CurrentTextFile, path);
                                    AppSources.CurrentItemFile.ChangePath("Data\\_fb0x04.fbs");
                                }
                                if (fs.Contains("_fb0x03.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x03.fbs").ToList()[0];
                                    AppSources.CurrentSpellFile = new SpellFile(AppSources.Progress, AppSources.CurrentTextFile, path);
                                    AppSources.CurrentSpellFile.ChangePath("Data\\_fb0x03.fbs");
                                }
                                if (fs.Contains("_fb0x01.fbs"))
                                {
                                    string path = strs.Where(s => s.Substring(s.LastIndexOf('\\') + 1).ToLower() == "_fb0x01.fbs").ToList()[0];
                                    AppSources.CurrentWizformFile = new WizformFile(AppSources.Progress, AppSources.CurrentTextFile, path);
                                    AppSources.CurrentWizformFile.ChangePath("Data\\_fb0x01.fbs");
                                }
                            }
                        }

                        WizformFile_ViewModel.File = AppSources.CurrentWizformFile;
                        TextFile_ViewModel.File = AppSources.CurrentTextFile;
                        SpellFile_ViewModel.File = AppSources.CurrentSpellFile;
                        ItemFile_ViewModel.File = AppSources.CurrentItemFile;
                        ScriptFile_ViewModel.File = AppSources.CurrentScriptFile;
                        DialogueFile_ViewModel.File = AppSources.CurrentDialogueFile;

                        ReportDone();
                    },
                    (_reset) =>
                    {
                        return true;
                    }
                    ));
            }
            set { _loadDataCommand = value; }
        }

        public AsyncCommand SaveDataCommand
        {
            get
            {
                return _saveDataCommand ??
                    (_saveDataCommand = new AsyncCommand(_all =>
                    {
                        AppSources.Time = DateTime.Now;
                        bool all = _all.ToString().ToLower() == "true";
                        Menu_Visibility = Visibility.Hidden;
                        if (all || WizformFile_Visibility == Visibility.Visible) AppSources.CurrentWizformFile.Write(AppSources.Progress);
                        if (all || TextFile_Visibility == Visibility.Visible) AppSources.CurrentTextFile.Write(AppSources.Progress);
                        if (all || SpellFile_Visibility == Visibility.Visible) AppSources.CurrentSpellFile.Write(AppSources.Progress);
                        if (all || ItemFile_Visibility == Visibility.Visible) AppSources.CurrentItemFile.Write(AppSources.Progress);
                        if (all || ScriptFile_Visibility == Visibility.Visible) AppSources.CurrentScriptFile.Write(AppSources.Progress);
                        if (all || DialogueFile_Visibility == Visibility.Visible) AppSources.CurrentDialogueFile.Write(AppSources.Progress);

                        ReportDone();
                    },
                    (_all) =>
                    {
                        return
                        WizformFile_Visibility == Visibility.Visible || TextFile_Visibility == Visibility.Visible ||
                        SpellFile_Visibility == Visibility.Visible || ItemFile_Visibility == Visibility.Visible ||
                        ScriptFile_Visibility == Visibility.Visible || DialogueFile_Visibility == Visibility.Visible;
                    }
                    ));
            }
            set { _saveDataCommand = value; }
        }

        public AsyncCommand LoadFileSaveCommand
        {
            get
            {
                return _loadFileSaveCommand ??
                    (_loadFileSaveCommand = new AsyncCommand(parameter =>
                    {
                        string p = parameter.ToString().ToUpper();

                        string path = "";

                        switch (p[0])
                        {
                            // Save directory
                            case 'S':
                                path = $"Save\\_000{p[1]}.dat";
                                break;

                            // VirtualStore directory
                            case 'V':
                                path =
                                $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\AppData\\Local\\VirtualStore\\Program Files (x86)\\Zanzarah\\Save\\_000{p[1]}.dat";
                                break;

                            // Choose
                            case 'C':
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                saveFileDialog.Filter = "DAT file (*.dat)|*.dat";
                                if (saveFileDialog.ShowDialog() == false) return;
                                else
                                {
                                    path = saveFileDialog.FileName;
                                }
                                break;

                            case 'R':
                                path = AppSources.CurrentSaveFile.FilePath;
                                break;
                        }

                        var save = AppSources.CurrentSaveFile =
                            new Models.Data.Files.SaveFile(AppSources.Progress, AppSources.CurrentItemFile, AppSources.CurrentSpellFile, AppSources.CurrentWizformFile, path);

                        SaveFileItems_ViewModel = new SaveFileItemsViewModel(save);
                        SaveFileSpells_ViewModel = new SaveFileSpellsViewModel(save);
                        SaveFileWizforms_ViewModel = new SaveFileWizformsViewModel(save);

                        AddSpells_ViewModel = new AddSpellsViewModel(save);
                        AddWizforms_ViewModel = new AddWizformsViewModel(save);

                        if (CurrentForm != CurrentForm.InventoryWizforms &&
                            CurrentForm != CurrentForm.InventorySpells &&
                            CurrentForm != CurrentForm.InventoryItems)
                            CurrentForm = CurrentForm.InventoryWizforms;

                        ReportDone();
                    },
                    (parameter) =>
                    {
                        string p = parameter.ToString().ToUpper();
                        string path = "";
                        switch (p[0])
                        {
                            case 'C': return true;
                            case 'R':
                                return AppSources.CurrentSaveFile != null;
                            case 'S':
                                path = $"Save\\_000{p[1]}.dat";
                                break;
                            case 'V':
                                path =
                                $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\AppData\\Local\\VirtualStore\\Program Files (x86)\\Zanzarah\\Save\\_000{p[1]}.dat";
                                break;
                        }
                        if (File.Exists(path) == false) return false;
                        try
                        {
                            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
                            {
                                reader.ReadBytes(reader.ReadInt32());
                                reader.ReadBytes(16);
                                reader.ReadBytes(reader.ReadInt32());
                                reader.ReadBytes(reader.ReadInt32());
                                reader.ReadBytes(8);
                                reader.ReadBytes(reader.ReadInt32());
                            }
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    ));
            }
            set { _loadFileSaveCommand = value; }
        }

        public AsyncCommand SaveFileSaveCommand
        {
            get
            {
                return _saveFileSaveCommand ??
                    (_saveFileSaveCommand = new AsyncCommand(_saveAs =>
                    {
                        AppSources.Time = DateTime.Now;
                        bool saveAs = _saveAs.ToString().ToLower() == "true";
                        if (saveAs)
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "DAT file (*.dat)|*.dat";
                            if (saveFileDialog.ShowDialog() == false) return;
                            else
                            {
                                AppSources.CurrentSaveFile.ChangePath(saveFileDialog.FileName);
                            }
                        }
                        Menu_Visibility = Visibility.Hidden;
                        AppSources.CurrentSaveFile.Write(AppSources.Progress);

                        ReportDone();
                    },
                    (_saveAs) => true));
            }
            set { _saveFileSaveCommand = value; }
        }

        public AsyncCommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ??
                    (_windowLoadedCommand = new AsyncCommand((parameter) =>
                    {
                        AppSources.Progress = new Progress<string>(s => ProgressText = s);

                        var txf = AppSources.CurrentTextFile = new TextFile(AppSources.Progress);
                        var dgf = AppSources.CurrentDialogueFile = new DialogueFile(AppSources.Progress);
                        var scf = AppSources.CurrentScriptFile = new ScriptFile(AppSources.Progress);
                        var itf = AppSources.CurrentItemFile = new ItemFile(AppSources.Progress, AppSources.CurrentTextFile);
                        var spf = AppSources.CurrentSpellFile = new SpellFile(AppSources.Progress, AppSources.CurrentTextFile);
                        var wzf = AppSources.CurrentWizformFile = new WizformFile(AppSources.Progress, AppSources.CurrentTextFile);

                        WizformFile_ViewModel = new WizformFileViewModel { File = wzf };
                        TextFile_ViewModel = new TextFileViewModel { File = txf };
                        SpellFile_ViewModel = new SpellFileViewModel { File = spf };
                        ItemFile_ViewModel = new ItemFileViewModel { File = itf };
                        ScriptFile_ViewModel = new ScriptFileViewModel { File = scf };
                        DialogueFile_ViewModel = new DialogueFileViewModel { File = dgf };

                        CurrentForm = CurrentForm.WizformData;
                        //TestViewModel = new TestViewModel();
                        //TestViewModel.GetWizforms();
                        ReportDone();
                    },
                    (parameter) => true));
            }
            set { _windowLoadedCommand = value; }
        }


        private TestViewModel _TestViewModel;
        public TestViewModel TestViewModel
        {
            get { return _TestViewModel; }
            set
            {
                if (_TestViewModel != value)
                {
                    _TestViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ReportDone()
        {
            ProgressText = "Done";
            Menu_Visibility = Visibility.Visible;
            System.Threading.Thread.Sleep(2500);
            ProgressText = "";
        }
    }
}
