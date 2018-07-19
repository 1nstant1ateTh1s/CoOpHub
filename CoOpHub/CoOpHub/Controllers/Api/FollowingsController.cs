using CoOpHub.Core;
using CoOpHub.Core.Dtos;
using CoOpHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class FollowingsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public FollowingsController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Check if following already exists in db, before adding new
			var following = _unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);
			if (following != null)
			{
				return BadRequest("Following already exists.");
			}

			following = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_unitOfWork.Followings.Add(following);
			_unitOfWork.Complete();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult Unfollow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Get the following for the specified host
			var following = _unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);

			// Check if following exists
			if (following == null)
			{
				return NotFound();
			}

			_unitOfWork.Followings.Remove(following);
			_unitOfWork.Complete();

			// Return ok with id of the following in the response
			return Ok(dto.FolloweeId);
		}
	}
}
