namespace KormoranMobile.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
		private string _username;
		public string Username { get => _username; set => SetProperty(ref _username, value); }
		
		private string _password;
		public string Password { get => _password; set => SetProperty(ref _password, value); }
	}
}