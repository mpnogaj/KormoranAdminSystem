using CommunityToolkit.Maui.Alerts;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;

namespace KormoranMobile.Maui.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        #region MVVM Props
        private string _login = string.Empty;
        public string Login 
        { 
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password = string.Empty;
        public string Password 
        { 
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            } 
        }
        #endregion

        #region Commands
        private readonly AsyncRelayCommand _loginCommand;
        public AsyncRelayCommand LoginCommand { get => _loginCommand; }
        #endregion

        public LoginPageViewModel()
        {
            _loginCommand = new AsyncRelayCommand(async () =>
            {
                await Toast.Make("Logowanie123").Show();
            }, () => !(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)));
        }
    }
}
