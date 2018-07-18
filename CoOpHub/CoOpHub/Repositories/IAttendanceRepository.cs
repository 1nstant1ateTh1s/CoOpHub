using System.Collections.Generic;
using CoOpHub.Models;

namespace CoOpHub.Repositories
{
	public interface IAttendanceRepository
	{
		Attendance GetAttendance(int coopId, string userId);
		IEnumerable<Attendance> GetFutureAttendances(string userId);
	}
}