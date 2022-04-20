using System.Threading.Tasks;
using System.Windows.Input;

namespace KormoranMobile.ViewModels.Commands
{
    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);

        bool CanExecute(T parameter);
    }

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}