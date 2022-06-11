using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Properties;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models.Requests;
using Refit;

namespace KormoranMobile.Maui.ViewModels.Popups
{
	internal class LoginPopupViewModel : ViewModelBase
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

		public AsyncRelayCommand LoginCommand { get; }

		public AsyncRelayCommand GoBackCommand { get; }

		public RelayCommand ClosePopupCommand { get; }

		#endregion

		public Action? ClosePopup { private get; set; }

		public LoginPopupViewModel()
		{
			var kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
			LoginCommand = new AsyncRelayCommand(async () =>
			{
				var res = await kormoranServer.Authenticate(new AuthenticateRequest
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
					await Toast.Make(Resources.LoginSuccessful, ToastDuration.Long).Show();
					AuthHelper.Token = res.Token;
					ClosePopup!();
				}
			}, () => !(string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)));
			ClosePopupCommand = new RelayCommand(() => ClosePopup!());
			GoBackCommand = new AsyncRelayCommand(async () => await Shell.Current.GoToAsync(".."));
		}
	}
}
