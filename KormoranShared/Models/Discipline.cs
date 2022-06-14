using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace KormoranShared.Models
{
	[Table("disciplines")]
	public class Discipline
	{
		[Key]
		[Column("discipline_id")]
		public int Id { get; set; }

		[Required]
		[Column("name")]
		[NotNull]
		public string? Name { get; set; }

		public override string ToString()
		{
			return Name ?? string.Empty;
		}
	}
}