using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CoOpHub.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Coop> Coops { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}