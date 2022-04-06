using KormoranShared.Models.Requests.Matches;
using KormoranMobile.Services;
using KormoranMobile.ViewModels.Commands;
using Refit;
using System;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    internal class MainPageViewModel : ViewModelBase
    {
        private readonly IKormoranServer _kormoranServer;
        private IToastMessageService _toaster;
        public AsyncRelayCommand<string> UpdateScore { get; private set; }

        private string _matchId;
        public string MatchId { get => _matchId; set => SetProperty(ref _matchId, value); }

        private string _points;
        public string Points { get => _points; set => SetProperty(ref _points, value); }

        public MainPageViewModel()
        {
            _kormoranServer = RestService.For<IKormoranServer>("http://172.20.10.6/api");
            _toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            UpdateScore = new AsyncRelayCommand<string>(async (string team) =>
            {
                try
                {
                    var request = new IncrementScoreRequestModel
                    {
                        Team = Convert.ToInt32(team),
                        MatchId = Convert.ToInt32(_matchId),
                        Value = Convert.ToInt32(_points)
                    };
                    var pingResponse = await _kormoranServer.IncrementScore(request);
                    _toaster.ShowToast(pingResponse.Message);
                }
                catch (Exception ex)
                {
                    _toaster.ShowToast($"Wystapił błąd. Treść: {ex.Message}");
                }
            }, () => true);
        }
    }
}
