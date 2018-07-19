using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class NotificationConfiguration : EntityTypeConfiguration<Notification>
	{
		public NotificationConfiguration()
		{
			/* KEY CONFIGURATION SECTION */

			/* PROPERTY CONFIGURATION SECTION */
			Property(n => n.CoopId)
				.IsRequired();

			/* RELATIONSHIP CONFIGURATION SECTION */
			//HasRequired(n => n.Coop);

			// Fluent API - turn off cascade delete between Notifications & Games
			HasOptional(n => n.OriginalGame)
				.WithMany()
				.WillCascadeOnDelete(false);
		}
	}
}