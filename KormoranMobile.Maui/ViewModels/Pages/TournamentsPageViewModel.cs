using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranMobile.Maui.Views.Pages;
using KormoranMobile.Maui.Views.Popups;
using KormoranShared.Models;
using KormoranShared.Models.Responses;
using Refit;
using System.Collections.ObjectModel;
using System.Diagnostics;
using KormoranMobile.Maui.Properties;

namespace KormoranMobile.Maui.ViewModels.Pages
{
	public class TournamentsPageViewModel : ViewModelBase
	{
		private IKormoranServer? _kormoranServer;
		private ObservableCollection<Tournament> _tournaments;
		private bool _isRefreshing;

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public ObservableCollection<Tournament> Tournaments
		{
			get => _tournaments;
			private set => SetProperty(ref _tournaments, value);
		}

		public AsyncRelayCommand ShowServerPopupCommand { get; }

		public AsyncRelayCommand ShowLoginPageCommand { get; }

		public AsyncRelayCommand<Tournament> ItemTappedCommand { get; }

		public AsyncRelayCommand RefreshTournamentsListCommand { get; }

		public Action OnAppearing => () => RefreshTournamentsList(false).FireAndForgetAsync(new LogErrorHandler());

		public TournamentsPageViewModel()
		{
			_isRefreshing = false;
			_tournaments = new ObservableCollection<Tournament>();

			ShowServerPopupCommand = new AsyncRelayCommand(async () =>
			{
				object? res = await PopupHelper.ShowPopupAsync(new SettingsPopup());
				if (res != null)
				{
					Preferences.Set(ServerHelper.AddressKey, (string)res);
					await SetupServer();
				}
			});

			ShowLoginPageCommand = new AsyncRelayCommand(async () =>
			{
				await PopupHelper.ShowPopupAsync(new LoginPopup());
			}, () => _kormoranServer != null);

			ItemTappedCommand = new AsyncRelayCommand<Tournament>(async (tournament) =>
			{
				try
				{
					await Shell.Current.GoToAsync($"{nameof(MatchesPage)}?tournament={tournament!}");
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			});
			RefreshTournamentsListCommand = new AsyncRelayCommand(async () =>
			{
				await RefreshTournamentsList(true);
			}, () => IsRefreshing == false && _kormoranServer != null);

			try
			{
				SetupServer().Wait();
			}
			catch
			{
				_kormoranServer = null;
				Preferences.Remove(ServerHelper.AddressKey);
			}
		}

		private async Task RefreshTournamentsList(bool showProgress)
		{
			if (_kormoranServer == null || !ServerHelper.AddressSet)
			{
				await Toast.Make(Resources.ServerAddressNotSetError).Show();
				return;
			}

			try
			{
				if (showProgress)
				{
					IsRefreshing = true;
				}

				CollectionResponse<Tournament> response =
					await _kormoranServer.GetTournaments();
				if (response.Error)
				{
					await Toast.Make(response.Message, ToastDuration.Long).Show();
				}
				else
				{
					Tournaments = new ObservableCollection<Tournament>(response.Collection);
				}
			}
			catch (Exception ex)
			{
				await Toast.Make(ex.Message, ToastDuration.Long).Show();
			}
			finally
			{
				if (showProgress)
				{
					IsRefreshing = false;
				}
			}
		}

		private async Task<bool> SetupServer()
		{
			if (!ServerHelper.AddressSet)
			{
				await Toast.Make(Resources.ServerAddressNotSetError).Show();
				_kormoranServer = null;
				return false;
			}
			else
			{
				try
				{
					_kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
				}
				catch
				{
					_kormoranServer = null;
					await Toast.Make(Resources.ServerAddressIncorrect).Show();
				}
			}

			ShowLoginPageCommand.RaiseCanExecuteChanged();
			RefreshTournamentsListCommand.RaiseCanExecuteChanged();
			return _kormoranServer != null;
		}
	}
}