using KormoranMobile.Maui.ViewModels.Abstraction;
using KormoranMobile.Maui.ViewModels.Commands;
using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;

namespace KormoranMobile.Maui.ViewModels.Popups
{
	public class EditScoresPopupViewModel : ViewModelBase
	{
		#region MVVM Props
		private Match _match;
		public Match Match
		{
			get => _match;
			set => SetProperty(ref _match, value);
		}
		#endregion

		#region Buttons
		private readonly RelayCommand _cancelCommand;
		public RelayCommand CancelCommand => _cancelCommand;

		private readonly RelayCommand _saveCommand;
		public RelayCommand SaveCommand => _saveCommand;
		#endregion

		private readonly Action<UpdateScoreRequestModel?> _closePopup;
		public EditScoresPopupViewModel(Match match, Action<UpdateScoreRequestModel?> closePopup)
		{
			_match = match;
			_closePopup = closePopup;
			_cancelCommand = new RelayCommand(() => _closePopup(null));
			_saveCommand = new RelayCommand(() => _closePopup(new UpdateScoreRequestModel
			{
				MatchId = match.MatchId,
				Team1Score = match.Team1Score,
				Team2Score = match.Team2Score
			}));
		}
	}
}
