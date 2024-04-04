using System;
using System.Windows.Input;

namespace Application.ViewModel.Commands
{
    public class CommonCommand : ICommand
    {
        public CommonCommand(Action<object> execute, Func<object, bool> canExecute = null) 
        { 
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
