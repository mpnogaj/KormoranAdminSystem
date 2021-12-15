using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace KormoranAdminSystemRevamped.Models
{
	[Table("tournaments")]
	public class Tournament
	{
		[Key]
		[Column("tournament_id", TypeName = "int(11)")]
		public int Id { get; set; }

		[Column("name")]
		[Required]
		public string Name { get; set; }
		
		[JsonIgnore]
		[Column("discipline_id")]
		[ForeignKey("Discipline")]
		public int DisciplineId { get; set; }
		public virtual Discipline Discipline { get; set; }
		
		[JsonIgnore]
		[Column("state_id")]
		[ForeignKey("State")]
		public int StateId { get; set; }
		public virtual State State { get; set; }
		public virtual ICollection<Match> Matches { get; set; }
		public virtual ICollection<Team> Teams { get; set; }

		[Required]
		[Column("tournament_type")]
		public string TournamentType { get; set; }

		[Required]
		[Column("tournament_type_short")]
		public string TournamentTypeShort { get; set; } = "";
	}
}
