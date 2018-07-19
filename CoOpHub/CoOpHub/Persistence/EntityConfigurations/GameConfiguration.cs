using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class GameConfiguration : EntityTypeConfiguration<Game>
	{
		public GameConfiguration()
		{
			/* KEY CONFIGURATION SECTION */

			/* PROPERTY CONFIGURATION SECTION */
			Property(g => g.GenreId)
				.IsRequired();

			Property(g => g.Name)
				.IsRequired()
				.HasMaxLength(255);

			/* RELATIONSHIP CONFIGURATION SECTION */
		}
	}
}