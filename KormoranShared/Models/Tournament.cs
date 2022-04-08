using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KormoranShared.Models
{
    [Table("tournaments")]
    public class Tournament
    {
        [Key]
        [Column("tournament_id", TypeName = "int(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TournamentId { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("discipline_id")]
        [ForeignKey("Discipline")]
        [JsonIgnore]
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        [Column("state_id")]
        [ForeignKey("State")]
        [JsonIgnore]
        public int StateId { get; set; }
        public State State { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Team> Teams { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
