using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common.Wpf.Mvvm
{
    public class AsyncCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            await Task.Factory.StartNew(() => this.execute(parameter), TaskCreationOptions.LongRunning);
        }
    }
}
