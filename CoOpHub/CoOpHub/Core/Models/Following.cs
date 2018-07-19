namespace CoOpHub.Core.Models
{
	public class Following
	{
		public ApplicationUser Follower { get; set; }
		public ApplicationUser Followee { get; set; }

		/* Define "composite" keys */
		public string FollowerId { get; set; }
		public string FolloweeId { get; set; }
	}
}