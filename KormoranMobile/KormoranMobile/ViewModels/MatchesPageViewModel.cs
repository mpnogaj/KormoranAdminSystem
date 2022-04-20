using KormoranMobile.Services;
using KormoranMobile.Util;
using KormoranMobile.ViewModels.Commands;
using KormoranShared.Models;
using Refit;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    public class MatchesPageViewModel : ViewModelBase
    {
        private ObservableCollection<Match> _matches;

        public ObservableCollection<Match> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        private int _tournamentId;

        public int TournamentId
        {
            get => _tournamentId;
            set => _tournamentId = value;
        }

        private AsyncRelayCommand _downloadMatches;

        public AsyncRelayCommand DownloadMatches
        {
            get => _downloadMatches;
            private set => SetProperty(ref _downloadMatches, value);
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

        public MatchesPageViewModel()
        {
            Matches = new ObservableCollection<Match>();
            var kormoranServer = RestService.For<IKormoranServer>(Constants.API_ADDRESS);
            var toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            DownloadMatches = new AsyncRelayCommand(async () =>
            {
                var response = await kormoranServer.GetMatches(TournamentId);
                if (response.Error)
                {
                    toaster.ShowToast(response.Message);
                }
                else
                {
                    Matches = new ObservableCollection<Match>(response.Collection);
                }
                IsRefreshing = false;
            });
        }
    }
}