using CoOpHub.Models;

namespace CoOpHub.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing(string userId, string hostId);
	}
}