using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranAdminSystemRevamped.Models
{
    [NotMapped]
    public class LeaderboardEntry
    {
        public Team? Team { get; set; }
        public int Wins { get; set; }
    }
}
