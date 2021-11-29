using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace KormoranAdminSystemRevamped.Models
{
	[Table("teams")]
	public class Team
	{
		[Key]
		[Column("team_id", TypeName = "int(11)")]
		public int Id { get; set; }
		
		[Required]
		[Column("name")]
		public string Name { get; set; }
		
		[JsonIgnore]
		[ForeignKey("Tournament")]
		[Column("tournament_id")]
		public int TournamentId { get; set; }
		[JsonIgnore]
		public Tournament Tournament { get; set; }
	}
}