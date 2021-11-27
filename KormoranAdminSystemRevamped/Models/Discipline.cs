using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranAdminSystemRevamped.Models
{
    [Table("discipline")]
    public class Discipline
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; } = "";
    }
}