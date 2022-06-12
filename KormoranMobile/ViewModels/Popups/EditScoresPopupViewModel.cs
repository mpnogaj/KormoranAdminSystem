using KormoranMobile.ViewModels.Abstraction;
using KormoranMobile.ViewModels.Commands;
using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;

namespace KormoranMobile.ViewModels.Popups
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

		#region Commands

		public RelayCommand CancelCommand { get; }

		public RelayCommand SaveCommand { get; }

		#endregion

		public EditScoresPopupViewModel(Match match, Action<UpdateScoreRequestModel?> closePopup)
		{
			_match = match;
			_team1Score = match.Team1Score;
			_team2Score = match.Team2Score;
			CancelCommand = new RelayCommand(() => closePopup(null));
			SaveCommand = new RelayCommand(() => closePopup(new UpdateScoreRequestModel
			{
				MatchId = match.MatchId,
				Team1Score = Team1Score,
				Team2Score = Team2Score
			}));
		}
	}
}
