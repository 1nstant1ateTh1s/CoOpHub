using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
{
	public class CoopRepository : ICoopRepository
	{
		private readonly IApplicationDbContext _context;

		public CoopRepository(IApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get a co-op session with host, game, & genre eager loaded.
		/// </summary>
		/// <param name="coopId">The id of the co-op session to get.</param>
		/// <returns>A co-op session with host, game, & genre eager loaded.</returns>
		public Coop GetCoop(int coopId)
		{
			return _context.Coops
				.Include(c => c.Host) // use eager loading to include the host
				.Include(c => c.Game) // use eager loading to include the game
				.Include(c => c.Game.Genre) // use eager loading to include the genre
				.SingleOrDefault(c => c.Id == coopId); // .SingleOrDefault() = if we don't have a coop w/ this coopId, a null will be returned
		}

		/// <summary>
		/// Get a co-op session with attendees eager loaded.
		/// </summary>
		/// <param name="coopId">The id of the co-op session to get.</param>
		/// <returns>A co-op session with attendees eager loaded.</returns>
		public Coop GetCoopWithAttendees(int coopId)
		{
			return _context.Coops
				.Include(c => c.Attendances.Select(a => a.Attendee)) // include any users who were attending this gig
				.SingleOrDefault(c => c.Id == coopId); // .SingleOrDefault() = if we don't have a coop w/ this coopId, a null will be returned
		}

		/// <summary>
		/// Get all upcoming co-op sessions that have not been cancelled.
		/// </summary>
		/// <param name="searchTerm">Optional search term to filter query by.</param>
		/// <returns>IEnumerable of co-op sessions.</returns>
		public IEnumerable<Coop> GetUpcomingCoops(string searchTerm = null)
		{
			var upcomingCoops = _context.Coops
				.Include(c => c.Host)
				.Include(c => c.Game)
				.Include(c => c.Game.Genre)
				.Where(c => c.DateTime > DateTime.Now && !c.IsCanceled);

			// If there is a search query provided, we need to extend this query for upcoming co-op sessions
			if (!String.IsNullOrWhiteSpace(searchTerm))
			{
				// Search for query in host name, game name, genre name, or venue
				upcomingCoops = upcomingCoops
					.Where(c =>
						c.Host.Name.Contains(searchTerm) ||
						c.Game.Name.Contains(searchTerm) ||
						c.Game.Genre.Name.Contains(searchTerm) ||
						c.Venue.Contains(searchTerm));
			}

			return upcomingCoops.ToList();
		}

		/// <summary>
		/// Get upcoming co-op sessions that have not been cancelled.
		/// </summary>
		/// <param name="userId">The user to get upcoming co-op sessions for.</param>
		/// <returns>IEnumerable of co-op sessions.</returns>
		public IEnumerable<Coop> GetUpcomingCoopsByHost(string userId)
		{
			return _context.Coops
				.Where(
					c => c.HostId == userId &&
					c.DateTime > DateTime.Now &&
					!c.IsCanceled)
				.Include(c => c.Game)
				.Include(c => c.Game.Genre)
				.ToList();
		}

		/// <summary>
		/// Get co-op sessions a user is attending.
		/// </summary>
		/// <param name="userId">The user to get co-op sessions being attended.</param>
		/// <returns>IEnumerable of coops.</returns>
		public IEnumerable<Coop> GetCoopsUserAttending(string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Coop.DateTime > DateTime.Now)
				.Select(a => a.Coop)
				.Include(c => c.Host)   // include the related "Host" object
				.Include(c => c.Game)   // include the related "Game" object
				.Include(c => c.Game.Genre)     // include the related "Genre" object
				.ToList(); // .ToList() = immediately execute query
		}

		/// <summary>
		/// Add a co-op session.
		/// </summary>
		/// <param name="coop">The co-op session to add.</param>
		public void Add(Coop coop)
		{
			_context.Coops.Add(coop);
		}
	}
}