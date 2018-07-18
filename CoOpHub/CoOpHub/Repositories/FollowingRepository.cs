using CoOpHub.Models;
using System.Linq;

namespace CoOpHub.Repositories
{
	public class FollowingRepository : IFollowingRepository
	{
		private readonly ApplicationDbContext _context;

		public FollowingRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get a following.
		/// </summary>
		/// <param name="userId">The follower id of the following to get.</param>
		/// <param name="hostId">The followee id of the following to get.</param>
		/// <returns>A following.</returns>
		public Following GetFollowing(string userId, string hostId)
		{
			return _context.Followings
					.SingleOrDefault(f => f.FolloweeId == hostId && f.FollowerId == userId);
		}
	}
}