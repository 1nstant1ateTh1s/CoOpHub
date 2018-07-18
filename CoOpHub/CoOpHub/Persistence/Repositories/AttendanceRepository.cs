using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly ApplicationDbContext _context;

		public AttendanceRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get an attendance.
		/// </summary>
		/// <param name="gigId">The coop id of the attendance to get.</param>
		/// <param name="userId">The user of the attendance to get.</param>
		/// <returns>An attendance.</returns>
		public Attendance GetAttendance(int coopId, string userId)
		{
			return _context.Attendances
					.SingleOrDefault(a => a.CoopId == coopId && a.AttendeeId == userId);
		}

		/// <summary>
		/// Get attendances for future co-op sessions for the specified user.
		/// </summary>
		/// <param name="userId">The user to get future attendances for.</param>
		/// <returns>IEnumerable of attendances.</returns>
		public IEnumerable<Attendance> GetFutureAttendances(string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Coop.DateTime > DateTime.Now) // get attendances for future co-op sessions only
				.ToList(); // .ToList() = immediately execute query
		}
	}
}