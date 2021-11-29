using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KormoranAdminSystemRevamped.Models
{
	[Table("disciplines")]
	public class Discipline
	{
		[Key]
		[Column("discipline_id", TypeName = "int(11)")]
		public int Id { get; set; }
		
		[Required]
		[Column("name")]
		public string Name { get; set; } = "";
	}
}