using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
	{
		public ApplicationUserConfiguration()
		{
			/* KEY CONFIGURATION SECTION */

			/* PROPERTY CONFIGURATION SECTION */
			Property(u => u.Name)
				.IsRequired()
				.HasMaxLength(100);

			/* RELATIONSHIP CONFIGURATION SECTION */
			// Fluent API - turn off cascade delete between Followers & Followees
			HasMany(u => u.Followers) // --> ea. Followee can have many Followers
				.WithRequired(f => f.Followee)
				.WillCascadeOnDelete(false);

			// Fluent API - turn off cascade delete between Followees & Followers
			HasMany(u => u.Followees) // --> ea. Follower can have many Followees
				.WithRequired(f => f.Follower)
				.WillCascadeOnDelete(false);
		}
	}
}