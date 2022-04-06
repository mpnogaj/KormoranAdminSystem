using System.Threading.Tasks;
using KormoranAdminSystemRevamped.Contexts;
using KormoranShared.Models;

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
}