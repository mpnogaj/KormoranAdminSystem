using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranShared.Models;

namespace KormoranMobile.Maui.ViewModels.Popups
{
	public class EditScoresPopupViewModel : ViewModelBase
	{
		private Match _match;
		public Match Match
		{
			get => _match;
			set => SetProperty(ref _match, value);
		}

		public EditScoresPopupViewModel(Match match)
		{
			_match = match;
		}
	}
}
