using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface IApplicationUserRepository
	{
		IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId);
	}
}
