using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class CoopConfiguration : EntityTypeConfiguration<Coop>
	{
		public CoopConfiguration()
		{
			/* KEY CONFIGURATION SECTION */

			/* PROPERTY CONFIGURATION SECTION */
			Property(c => c.GameId) // *Property is part of the EntityTypeConfiguration class that we inherit
				.IsRequired();

			Property(c => c.HostId)
				.IsRequired();

			Property(c => c.Venue)
				.IsRequired()
				.HasMaxLength(255);

			/* RELATIONSHIP CONFIGURATION SECTION */
			// Fluent API - turn off cascade delete between Coops & Attendance
			HasMany(c => c.Attendances)
				.WithRequired(a => a.Coop)
				.WillCascadeOnDelete(false);
		}
	}
}