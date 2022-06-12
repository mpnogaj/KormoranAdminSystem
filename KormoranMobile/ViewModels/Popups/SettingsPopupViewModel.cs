using KormoranMobile.Helpers;
using KormoranMobile.ViewModels.Abstraction;
using KormoranMobile.ViewModels.Commands;

namespace KormoranMobile.ViewModels.Popups
{
	public class SettingsPopupViewModel : ViewModelBase
	{
		#region MVVM Props
		private string _serverAddress;
		public string ServerAddress
		{
			get => _serverAddress;
			set => SetProperty(ref _serverAddress, value);
		}
		#endregion
		#region Commands

		public RelayCommand CancelCommand { get; }

		public RelayCommand SaveCommand { get; }

		#endregion

		public Action<string?>? ClosePopup { private get; set; }

		public SettingsPopupViewModel()
		{
			_serverAddress = Preferences.Get(ServerHelper.AddressKey, string.Empty);
			SaveCommand = new RelayCommand(() =>
			{
				ClosePopup!(ServerAddress);
			});
			CancelCommand = new RelayCommand(() =>
			{
				ClosePopup!(null);
			});
		}
	}
}
