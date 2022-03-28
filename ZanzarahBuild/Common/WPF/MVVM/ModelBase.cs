using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Wpf.Mvvm
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    SelectedChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void SelectedChanged() { }
    }
}