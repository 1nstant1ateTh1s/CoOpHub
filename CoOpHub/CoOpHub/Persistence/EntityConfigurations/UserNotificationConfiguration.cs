using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
	{
		public UserNotificationConfiguration()
		{
			/* KEY CONFIGURATION SECTION */
			HasKey(un => new { un.UserId, un.NotificationId });

			/* PROPERTY CONFIGURATION SECTION */

			/* RELATIONSHIP CONFIGURATION SECTION */
			// Fluent API - turn off cascade delete between UserNotifications & Users
			HasRequired(n => n.User)           // --> ea. UserNotification has one, and only one, User
				.WithMany(u => u.UserNotifications) // --> and the reverse of the relationship - ea. User can have many UserNotification's
				.WillCascadeOnDelete(false);
		}
	}
}