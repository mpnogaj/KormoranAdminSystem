using CommunityToolkit.Maui.Alerts;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models.Requests;
using Refit;

namespace KormoranMobile.Maui.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        private IKormoranServer _kormoranServer;

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
            _kormoranServer = RestService.For<IKormoranServer>("http://192.168.88.122/api");
            _loginCommand = new AsyncRelayCommand(async () =>
            {
                var res = await _kormoranServer.Authenticate(new AuthenticateRequest
                {
                    Username = Login,
                    Password = Password
                });
                if (res.Error)
                {
                    await Toast.Make(res.Message).Show();
                }
                else
                {
                    await Toast.Make(res.Token).Show();
                }
            }, () => !(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)));
        }
    }
}
