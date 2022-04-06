using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public DateTime Date { get; set; }
		[Column("author")]
		public string Author { get; set; }
		[Column("action")]
		public string Action { get; set; }

		public LogEntry(DateTime date, string author, string action)
		{
			Date = date;
			Author = author;
			Action = action;
		}
	}
}