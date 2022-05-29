using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranShared.Models;
using Refit;
using System.Text.Json;

namespace KormoranMobile.Maui.ViewModels
{
    [QueryProperty(nameof(TournamentReceiver), "tournamentId")]
    public class MatchesPageViewModel : ViewModelBase
    {
        private readonly IKormoranServer _kormoranServer;

        public string TournamentReceiver
        {
            set => Tournament = JsonSerializer.Deserialize<Tournament>(value);
        }

        private Tournament _tournament;
        public Tournament Tournament
        {
            get => _tournament;
            set => SetProperty(ref _tournament, value);
        }

        public MatchesPageViewModel()
        {
            _kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
        }

    }
}
