using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace KormoranAdminSystemRevamped.Models
{
	[Table("matches")]
	public class Match
	{
		[Key]
		[Column("match_id", TypeName = "int(11)")]
		public int MatchId { get; set; }
		
		[JsonIgnore]
		[ForeignKey("Tournament")]
		[Column("tournament_id")]
		public int TournamentId { get; set; }
		[JsonIgnore]
		public virtual Tournament Tournament { get; set; }
		
		
		[JsonIgnore]
		[ForeignKey("State")]
		[Column("state_id")]
		public int StateId { get; set; }
		public virtual State State { get; set; }
		
		[JsonIgnore]
		[ForeignKey("Team1")]
		[Column("team_1_id")]
		public int Team1Id { get; set; }
		public virtual Team Team1 { get; set; }
		
		[JsonIgnore]
		[ForeignKey("Team2")]
		[Column("team_2_id")]
		public int Team2Id { get; set; }
		public virtual Team Team2 { get; set; }
		
		[JsonIgnore]
		[ForeignKey("Winner")]
		[Column("winner_id")]
		public int WinnerId { get; set; }
		public virtual Team Winner { get; set; }
		
		[Column("team_1_score", TypeName = "int(11)")]
		public int Team1Score { get; set; }
		
		[Column("team_2_score", TypeName = "int(11)")]
		public int Team2Score { get; set; }
	}
}