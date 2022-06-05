using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KormoranMobile.Maui.ViewModels.Popups
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
		private readonly RelayCommand _cancelCommand;
		public RelayCommand CancelCommand => _cancelCommand;

		private readonly RelayCommand _saveCommand;
		public RelayCommand SaveCommand => _saveCommand;
		#endregion

		public Action<string?>? ClosePopup { private get; set; }

		public SettingsPopupViewModel()
		{
			_serverAddress = Preferences.Get(ServerHelper.AddressKey, string.Empty);
			_saveCommand = new RelayCommand(() =>
			{
				ClosePopup!(ServerAddress);
			});
			_cancelCommand = new RelayCommand(() =>
			{
				ClosePopup!(null);
			});
		}
	}
}
