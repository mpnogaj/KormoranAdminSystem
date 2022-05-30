using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KormoranMobile.Maui.ViewModels
{
	[QueryProperty(nameof(MatchReceiver), "match")]
	public class EditScoresPageViewModel : ViewModelBase
	{
		public string MatchReceiver
		{
			set => Match = JsonSerializer.Deserialize<Match>(value) ??
				throw new Exception("Match cannot be null!");
		}

		private Match _match;
		public Match Match
		{
			get => _match;
			set => SetProperty(ref _match, value);
		}
	}
}
