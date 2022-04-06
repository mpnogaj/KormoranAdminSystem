using KormoranAdminSystemRevamped.Models;
using KormoranAdminSystemRevamped.Services;
using KormoranShared.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KormoranAdminSystemRevamped.Contexts
{
	public class KormoranContext : DbContext
	{
		public KormoranContext(DbContextOptions<KormoranContext> options)
			: base(options)
		{
		}

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Tournament> Tournaments { get; set; }
		public virtual DbSet<Match> Matches { get; set; }
		public virtual DbSet<Team> Teams { get; set; }
		public virtual DbSet<LogEntry> Logs { get; set; }
		public virtual DbSet<Discipline> Disciplines { get; set; }
		public virtual DbSet<State> States { get; set; }
	}
}
