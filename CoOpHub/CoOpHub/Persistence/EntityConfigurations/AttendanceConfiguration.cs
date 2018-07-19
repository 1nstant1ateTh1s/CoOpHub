using CoOpHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CoOpHub.Persistence.EntityConfigurations
{
	public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
	{
		public AttendanceConfiguration()
		{
			/* KEY CONFIGURATION SECTION */
			HasKey(a => new { a.CoopId, a.AttendeeId });

			/* PROPERTY CONFIGURATION SECTION */

			/* RELATIONSHIP CONFIGURATION SECTION */
		}
	}
}