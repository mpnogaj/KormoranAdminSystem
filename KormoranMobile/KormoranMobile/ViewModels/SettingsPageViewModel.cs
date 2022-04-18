using KormoranMobile.Services;
using KormoranMobile.ViewModels.Commands;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private static ISettings AppSettings =>
            CrossSettings.Current;
        private IToastMessageService _toaster;

        private string _serverAddress;
        public string ServerAddress 
        {
            get => _serverAddress;
            set => SetProperty(ref _serverAddress, value);
        }

        private RelayCommand _saveSettings;
        public RelayCommand SaveSettings
        {
            get => _saveSettings;
            private set => SetProperty(ref _saveSettings, value);
        }

        public SettingsPageViewModel()
        {
            _toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            ServerAddress = AppSettings.GetValueOrDefault(nameof(ServerAddress), string.Empty);
            SaveSettings = new RelayCommand(() =>
            {
                AppSettings.AddOrUpdateValue(nameof(ServerAddress), ServerAddress);
                _toaster.ShowToast("Uruchom ponownie aplikację!");
            });
        }
    }
}
