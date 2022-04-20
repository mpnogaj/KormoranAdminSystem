using KormoranMobile.Services;
using KormoranMobile.Util;
using KormoranMobile.ViewModels.Commands;
using KormoranMobile.Views;
using Refit;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;
        private readonly IKormoranServer _kormoranServer;

        #region MVVM Props

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private AsyncRelayCommand _loginGuestCommand;

        public AsyncRelayCommand LoginGuestCommand
        {
            get => _loginGuestCommand;
            private set => SetProperty(ref _loginGuestCommand, value);
        }

        private AsyncRelayCommand _loginCommand;

        public AsyncRelayCommand LoginCommand
        {
            get => _loginCommand;
            private set => SetProperty(ref _loginCommand, value);
        }

        #endregion MVVM Props

        public LoginPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _kormoranServer = RestService.For<IKormoranServer>(Constants.API_ADDRESS);
            _loginGuestCommand = new AsyncRelayCommand(async () => await Navigate());
            _loginCommand = new AsyncRelayCommand(async () =>
            {
                //get token
                await Navigate();
            }, () => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)));
        }

        private async Task Navigate()
        {
            await _navigation.PushAsync(new TournamentsPage());
        }
    }
}