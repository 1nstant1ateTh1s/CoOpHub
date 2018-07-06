using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	// Coops API controller
	[Authorize]
	public class CoopsController : ApiController
	{
		private ApplicationDbContext _context;

		public CoopsController()
		{
			_context = new ApplicationDbContext();
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
			var coop = _context.Coops
				.Include(c => c.Attendances.Select(a => a.Attendee)) // include any users who were attending this co-op session
				.Single(c => c.Id == id && c.HostId == userId);

			// If a co-op session has already been canceled, treat it like a record that does not exist in the DB
			if (coop.IsCanceled)
			{
				return NotFound();
			}

			// Cancel the co-op session
			coop.Cancel();

			// Save changes
			_context.SaveChanges();

			return Ok();
		}
	}
}
