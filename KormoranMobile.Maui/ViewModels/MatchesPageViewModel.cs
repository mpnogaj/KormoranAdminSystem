using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Services;
using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranShared.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KormoranMobile.Maui.ViewModels
{
	[QueryProperty(nameof(Tournament), "tournamentId")]
	public class MatchesPageViewModel : ViewModelBase
	{
		private readonly IKormoranServer _kormoranServer;

		public int TournamentId { get; set; }

		private Tournament _tournament;
		public Tournament Tournament;

		public MatchesPageViewModel()
		{
			_kormoranServer = RestService.For<IKormoranServer>(ServerHelper.DefaultHttpClient);
		}

	}
}
