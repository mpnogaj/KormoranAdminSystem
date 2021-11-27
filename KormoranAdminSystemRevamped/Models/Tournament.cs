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
		[Required]
		public string Name { get; set; }
		
		[Column("discipline")]
		[Required]
		public Discipline Discipline { get; set; }
		
		[Column("state")]
		[Required]
		public State State { get; set; }

		[Column("tournament_type")]
		[Required]
		public string TournamentType { get; set; }

		[Column("tournament_type_short")]
		[Required]
		public string TournamentTypeShort { get; set; } = "";
	}
}
