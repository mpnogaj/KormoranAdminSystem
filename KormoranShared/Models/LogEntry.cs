using KormoranShared.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace KormoranShared.Models
{
	[Table("logs")]
	public class LogEntry
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("level")]
		public int Level { get; set; }

		[Column("date")]
		[NotNull]
		public string Date { get; set; }

		[Column("author")]
		[NotNull]
		public string Author { get; set; }

		[Column("action")]
		[NotNull]
		public string Action { get; set; }

		//Default ctor for EF
#pragma warning disable CS8618
		public LogEntry()
		{

		}
#pragma warning restore CS8618

		public LogEntry(string author, string action)
		{
			Date = DateTime.Now.SerializeDate();
			Author = author;
			Action = action;
		}

		public LogEntry(string author, string action, DateTime date)
		{
			Date = date.SerializeDate();
			Author = author;
			Action = action;
		}
	}
}