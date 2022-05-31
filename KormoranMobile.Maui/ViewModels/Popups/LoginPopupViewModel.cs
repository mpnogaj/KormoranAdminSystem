using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models.Requests;
using Refit;
using System.Diagnostics;

namespace KormoranMobile.Maui.ViewModels.Popups
{
	internal class LoginPopupViewModel : ViewModelBase
	{
		private readonly IKormoranServer _kormoranServer;

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
		private readonly AsyncRelayCommand _goBackCommand;
		private readonly RelayCommand _closePopupCommand;
		public AsyncRelayCommand LoginCommand => _loginCommand;
		public AsyncRelayCommand GoBackCommand => _goBackCommand;
		public RelayCommand ClosePopupCommand => _closePopupCommand;
		#endregion

		public Action? ClosePopup { private get; set; }

		public LoginPopupViewModel()
		{
			Debug.WriteLine("LoginPageCtor");
			_kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
			_loginCommand = new AsyncRelayCommand(async () =>
			{
				var res = await _kormoranServer.Authenticate(new AuthenticateRequest
				{
					Username = Login,
					Password = Password
				});
				if (res.Error)
				{
					await Toast.Make(res.Message, ToastDuration.Long).Show();
				}
				else
				{
					await Toast.Make("Zalogowano pomyślnie", ToastDuration.Long).Show();
					AuthHelper.Token = res.Token;
					ClosePopup!();
				}
			}, () => !(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)));
			_closePopupCommand = new RelayCommand(() => ClosePopup!());
			_goBackCommand = new AsyncRelayCommand(async () => await Shell.Current.GoToAsync(".."));
		}
	}
}
