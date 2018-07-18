using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface IGameRepository
	{
		IEnumerable<Game> GetGames();
	}
}