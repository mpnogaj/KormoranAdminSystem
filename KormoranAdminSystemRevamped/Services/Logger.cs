using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using KormoranAdminSystemRevamped.Contexts;

namespace KormoranAdminSystemRevamped.Services
{
	public interface ILogger
	{
		public Task LogNormal(LogEntry log);
		public Task LogMinor(LogEntry log);
		public Task LogMajor(LogEntry log);
	}
	
	public class Logger : ILogger
	{
		private readonly KormoranContext _db;
		public Logger(KormoranContext db)
		{
			_db = db;
		}
		
		public async Task LogNormal(LogEntry log)
		{
			log.Level = 1;
			await AddEntryToDb(log);
		}

		public async Task LogMinor(LogEntry log)
		{
			log.Level = 2;
			await AddEntryToDb(log);
		}

		public async Task LogMajor(LogEntry log)
		{
			log.Level = 3;
			await AddEntryToDb(log);
		}

		private async Task AddEntryToDb(LogEntry log)
		{
			await _db.Logs.AddAsync(log);
			await _db.SaveChangesAsync();
		}
	}

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