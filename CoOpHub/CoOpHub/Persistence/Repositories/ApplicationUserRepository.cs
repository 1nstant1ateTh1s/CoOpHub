using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
{
	public class ApplicationUserRepository : IApplicationUserRepository
	{
		private readonly ApplicationDbContext _context;

		public ApplicationUserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get artists followed by a specified user.
		/// </summary>
		/// <param name="userId">The user to get followed artists for.</param>
		/// <returns>IEnumerable of users.</returns>
		public IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId)
		{
			return _context.Followings
				.Where(f => f.FollowerId == userId)
				.Select(f => f.Followee)
				.ToList();
		}
	}
}