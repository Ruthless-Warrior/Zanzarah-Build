using Common.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZanzarahBuild.Models.Data;

namespace ZanzarahBuild.ViewModels
{
    public class TestViewModel : ViewModelBase
    {


        private ObservableCollection<Wizform> _wizforms = new ObservableCollection<Wizform>();
        public ObservableCollection<Wizform> Wizforms
        {
            get { return _wizforms; }
            set
            {
                if (_wizforms != value)
                {
                    _wizforms = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }


        public void GetWizforms()
        {
            foreach (var w in AppSources.CurrentWizformFile.Wizforms.Where(f => f.Number < 77))
            {
                Wizforms.Add(w);
            }

            foreach (int u in new int[] { 4, 6, 0, 1, 2, 60, 7, 3, 13, 61, 15, 8, 51, 52, 53, 70, 17, 18, 19, 20, 58, 59, 16, 10, 26, 44, 45, 46, 47, 48, 62, 63, 64, 65, 66, 67, 54, 55, 56, 57, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 21, 22, 23, 24, 25, 27, 71, 72, 73 })
                foreach (var i in AppSources.CurrentItemFile.Items.Where(t => t.Number == u))
                {
                    Items.Add(i);
                }
        }



    }
}
