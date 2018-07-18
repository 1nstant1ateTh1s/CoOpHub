using CoOpHub.Core.Models;

namespace CoOpHub.Core.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing(string userId, string hostId);
	}
}