using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface ICoopRepository
	{
		void Add(Coop coop);
		Coop GetCoop(int coopId);
		IEnumerable<Coop> GetCoopsUserAttending(string userId);
		Coop GetCoopWithAttendees(int coopId);
		IEnumerable<Coop> GetUpcomingCoopsByHost(string userId);
		IEnumerable<Coop> GetUpcomingCoops(string searchTerm = null);
	}
}