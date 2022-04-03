using KormoranMobile.Services;
using KormoranMobile.ViewModels.Commands;
using System;
using Xamarin.Forms;

namespace KormoranMobile.ViewModels
{
    internal class MainPageViewModel : ViewModelBase
    {
        private IRequesterService _requester;
        public AsyncRelayCommand<string> UpdateScore { get; private set; }

        private string _matchId;
        public string MatchId { get => _matchId; set => SetProperty(ref _matchId, value); }

        private string _points;
        public string Points { get => _points; set => SetProperty(ref _points, value);
        }

        public MainPageViewModel()
        {
            _requester = DependencyService.Get<IRequesterService>(DependencyFetchTarget.GlobalInstance);
            UpdateScore = new AsyncRelayCommand<string>(async (string team) =>
            {
                var res = await _requester.SendPost<BasicResponse>("/api/matches/IncrementScore", new RequestModel
                {
                    MatchId = Convert.ToInt32(_matchId),
                    Value = Convert.ToInt32(_points),
                    Team = Convert.ToInt32(team)
                });


            }, () => true);
        }
    }

    public class RequestModel
    {
        public int MatchId { get; set; }
        public int Team { get; set; }
        public int Value { get; set; }
    }

    public class BasicResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
