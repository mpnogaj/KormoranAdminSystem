using KormoranMobile.Maui.Helpers;
using System.Windows.Input;

namespace KormoranMobile.Maui.ViewModels.Commands
{
	public class AsyncRelayCommand<T> : IAsyncCommand<T>
	{
		public event EventHandler? CanExecuteChanged;

		private readonly Func<T?, Task> _execute;
		private readonly bool _allowNull;
		private readonly Func<T?, bool>? _canExecute;
		private readonly IErrorHandler _errorHandler;
		private bool _isExecuting;

		public AsyncRelayCommand(Func<T?, Task> execute, bool allowNull = false) : this(execute, allowNull, null)
		{
		}

		public AsyncRelayCommand(Func<T?, Task> execute,
			bool allowNull,
			Func<T?, bool>? canExecute) : this(execute,
			allowNull,
			canExecute,
			new LogErrorHandler())
		{
		}

		public AsyncRelayCommand(Func<T?, Task> execute,
			bool allowNull,
			Func<T?, bool>? canExecute,
			IErrorHandler errorHandler)
		{
			_execute = execute;
			_allowNull = allowNull;
			_canExecute = canExecute;
			_errorHandler = errorHandler;
		}

		public void RaiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);

		public bool CanExecute(T? parameter) =>
			!_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

		public async Task ExecuteAsync(T? parameter)
		{
			if (CanExecute(parameter))
			{
				try
				{
					_isExecuting = true;
					await _execute(parameter);
				}
				finally
				{
					_isExecuting = false;
				}
			}

			RaiseCanExecuteChanged();
		}

		#region ICommand implementation

		bool ICommand.CanExecute(object? parameter) =>
			TypeHelper.CheckType(parameter, typeof(T), _allowNull) &&
			CanExecute(parameter == null ? default : (T)parameter);

		void ICommand.Execute(object? parameter)
		{
			if (!TypeHelper.CheckType(parameter, typeof(T), _allowNull))
			{
				throw new InvalidOperationException();
			}

			ExecuteAsync(parameter == null ? default : (T)parameter).FireAndForgetAsync(_errorHandler);
		}

		#endregion ICommand implementation
	}

	public class AsyncRelayCommand : IAsyncCommand
	{
		public event EventHandler? CanExecuteChanged;

		private readonly Func<Task> _execute;
		private readonly Func<bool>? _canExecute;
		private readonly IErrorHandler _errorHandler;
		private bool _isExecuting;

		public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
			: this(execute, canExecute, new LogErrorHandler())
		{
		}

		public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute, IErrorHandler errorHandler)
		{
			_execute = execute;
			_canExecute = canExecute;
			_errorHandler = errorHandler;
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute()
		{
			return !_isExecuting && (_canExecute?.Invoke() ?? true);
		}

		public async Task ExecuteAsync()
		{
			if (CanExecute())
			{
				try
				{
					_isExecuting = true;
					await _execute();
				}
				finally
				{
					_isExecuting = false;
				}
			}

			RaiseCanExecuteChanged();
		}

		#region ICommand implementation

		bool ICommand.CanExecute(object? parameter)
		{
			return CanExecute();
		}

		void ICommand.Execute(object? parameter)
		{
			ExecuteAsync().FireAndForgetAsync(_errorHandler);
		}

		#endregion ICommand implementation
	}
}