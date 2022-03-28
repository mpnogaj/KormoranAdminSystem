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
		
		[Column("discipline_id")]
		[ForeignKey("Discipline")]
		[JsonIgnore]
		public int DisciplineId { get; set; }
		public virtual Discipline Discipline { get; set; }
		
		[Column("state_id")]
		[ForeignKey("State")]
		[JsonIgnore]
		public int StateId { get; set; }
		public virtual State State { get; set; }
		public virtual ICollection<Match> Matches { get; set; }
		public virtual ICollection<Team> Teams { get; set; }
	}
}
