using CommunityToolkit.Maui.Alerts;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models;
using Refit;
using System.Diagnostics;
using System.Text.Json;

namespace KormoranMobile.Maui.ViewModels
{
	[QueryProperty(nameof(TournamentReceiver), "tournamentId")]
	public class MatchesPageViewModel : ViewModelBase
	{
		private readonly IKormoranServer _kormoranServer;

		private readonly AsyncRelayCommand _refreshMatchesCommand;
		public AsyncRelayCommand RefreshMatchesCommand
			=> _refreshMatchesCommand;

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
		}
	}
}
