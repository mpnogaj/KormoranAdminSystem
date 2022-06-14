using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranShared.Models
{
	[Table("teams")]
	public class Team
	{
		[Key]
		[Column("team_id")]
		public int Id { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }

		[ForeignKey("Tournament")]
		[Column("tournament_id")]
		public int TournamentId { get; set; }

		public override string ToString()
		{
			return Name ?? string.Empty;
		}
	}
}