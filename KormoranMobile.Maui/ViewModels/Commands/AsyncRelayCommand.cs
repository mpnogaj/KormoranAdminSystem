using System.Windows.Input;

namespace KormoranMobile.Maui.ViewModels.Commands
{
    public class AsyncRelayCommand<T> : IAsyncCommand<T>
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<T, Task> _execute;
        private readonly Func<bool> _canExecute;
        private bool _isExecuting = false;

        public AsyncRelayCommand(Func<T, Task> execute) : this(execute, () => true) { }

        public AsyncRelayCommand(Func<T, Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(T parameter)
        {
            if (_isExecuting)
            {
                return false;
            }
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (!_isExecuting)
            {
                _isExecuting = true;
                await _execute(parameter);
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        #region ICommand implementation

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null || parameter.GetType() != typeof(T)) return false;
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter == null || parameter.GetType() != typeof(T)) return;
            _ = ExecuteAsync((T)parameter);
        }

        #endregion ICommand implementation
    }

    public class AsyncRelayCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private bool _isExecuting = false;

        public AsyncRelayCommand(Func<Task> execute) : this(execute, () => true)
        {
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute()
        {
            if (_isExecuting)
            {
                return false;
            }
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public async Task ExecuteAsync()
        {
            if (!_isExecuting)
            {
                _isExecuting = true;
                await _execute();
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        #region ICommand implementation

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            _ = ExecuteAsync();
        }

        #endregion ICommand implementation
    }
}