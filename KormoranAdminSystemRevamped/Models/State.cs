using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KormoranAdminSystemRevamped.Models
{
    public class State
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}