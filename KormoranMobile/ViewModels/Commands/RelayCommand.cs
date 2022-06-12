using KormoranMobile.Helpers;
using System.Windows.Input;

namespace KormoranMobile.ViewModels.Commands
{
	public class RelayCommand<T> : ICommand
	{
		#region Fields

		public event EventHandler? CanExecuteChanged;

		private readonly Action<T?> _execute;
		private readonly bool _allowNull;
		private readonly Func<T?, bool>? _canExecute;

		#endregion Fields

		#region Constructors

		public RelayCommand(Action<T?> execute, bool allowNull = false, Func<T?, bool>? canExecute = null)
		{
			_execute = execute;
			_allowNull = allowNull;
			_canExecute = canExecute;
		}

		#endregion Constructors

		#region ICommand Members

		public bool CanExecute(object? parameter) =>
			TypeHelper.CheckType(parameter, typeof(T), _allowNull) &&
			(_canExecute?.Invoke(parameter == null ? default : (T)parameter) ?? true);

		public void Execute(object? parameter)
		{
			if (!TypeHelper.CheckType(parameter, typeof(T), _allowNull))
			{
				throw new InvalidOperationException();
			}
			if (CanExecute(parameter))
			{
				_execute.Invoke(parameter == null ? default : (T)parameter);
			}
			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);

		#endregion ICommand Members
	}

	public class RelayCommand : ICommand
	{
		#region Fields

		public event EventHandler? CanExecuteChanged;

		private readonly Action _execute;
		private readonly Func<bool>? _canExecute;

		#endregion Fields

		#region Constructors

		public RelayCommand(Action execute, Func<bool>? canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion Constructors

		#region ICommand Members

		public bool CanExecute(object? parameter)
		{
			return _canExecute?.Invoke() ?? true;
		}

		public void Execute(object? parameter)
		{
			if (CanExecute(parameter))
			{
				_execute.Invoke();
			}
			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion ICommand Members
	}
}