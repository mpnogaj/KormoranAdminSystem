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
	}
}
