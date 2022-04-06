using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KormoranShared.Models
{
    [Table("matches")]
    public class Match
    {
        [Key]
        [Column("match_id", TypeName = "int(11)")]
        public int MatchId { get; set; }

        [ForeignKey("Tournament")]
        [Column("tournament_id")]
        public int TournamentId { get; set; }
        [JsonIgnore]
        public virtual Tournament? Tournament { get; set; }

        [ForeignKey("State")]
        [Column("state_id")]
        [JsonIgnore]
        public int StateId { get; set; }
        public virtual State? State { get; set; }

        [ForeignKey("Team1")]
        [Column("team_1_id")]
        [JsonIgnore]
        public int Team1Id { get; set; }
        public virtual Team? Team1 { get; set; }

        [ForeignKey("Team2")]
        [Column("team_2_id")]
        [JsonIgnore]
        public int Team2Id { get; set; }
        public virtual Team? Team2 { get; set; }

        [NotMapped]
        public virtual Team Winner
        {
            get
            {
                if (Team2 == null || Team1 == null) return null;
                return Team1Score >= Team2Score ? Team1 : Team2;
            }
        }

        [Column("team_1_score", TypeName = "int(11)")]
        public int Team1Score { get; set; }

        [Column("team_2_score", TypeName = "int(11)")]
        public int Team2Score { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}