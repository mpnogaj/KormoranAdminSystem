using CommunityToolkit.Maui.Alerts;
using KormoranMobile.Helpers;
using KormoranMobile.Services;
using KormoranMobile.ViewModels.Abstraction;
using KormoranMobile.ViewModels.Commands;
using KormoranMobile.Views.Popups;
using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;
using Refit;
using System.Text.Json;

namespace KormoranMobile.ViewModels.Pages
{
	[QueryProperty(nameof(TournamentReceiver), "tournament")]
	public class MatchesPageViewModel : ViewModelBase
	{
		public AsyncRelayCommand RefreshMatchesCommand { get; }

		public AsyncRelayCommand<Match> ItemTappedCommand { get; }

		public string TournamentReceiver
		{
			set => Tournament =
				JsonSerializer.Deserialize<Tournament>(value) ?? throw new Exception("Tournament cannot be null!");
		}

		private Tournament? _tournament;
		public Tournament Tournament
		{
			get => _tournament ?? new Tournament();
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
			var kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
			RefreshMatchesCommand = new AsyncRelayCommand(async () =>
			{
				try
				{
					IsRefreshing = true;
					var response = await kormoranServer.GetMatches(Tournament.TournamentId);
					if (!response.Error)
					{
						Tournament.Matches = response.Collection!;
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

			ItemTappedCommand = new AsyncRelayCommand<Match>(async (match) =>
			{
				var popup = new EditScoresPopup(match!);
				var modalRes = await PopupHelper.ShowPopupAsync(popup);
				if (modalRes != null)
				{
					var res = await kormoranServer.UpdateScore(AuthHelper.Token ?? string.Empty, (UpdateScoreRequestModel)modalRes);
					await Toast.Make(res.Message).Show();
					await RefreshMatchesCommand.ExecuteAsync();
				}
			}, false, (_) => AuthHelper.IsLoggedIn);
		}
	}
}
