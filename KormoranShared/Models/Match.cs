using KormoranShared.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace KormoranShared.Models
{
	[Table("matches")]
	public class Match
	{
		[Key]
		[Column("match_id")]
		public int MatchId { get; set; }

		[ForeignKey("Tournament")]
		[Column("tournament_id")]
		public int TournamentId { get; set; }

		[JsonIgnore]
		[NotNull]
		public virtual Tournament? Tournament { get; set; }

		[ForeignKey("State")]
		[Column("state_id")]
		[JsonIgnore]
		public int StateId { get; set; }

		public State State { get; set; }

		[ForeignKey("Team1")]
		[Column("team_1_id")]
		[JsonIgnore]
		public int Team1Id { get; set; }

		public Team Team1 { get; set; }

		[ForeignKey("Team2")]
		[Column("team_2_id")]
		[JsonIgnore]
		public int Team2Id { get; set; }

		public Team Team2 { get; set; }

		[NotMapped]
		public Team? Winner
		{
			get
			{
				if (Team2 == null || Team1 == null) return null;
				return Team1Score > Team2Score ? Team1 : Team1Score < Team2Score ? Team2 : new Team
				{
					Id = 0,
					Name = "-"
				};
			}
		}

		[Column("team_1_score")]
		public int Team1Score { get; set; }

		[Column("team_2_score")]
		public int Team2Score { get; set; }

		public override string ToString()
		{
			return this.Serialize();
		}
	}
}