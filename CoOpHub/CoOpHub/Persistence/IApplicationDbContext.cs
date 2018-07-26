using CoOpHub.Core.Models;
using System.Data.Entity;

namespace CoOpHub.Persistence
{
	public interface IApplicationDbContext
	{
		DbSet<Attendance> Attendances { get; set; }
		DbSet<Coop> Coops { get; set; }
		DbSet<Following> Followings { get; set; }
		DbSet<Game> Games { get; set; }
		DbSet<Genre> Genres { get; set; }
		DbSet<Notification> Notifications { get; set; }
		DbSet<UserNotification> UserNotifications { get; set; }
		IDbSet<ApplicationUser> Users { get; set; }
	}
}