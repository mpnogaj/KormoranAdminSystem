using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models.Requests;
using Refit;
using System.Diagnostics;

namespace KormoranMobile.Maui.ViewModels
{
	internal class LoginPageViewModel : ViewModelBase
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
		public AsyncRelayCommand LoginCommand => _loginCommand;
		public AsyncRelayCommand GoBackCommand => _goBackCommand;
		#endregion

		public LoginPageViewModel()
		{
			Debug.WriteLine("LoginPageCtor");
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
					await Toast.Make(res.Message, ToastDuration.Long).Show();
				}
				else
				{
					await Toast.Make("Zalogowano pomyślnie", ToastDuration.Long).Show();
					AuthHelper.Token = res.Token;
				}
			}, () => !(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)));
			_goBackCommand = new AsyncRelayCommand(async () => await Shell.Current.GoToAsync(".."));
		}
	}
}
