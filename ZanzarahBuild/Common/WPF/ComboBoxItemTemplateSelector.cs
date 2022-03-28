using System.Windows;
using System.Windows.Controls;

namespace Common.Wpf
{
    public class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        // Can set both templates from XAML
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            bool selected = false;

            // container is the ContentPresenter
            if (container is FrameworkElement fe)
            {
                DependencyObject parent = fe.TemplatedParent;
                if (parent != null && parent is ComboBox) selected = true;
            }

            if (selected) return SelectedItemTemplate;
            else return ItemTemplate;
        }
    }
}

// Данный класс для создания двух РАЗЛИЧНЫХ шаблонов ComboBox: один используется для отображения
// выбранного элемента в ToggleButton, второй - для отображенияя каждого элемента в списке Popup
// https://wpf.2000things.com/2014/02/19/1012-using-a-different-data-template-for-the-face-of-a-combobox/
