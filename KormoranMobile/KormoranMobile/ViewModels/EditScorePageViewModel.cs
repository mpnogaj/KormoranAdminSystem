using KormoranShared.Models.Requests.Matches;
using KormoranMobile.Services;
using KormoranMobile.ViewModels.Commands;
using Refit;
using System;
using Xamarin.Forms;
using System.Diagnostics;
using KormoranShared.Models;
using KormoranMobile.Util;

namespace KormoranMobile.ViewModels
{
    public class EditScorePageViewModel : ViewModelBase
    {
        private readonly IKormoranServer _kormoranServer;
        private IToastMessageService _toaster;
        public AsyncRelayCommand<string> UpdateScore { get; private set; }

        private Match _match;
        public Match Match { get => _match; set => SetProperty(ref _match, value); }

        private string _points;
        public string Points { get => _points; set => SetProperty(ref _points, value); }

        public EditScorePageViewModel()
        {
            _kormoranServer = RestService.For<IKormoranServer>(Constants.API_ADDRESS);
            _toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            UpdateScore = new AsyncRelayCommand<string>(async (string team) =>
            {
                try
                {
                    int pts = Convert.ToInt32(_points);
                    int targetTeam = Convert.ToInt32(team);
                    var request = new IncrementScoreRequestModel
                    {
                        Team = targetTeam,
                        MatchId = Match.MatchId,
                        Value = pts
                    };
                    var response = await _kormoranServer.IncrementScore(request);
                    if(!response.Error)
                    {
                        switch (targetTeam)
                        {
                            case 1:
                                Match.Team1Score += pts;
                                break;
                            case 2:
                                Match.Team2Score += pts;
                                break;
                        }
                        OnPropertyChanged(nameof(Match));
                    }
                    _toaster.ShowToast(response.Message);
                }
                catch (Exception ex)
                {
                    _toaster.ShowToast($"Wystapił błąd. Treść: {ex.Message}");
                }
            }, () => true);
        }
    }
}
