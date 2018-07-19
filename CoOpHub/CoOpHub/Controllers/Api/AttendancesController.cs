using CoOpHub.Core;
using CoOpHub.Core.Dtos;
using CoOpHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class AttendancesController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public AttendancesController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		public IHttpActionResult Attend(AttendanceDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Check if attendance already exists in db, before creating new
			var attendance = _unitOfWork.Attendances.GetAttendance(dto.CoopId, userId);
			if (attendance != null)
			{
				return BadRequest("The attendance already exists.");
			}

			attendance = new Attendance
			{
				CoopId = dto.CoopId,
				AttendeeId = userId
			};

			_unitOfWork.Attendances.Add(attendance);
			_unitOfWork.Complete();

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteAttendance(int id)
		{
			var userId = User.Identity.GetUserId();

			// Get the attendance for the specified co-op session Id
			var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

			// Check if attendance exists
			if (attendance == null)
			{
				return NotFound();
			}

			_unitOfWork.Attendances.Remove(attendance);
			_unitOfWork.Complete();

			// Return ok with id of the attendance in the response
			return Ok(id);
		}
	}
}
