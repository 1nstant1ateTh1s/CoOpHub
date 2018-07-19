using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class GenreConfiguration : EntityTypeConfiguration<Genre>
	{
		public GenreConfiguration()
		{
			/* KEY CONFIGURATION SECTION */

			/* PROPERTY CONFIGURATION SECTION */
			Property(g => g.Name)
				.IsRequired()
				.HasMaxLength(255);

			/* RELATIONSHIP CONFIGURATION SECTION */
		}
	}
}