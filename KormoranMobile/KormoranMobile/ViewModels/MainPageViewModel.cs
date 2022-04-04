using KormoranMobile.Services;
using KormoranMobile.ViewModels.Commands;
using System;
using Xamarin.Forms;
using Refit;
using System.Diagnostics;

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

        private string _address;
        public string Address { get => _address; set => SetProperty(ref _address, value); }

        public MainPageViewModel()
        {
            _kormoranServer = RestService.For<IKormoranServer>("http://www.http2demo.io");
            _toaster = DependencyService.Get<IToastMessageService>(DependencyFetchTarget.GlobalInstance);
            UpdateScore = new AsyncRelayCommand<string>(async (string team) =>
            {
                try
                {
                    var pingResponse = await _kormoranServer.PingTest();
                    Debug.WriteLine(pingResponse);
                }
                catch (Exception ex)
                {
                    _toaster.ShowToast($"Wystapił błąd. Treść: {ex.Message}");
                }
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
