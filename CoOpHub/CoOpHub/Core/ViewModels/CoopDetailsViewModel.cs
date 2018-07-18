using CoOpHub.Core.Models;

namespace CoOpHub.Core.ViewModels
{
	public class CoopDetailsViewModel
	{
		public Coop Coop { get; set; }
		public bool IsFollowing { get; set; }
		public bool IsAttending { get; set; }
	}
}