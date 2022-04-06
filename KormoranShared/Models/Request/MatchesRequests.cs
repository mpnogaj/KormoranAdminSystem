namespace KormoranShared.Models.Requests.Matches
{
	public class UpdateMatchRequestModel
	{
		public int MatchId { get; set; }
		public int TournamentId { get; set; }
		public int StateId { get; set; }
		public int Team1 { get; set; }
		public int Team2 { get; set; }
		public int Team1Score { get; set; }
		public int Team2Score { get; set; }
	}

	public class IncrementScoreRequestModel
	{
		public int MatchId { get; set; }
		public int Team { get; set; }
		public int Value { get; set; }
	}
}