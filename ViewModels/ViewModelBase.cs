using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TikTokTTS
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class DelegateCommand : ICommand
        {
            private readonly Action<object> _executeAction;
            private readonly Func<object, bool> _canExecuteFunc;

            public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
            {
                _executeAction = executeAction;
                _canExecuteFunc = canExecuteFunc;
            }

            public DelegateCommand(Action<object> executeAction) : this(executeAction, (p) => true) { }

            public void Execute(object parameter) => _executeAction(parameter);

            public bool CanExecute(object parameter) => _canExecuteFunc?.Invoke(parameter) ?? true;

            public event EventHandler CanExecuteChanged;

            public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
