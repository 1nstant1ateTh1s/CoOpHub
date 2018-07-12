using CoOpHub.Dtos;
using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class FollowingsController : ApiController
	{
		private ApplicationDbContext _context;

		public FollowingsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Check if following already exists in db
			if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
			{
				return BadRequest("Following already exists.");
			}

			var following = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_context.Followings.Add(following);
			_context.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult Unfollow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Get the following for the specified artist
			var following = _context.Followings
				.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);

			// Check if following exists
			if (following == null)
			{
				return NotFound();
			}

			// Remove the attendance entity & save changes
			_context.Followings.Remove(following);
			_context.SaveChanges();

			// Return ok with id of the following in the response
			return Ok(dto.FolloweeId);
		}
	}
}
