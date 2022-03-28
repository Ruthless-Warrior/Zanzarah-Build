using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class WizformFileViewModel : ViewModelBase
    {
        private Wizform _selected;
        private WizformFile _file;
        private ObservableCollection<SingleLevelOfMagic> _levelOfMagicList;
        // --------------------------------------------------------------------------------
        private RelayCommand _nameChangedCommand;
        private RelayCommand _playVoiceCommand;
        // --------------------------------------------------------------------------------




        private SoundPlayer SoundPlayer { get; set; } = new SoundPlayer();

        public Wizform Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    ChangeHeader();
                    OnPropertyChanged();
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

        public WizformFile File
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

        public ObservableCollection<SingleLevelOfMagic> LevelOfMagicList
        {
            get
            {
                if (_levelOfMagicList == null)
                {
                    _levelOfMagicList = new ObservableCollection<SingleLevelOfMagic>();
                    for (byte b = 0x0; b < 0xE; b++)
                        _levelOfMagicList.Add(new SingleLevelOfMagic(new Element(b), true));
                }
                return _levelOfMagicList;
            }
        }

        public ObservableCollection<Wizform> WizformEvoList
        {
            get { return File.WizformEvoList; }
        }

        public List<Voice> VoiceList
        {
            get
            {
                return Voice.GetVoiceList();
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

        public RelayCommand PlayVoiceCommand
        {
            get
            {
                return _playVoiceCommand ??
                    (_playVoiceCommand = new RelayCommand(parameter =>
                    {
                        if (Selected == null) return;
                        string path = $"Audio\\Voices\\{Selected.Voice}\\{parameter}.mp3";
                        if (System.IO.File.Exists(path) == false) throw new FileNotFoundException($"Voice not found.\n{path}");
                        SoundPlayer.Stream = new MemoryStream(System.IO.File.ReadAllBytes(path), true);
                        SoundPlayer.Play();
                    },
                    (parameter) => true));
            }
            set { _playVoiceCommand = value; }
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
        public string HitPoints_Label
        {
            get { return AppSources.GetLabel("Hit Points"); }
        }
        public string Dexterity_Label
        {
            get { return AppSources.GetLabel("Dexterity"); }
        }
        public string JumpAbility_Label
        {
            get { return AppSources.GetLabel("Jump Ability"); }
        }
        public string Special_Label
        {
            get { return AppSources.GetLabel("Special"); }
        }
        public string ExperienceModifier_Label
        {
            get { return AppSources.GetLabel("Experience Modifier"); }
        }
        public string Model_Label
        {
            get { return AppSources.GetLabel("Model"); }
        }
        public string Evolution_Label
        {
            get { return AppSources.GetLabel("Evolution"); }
        }
        public string EvolutionWizform_Label
        {
            get { return AppSources.GetLabel("Evolution Wizform"); }
        }
        public string EvolutionLevel_Label
        {
            get { return AppSources.GetLabel("Evolution Level"); }
        }
        public string Voice_Label
        {
            get { return AppSources.GetLabel("Voice"); }
        }
        public string Element_Label
        {
            get { return AppSources.GetLabel("Element"); }
        }
        public string Attack_Label
        {
            get { return AppSources.GetLabel("Attack"); }
        }
        public string CriticalHit1_Label
        {
            get { return AppSources.GetLabel("Critical Hit") + " 1"; }
        }
        public string CriticalHit2_Label
        {
            get { return AppSources.GetLabel("Critical Hit") + " 2"; }
        }
        public string Pain_Label
        {
            get { return AppSources.GetLabel("Pain"); }
        }
        public string Triumph_Label
        {
            get { return AppSources.GetLabel("Triumph"); }
        }
        public string Level_Label
        {
            get { return AppSources.GetLabel("Level"); }
        }
        public WizformFileViewModel()
        {
            OnPropertyChanged("File");
        }
    }
}
