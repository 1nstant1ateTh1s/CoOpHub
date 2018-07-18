using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOpHub.Core.Models
{
	public class Attendance
	{
		public Coop Coop { get; set; }
		public ApplicationUser Attendee { get; set; }

		/* Define "composite" keys */
		[Key]
		[Column(Order = 1)]
		public int CoopId { get; set; }

		[Key]
		[Column(Order = 2)]
		public string AttendeeId { get; set; }
	}
}