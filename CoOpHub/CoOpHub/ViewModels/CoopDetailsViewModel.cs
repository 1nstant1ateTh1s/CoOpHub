using CoOpHub.Models;

namespace CoOpHub.ViewModels
{
	public class CoopDetailsViewModel
	{
		public Coop Coop { get; set; }
		public bool IsFollowing { get; set; }
		public bool IsAttending { get; set; }
	}
}