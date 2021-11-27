using KormoranAdminSystemRevamped.Models;
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
		public virtual DbSet<Discipline> Disciplines { get; set; }
		public virtual DbSet<State> States { get; set; }
	}
}
