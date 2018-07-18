using CoOpHub.Core.Models;
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
			// Fluent API - turn off cascade delete between Coops & Attendance
			modelBuilder.Entity<Attendance>()
				.HasRequired(a => a.Coop)
				.WithMany(c => c.Attendances)
				.WillCascadeOnDelete(false);

			// Fluent API - turn off cascade delete between Followers & Followees
			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followers)
				.WithRequired(f => f.Followee)
				.WillCascadeOnDelete(false);

			// Fluent API - turn off cascade delete between Followees & Followers
			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Followees)
				.WithRequired(f => f.Follower)
				.WillCascadeOnDelete(false);

			// Fluent API - turn off cascade delete between UserNotifications & Users
			modelBuilder.Entity<UserNotification>()
				.HasRequired(n => n.User)			// --> ea. UserNotification has one, and only one, User
				.WithMany(u => u.UserNotifications) // --> and the reverse of the relationship - ea. User can have many UserNotification's
				.WillCascadeOnDelete(false);

			// Fluent API - turn off cascade delete between Notifications & Games
			modelBuilder.Entity<Notification>()
				.HasOptional(n => n.OriginalGame)
				.WithMany()
				.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);
		}
	}
}