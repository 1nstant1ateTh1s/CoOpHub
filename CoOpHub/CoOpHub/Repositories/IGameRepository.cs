using System.Collections.Generic;
using CoOpHub.Models;

namespace CoOpHub.Repositories
{
	public interface IGameRepository
	{
		IEnumerable<Game> GetGames();
	}
}