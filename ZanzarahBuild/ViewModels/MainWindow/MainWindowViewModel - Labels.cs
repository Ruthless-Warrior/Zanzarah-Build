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
        public string DataFiles_Label
        {
            get { return AppSources.GetLabel("Data Files"); }
        }

        public string SaveFiles_Label
        {
            get { return AppSources.GetLabel("Save Files"); }
        }

        public string Wizforms_Label
        {
            get { return AppSources.GetLabel("Wizforms"); }
        }

        public string Texts_Label
        {
            get { return AppSources.GetLabel("Texts"); }
        }

        public string Spells_Label
        {
            get { return AppSources.GetLabel("Spells"); }
        }

        public string Items_Label
        {
            get { return AppSources.GetLabel("Items"); }
        }

        public string Scripts_Label
        {
            get { return AppSources.GetLabel("Scripts"); }
        }

        public string Dialogues_Label
        {
            get { return AppSources.GetLabel("Dialogues"); }
        }

        public string Open_Label
        {
            get { return AppSources.GetLabel("Open"); }
        }

        public string Choose_Label
        {
            get { return AppSources.GetLabel("Choose..."); }
        }

        public string Load_Label
        {
            get { return AppSources.GetLabel("Load"); }
        }

        public string Reset_Label
        {
            get { return AppSources.GetLabel("Reset Changes"); }
        }

        public string Save_Label
        {
            get { return AppSources.GetLabel("Save"); }
        }

        public string SaveAs_Label
        {
            get { return AppSources.GetLabel("Save As..."); }
        }

        public string SaveAll_Label
        {
            get { return AppSources.GetLabel("Save All"); }
        }

        public string Add_Label
        {
            get { return AppSources.GetLabel("Add"); }
        }

        public string Remove_Label
        {
            get { return AppSources.GetLabel("Remove"); }
        }

        public string Ok_Label
        {
            get { return AppSources.GetLabel("Ok"); }
        }

        public string Cancel_Label
        {
            get { return AppSources.GetLabel("Cancel"); }
        }
    }
}
