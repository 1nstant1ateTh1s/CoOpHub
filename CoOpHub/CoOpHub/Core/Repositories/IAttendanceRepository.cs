using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface IAttendanceRepository
	{
		Attendance GetAttendance(int coopId, string userId);
		IEnumerable<Attendance> GetFutureAttendances(string userId);
	}
}