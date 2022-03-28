using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.Models.Data
{
    public class Wizform : InventoryEntity
    {
        private WizformFile wizformFile;

        private string _model;
        private SingleLevelOfMagic _element;
        private ObservableCollection<Magic> _magics = new ObservableCollection<Magic>();
        private int _hitPoints;
        private int _evolutionWizformNumber;
        private int _evolutionLevel;
        private int _dexterity;
        private int _jumpAbility;
        private int _special;
        private int _voice;
        private int _experienceModifier;

        public int Litter0 { get; set; }
        public int Litter1 { get; set; }
        public int Litter2 { get; set; }
        public int Litter3 { get; set; }
        public int Litter4 { get; set; }
        public int Litter5 { get; set; }
        public int Litter6 { get; set; }
        public int Litter7 { get; set; }
        public int Litter8 { get; set; }
        public int Litter9 { get; set; }
        public int Litter10 { get; set; }
        public int Litter11 { get; set; }
        public int Litter12 { get; set; }
        public int Litter13 { get; set; }
        public int Litter14 { get; set; }

        public string Model
        {
            get { return _model; }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged();
                }
            }
        }
        public SingleLevelOfMagic Element
        {
            get { return _element; }
            set
            {
                if (_element != value)
                {
                    _element = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Magic> Magics
        {
            get { return _magics; }
            set
            {
                if (_magics != value)
                {
                    _magics = value;
                    OnPropertyChanged();
                }
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                if (_hitPoints != value)
                {
                    _hitPoints = value;
                    OnPropertyChanged();
                }
            }
        }
        public int EvolutionWizformNumber
        {
            get { return _evolutionWizformNumber; }
            set
            {
                if (_evolutionWizformNumber != value)
                {
                    _evolutionWizformNumber = value;
                    if (value == -1) EvolutionLevel = -1;
                    else if (EvolutionLevel == -1) EvolutionLevel = 1;
                    OnPropertyChanged();
                    OnPropertyChanged("EvolutionWizform");
                    OnPropertyChanged("EvolutionLevel");
                }
            }
        }
        public Wizform EvolutionWizform
        {
            get
            {
                if (EvolutionWizformNumber == -1) return null;
                var wizforms = wizformFile.Wizforms.Where(w => w.Number == EvolutionWizformNumber).ToList();
                if (wizforms.Count != 1) return null;
                return wizforms[0];
            }
        }
        public int EvolutionLevel
        {
            get { return _evolutionLevel; }
            set
            {
                if (_evolutionLevel != value)
                {
                    _evolutionLevel = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Dexterity
        {
            get { return _dexterity; }
            set
            {
                if (_dexterity != value)
                {
                    _dexterity = value;
                    OnPropertyChanged();
                }
            }
        }
        public int JumpAbility
        {
            get { return _jumpAbility; }
            set
            {
                if (_jumpAbility != value)
                {
                    _jumpAbility = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Special
        {
            get { return _special; }
            set
            {
                if (_special != value)
                {
                    _special = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Voice
        {
            get { return _voice; }
            set
            {
                if (_voice != value)
                {
                    _voice = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ExperienceModifier
        {
            get { return _experienceModifier; }
            set
            {
                if (_experienceModifier != value)
                {
                    _experienceModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        public void GetMagic(int level, out LevelOfMagic a1, out LevelOfMagic p1, out LevelOfMagic a2, out LevelOfMagic p2)
        {
            a1 = new LevelOfMagic(Element.Element, passive: false);
            p1 = new LevelOfMagic(passive: true);
            a2 = new LevelOfMagic(passive: false);
            p2 = new LevelOfMagic(passive: true);
            for (int l = 1; l <= level; l++)
            {
                foreach (var magic in Magics)
                {
                    if (magic.Slot == 0) continue;
                    if (magic.Level == l)
                    {
                        switch (magic.Slot)
                        {
                            case 1:
                                a1 = new LevelOfMagic
                                { Element1 = new Element(magic.X1), Element2 = new Element(magic.X2), Element3 = new Element(magic.X3), IsPassive = false };
                                break;
                            case 2:
                                p1 = new LevelOfMagic
                                { Element1 = new Element(magic.X1), Element2 = new Element(magic.X2), Element3 = new Element(magic.X3), IsPassive = true };
                                break;
                            case 3:
                                a2 = new LevelOfMagic
                                { Element1 = new Element(magic.X1), Element2 = new Element(magic.X2), Element3 = new Element(magic.X3), IsPassive = false };
                                break;
                            case 4:
                                p2 = new LevelOfMagic
                                { Element1 = new Element(magic.X1), Element2 = new Element(magic.X2), Element3 = new Element(magic.X3), IsPassive = true };
                                break;
                        }
                    }
                }
            }
        }

        public override CroppedBitmap Icon
        {
            get
            {
                if (Number < 0 || Number > 76)
                    return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\ZAN000T.BMP", UriKind.Relative)),
                new Int32Rect(1, 2, 38, 38));

                return new CroppedBitmap(
                new BitmapImage(new Uri(@"Bitmap\Origin\WIZ000T.BMP", UriKind.Relative)),
                new Int32Rect(1 + 40 * Number, 2, 38, 38));
            }
        }

        //public bool IsNotEmpty { get; } = true;

        public Wizform(TextFile textFile, WizformFile wizformFile) : base(textFile)
        {
            this.wizformFile = wizformFile;
        }
    }
}
