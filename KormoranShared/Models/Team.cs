using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KormoranShared.Models
{
	[Table("teams")]
    public class Team
    {
		[Key]
		[Column("team_id", TypeName = "int(11)")]
		public int Id { get; set; }

		[Required]
		[Column("name")]
		public string? Name { get; set; }

		[ForeignKey("Tournament")]
		[Column("tournament_id")]
		public int TournamentId { get; set; }
		[JsonIgnore]
		public virtual Tournament? Tournament { get; set; }

        public override string ToString()
        {
			return Name ?? string.Empty;
        }
    }
}
