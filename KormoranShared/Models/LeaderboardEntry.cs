using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranShared.Models
{
    [NotMapped]
    public class LeaderboardEntry
    {
        public Team Team { get; set; }
        public int Wins { get; set; }

        public override string ToString()
        {
            if (Team == null)
            {
                return string.Empty;
            }
            return $"{Team} - {Wins}";
        }
    }
}
