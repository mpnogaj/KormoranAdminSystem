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

		private int _team1Score;
		public int Team1Score
		{
			get => _team1Score;
			set => SetProperty(ref _team1Score, value);
		}

		private int _team2Score;
		public int Team2Score
		{
			get => _team2Score;
			set => SetProperty(ref _team2Score, value);
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
			_team1Score = match.Team1Score;
			_team2Score = match.Team2Score;
			_closePopup = closePopup;
			_cancelCommand = new RelayCommand(() => _closePopup(null));
			_saveCommand = new RelayCommand(() => _closePopup(new UpdateScoreRequestModel
			{
				MatchId = match.MatchId,
				Team1Score = Team1Score,
				Team2Score = Team2Score
			}));
		}
	}
}
