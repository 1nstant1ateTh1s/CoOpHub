using CoOpHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoOpHub.Repositories
{
	public class GameRepository : IGameRepository
	{
		private readonly ApplicationDbContext _context;

		public GameRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get all games.
		/// </summary>
		/// <returns>IEnumerable of games.</returns>
		public IEnumerable<Game> GetGames()
		{
			return _context.Games
				.Include(g => g.Genre) // eager load the related "Genre" object
				.ToList(); // .ToList() = immediately execute query
		}
	}
}