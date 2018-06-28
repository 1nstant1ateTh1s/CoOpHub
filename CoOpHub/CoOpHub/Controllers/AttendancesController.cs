using CoOpHub.Dtos;
using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers
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
	}
}
