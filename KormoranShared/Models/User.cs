using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranShared.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id", TypeName = "int(11)")]
        public int Id { get; set; }

        [Required]
        [Column("user")]
        public string Login { get; set; }

        [Required]
        [Column("pass")]
        public string PasswordHash { get; set; }

        [Required]
        [Column("fullname")]
        public string Fullname { get; set; }

        [Column("permissions", TypeName = "json")]
        public string Permissions { get; set; }

        public override string ToString()
        {
            return $"{Login} - {Fullname}";
        }
    }
}