using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class FollowingConfiguration : EntityTypeConfiguration<Following>
	{
		public FollowingConfiguration()
		{
			/* KEY CONFIGURATION SECTION */
			HasKey(f => new { f.FollowerId, f.FolloweeId });

			/* PROPERTY CONFIGURATION SECTION */

			/* RELATIONSHIP CONFIGURATION SECTION */
		}
	}
}