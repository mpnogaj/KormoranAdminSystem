using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace KormoranShared.Models
{
	[Table("users")]
	public class User
	{
		[Key]
		[Column("user_id")]
		public int Id { get; set; }

		[Required]
		[Column("user")]
		[NotNull]
		public string? Login { get; set; }

		[Required]
		[Column("pass")]
		[JsonIgnore]
		public string PasswordHash { get; set; }

		[Required]
		[Column("fullname")]
		public string Fullname { get; set; }

		[Required]
		[Column("is_admin")]
		public bool IsAdmin { get; set; }

		public override string ToString()
		{
			return $"{Login} - {Fullname}";
		}
	}
}