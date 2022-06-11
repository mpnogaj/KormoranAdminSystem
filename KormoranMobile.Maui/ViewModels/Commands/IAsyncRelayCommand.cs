using System.Windows.Input;

namespace KormoranMobile.Maui.ViewModels.Commands
{
	public interface IAsyncCommand<in T> : ICommand
	{
		Task ExecuteAsync(T? parameter);

		bool CanExecute(T? parameter);
	}

	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync();

		bool CanExecute();
	}
}