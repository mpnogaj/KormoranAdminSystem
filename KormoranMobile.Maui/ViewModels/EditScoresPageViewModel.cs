using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranShared.Models;

namespace KormoranMobile.Maui.ViewModels
{
	public class EditScoresPageViewModel : ViewModelBase
	{
		private Match _match;
		public Match Match
		{
			get => _match;
			set => SetProperty(ref _match, value);
		}

		public EditScoresPageViewModel(Match match)
		{
			_match = match;
		}
	}
}
