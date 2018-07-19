namespace CoOpHub.Core.Models
{
	public class Attendance
	{
		public Coop Coop { get; set; }
		public ApplicationUser Attendee { get; set; }

		/* Define "composite" keys */
		public int CoopId { get; set; }
		public string AttendeeId { get; set; }
	}
}