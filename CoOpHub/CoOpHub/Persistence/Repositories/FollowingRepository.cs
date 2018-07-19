using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
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

		/// <summary>
		/// Add a following.
		/// </summary>
		/// <param name="following">The following to add.</param>
		public void Add(Following following)
		{
			_context.Followings.Add(following);
		}

		/// <summary>
		/// Remove a following.
		/// </summary>
		/// <param name="gig">The following to remove.</param>
		public void Remove(Following following)
		{
			_context.Followings.Remove(following);
		}
	}
}