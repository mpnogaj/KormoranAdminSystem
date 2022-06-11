using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranMobile.Maui.ViewModels.Popups;
using KormoranMobile.Maui.Views;
using KormoranMobile.Maui.Views.Popups;
using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;
using Refit;
using System.Text.Json;

namespace KormoranMobile.Maui.ViewModels.Pages
{
	[QueryProperty(nameof(TournamentReceiver), "tournament")]
	public class MatchesPageViewModel : ViewModelBase
	{
		private readonly IKormoranServer _kormoranServer;

		private readonly AsyncRelayCommand _refreshMatchesCommand;
		public AsyncRelayCommand RefreshMatchesCommand
			=> _refreshMatchesCommand;

		private readonly AsyncRelayCommand<Match> _itemTappedCommand;
		public AsyncRelayCommand<Match> ItemTappedCommand
			=> _itemTappedCommand;

		public string TournamentReceiver
		{
			set => Tournament =
				JsonSerializer.Deserialize<Tournament>(value) ?? throw new Exception("Tournament cannot be null!");
		}

		private Tournament? _tournament;
		public Tournament Tournament
		{
			get => _tournament ?? throw new NullReferenceException();
			set => SetProperty(ref _tournament, value);
		}

		private bool _isRefreshing;
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MatchesPageViewModel()
		{
			_kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
			_refreshMatchesCommand = new AsyncRelayCommand(async () =>
			{
				try
				{
					IsRefreshing = true;
					var response = await _kormoranServer.GetMatches(Tournament.TournamentId);
					if (!response.Error)
					{
						Tournament.Matches = response.Collection;
						OnPropertyChanged(nameof(Tournament));
					}
					else
					{
						await Toast.Make(response.Message).Show();
					}
				}
				catch (Exception ex)
				{
					await Toast.Make(ex.Message).Show();
				}
				finally
				{
					IsRefreshing = false;
				}
			}, () => IsRefreshing == false);

			_itemTappedCommand = new AsyncRelayCommand<Match>(async (m) =>
			{
				var popup = new EditScoresPopup(m);
				var modalRes = await PopupHelper.ShowPopupAsync(popup);
				if(modalRes != null)
				{
					var res = await _kormoranServer.UpdateScore((UpdateScoreRequestModel)modalRes);
					await Toast.Make(res.Message).Show();
					await _refreshMatchesCommand.ExecuteAsync();
				}
			}, () => AuthHelper.IsLoggedIn);
		}
	}
}
