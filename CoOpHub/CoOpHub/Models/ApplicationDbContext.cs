using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CoOpHub.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Coop> Coops { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Attendance> Attendances { get; set; }

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Fluent API - turn off cascade delete between Coops & Attendance
			modelBuilder.Entity<Attendance>()
				.HasRequired(a => a.Coop)
				.WithMany()
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}