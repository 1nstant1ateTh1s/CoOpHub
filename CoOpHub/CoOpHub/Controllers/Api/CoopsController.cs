using CoOpHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	// Coops API controller
	[Authorize]
	public class CoopsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public CoopsController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Logically deletes (as opposed to physically deleting) the co-op session record.
		/// </summary>
		/// <param name="id">Id of the co-op session to cancel.</param>
		/// <returns></returns>
		[HttpDelete]
		public IHttpActionResult Cancel(int id)
		{
			// Retrieve the co-op session entity only if it is hosted by the currently logged in user
			var userId = User.Identity.GetUserId();

			// Use eager loading to get co-op session + all of it's attendees
			var coop = _unitOfWork.Coops.GetCoopWithAttendees(id);

			// Security checks on returned co-op session:
			if (coop == null || coop.IsCanceled)
			{
				// Co-op session not found, or if a co-op session has already been canceled, treat it like a record that does not exist in the DB
				return NotFound();
			}
			
			if (coop.HostId != userId)
			{
				// Co-op session does not belong to the current user
				return Unauthorized();
			}

			// Cancel the co-op session
			coop.Cancel();

			_unitOfWork.Complete();

			return Ok();
		}
	}
}
