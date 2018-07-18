using System.Collections.Generic;
using CoOpHub.Models;

namespace CoOpHub.Repositories
{
	public interface ICoopRepository
	{
		void Add(Coop coop);
		Coop GetCoop(int coopId);
		IEnumerable<Coop> GetCoopsUserAttending(string userId);
		Coop GetCoopWithAttendees(int coopId);
		IEnumerable<Coop> GetUpcomingCoopsByHost(string userId);
	}
}