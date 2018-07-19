using CoOpHub.Core.Models;
using CoOpHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CoOpHub.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Coop> Coops { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Following> Followings { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; } 

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
			// Add Fluent API configurations:
			modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
			modelBuilder.Configurations.Add(new AttendanceConfiguration());
			modelBuilder.Configurations.Add(new CoopConfiguration());
			modelBuilder.Configurations.Add(new FollowingConfiguration());
			modelBuilder.Configurations.Add(new GameConfiguration());
			modelBuilder.Configurations.Add(new GenreConfiguration());
			modelBuilder.Configurations.Add(new NotificationConfiguration());
			modelBuilder.Configurations.Add(new UserNotificationConfiguration());

			//// Fluent API - turn off cascade delete between Coops & Attendance
			//modelBuilder.Entity<Attendance>()
			//	.HasRequired(a => a.Coop)
			//	.WithMany(c => c.Attendances)
			//	.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}