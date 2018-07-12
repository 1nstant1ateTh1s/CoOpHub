using CoOpHub.Dtos;
using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class AttendancesController : ApiController
	{
		private ApplicationDbContext _context;

		public AttendancesController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult Attend(AttendanceDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Check if attendance already exists in db
			if (_context.Attendances.Any(a => a.AttendeeId == userId && a.CoopId == dto.CoopId))
			{
				return BadRequest("The attendance already exists.");
			}

			var attendance = new Attendance
			{
				CoopId = dto.CoopId,
				AttendeeId = userId
			};

			_context.Attendances.Add(attendance);
			_context.SaveChanges();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteAttendance(int id)
		{
			var userId = User.Identity.GetUserId();

			// Get the attendance for the specified co-op session Id
			var attendance = _context.Attendances
				.SingleOrDefault(a => a.CoopId == id && a.AttendeeId == userId);

			// Check if attendance exists
			if (attendance == null)
			{
				return NotFound();
			}

			// Remove the attendance entity & save changes
			_context.Attendances.Remove(attendance);
			_context.SaveChanges();

			// Return ok with id of the attendance in the response
			return Ok(id);
		}
	}
}
