using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KormoranAdminSystemRevamped.Models
{
	[Table("tournaments")]
	public class Tournament
	{
		[Key]
		[Column("id", TypeName = "int(11)")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("game")]
		public string Game { get; set; }

		[Column("state")]
		public string State { get; set; }

		[Column("tournament_type")]
		public string TournamentType { get; set; }

		[Column("tournament_type_short")]
		public string TournamentTypeShort { get; set; }
	}
}
