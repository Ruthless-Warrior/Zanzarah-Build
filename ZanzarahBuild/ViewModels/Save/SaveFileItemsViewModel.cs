using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ZanzarahBuild.Models.Data;
using ZanzarahBuild.Models.Data.Files;

namespace ZanzarahBuild.ViewModels
{
    public class SaveFileItemsViewModel : ViewModelBase
    {
        private InventoryItem _selected;
        private SaveFile _file;

        public InventoryItem Selected
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
            if (Selected == null || Selected.Item == null)
            {
                AppSources.Icon = null;
                AppSources.Title = null;
            }
            else
            {
                AppSources.Icon = Selected.Item.Icon;
                AppSources.Title = Selected.Item.Name;
            }
        }
        public SaveFile File
        {
            get
            {
                return _file;
            }
        }


        public string Available_Label
        {
            get { return AppSources.GetLabel("Available"); }
        }
        public string Number_Label
        {
            get { return AppSources.GetLabel("Number"); }
        }
        public string Name_Label
        {
            get { return AppSources.GetLabel("Name"); }
        }
        public string Quantity_Label
        {
            get { return AppSources.GetLabel("Quantity"); }
        }

        public SaveFileItemsViewModel(SaveFile file)
        {
            _file = file;
            OnPropertyChanged("File");
        }
    }
}
