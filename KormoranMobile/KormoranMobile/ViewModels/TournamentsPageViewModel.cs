using KormoranMobile.Services;
using KormoranMobile.Util;
using KormoranMobile.ViewModels.Commands;
using KormoranShared.Models;
using Refit;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    internal class TournamentsPageViewModel : ViewModelBase
    {
        private readonly IKormoranServer _kormoranServer;
        private readonly IToastMessageService _toaster;

        private ObservableCollection<Tournament> _tournaments;
        public ObservableCollection<Tournament> Tournaments
        {
            get => _tournaments;
            private set => SetProperty(ref _tournaments, value);
        }

        private AsyncRelayCommand _downloadTournaments;
        public AsyncRelayCommand DownloadTournaments
        {
            get => _downloadTournaments;
            private set => SetProperty(ref _downloadTournaments, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        public TournamentsPageViewModel()
        {
            Tournaments = new ObservableCollection<Tournament>();
            _toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            try
            {
                _kormoranServer = RestService.For<IKormoranServer>(Constants.API_ADDRESS);
            }
            catch
            {
                _toaster.ShowToast("Upewnij się że adres podany w ustawieniach jest poprwany!");
            }

            DownloadTournaments = new AsyncRelayCommand(async () =>
            {
                var response = await _kormoranServer.GetTournaments();
                if (response.Error)
                {
                    _toaster.ShowToast(response.Message);
                }
                else
                {
                    Tournaments = new ObservableCollection<Tournament>(response.Collection);
                }
                IsRefreshing = false;
            });
        }

        public void RecreateHttpClient()
        {
            
        }
    }
}
