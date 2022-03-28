using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class ItemFileViewModel : ViewModelBase
    {
        private Item _selected;
        private ItemFile _file;
        // --------------------------------------------------------------------------------
        private RelayCommand _nameChangedCommand;
        // --------------------------------------------------------------------------------



        public Item Selected
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

        public ItemFile File
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
        public string Script_Label
        {
            get { return AppSources.GetLabel("Script"); }
        }
    }
}
