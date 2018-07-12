using CoOpHub.Models;
using CoOpHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext _context;

		public HomeController()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index(string query = null)
		{
			// Eagerly load upcoming co-op sessions that have not been cancelled
			var upcomingCoops = _context.Coops
				.Include(c => c.Host)	// include the related "Host" object
				.Include(c => c.Game)	// include the related "Game" object
				.Include(c => c.Game.Genre)		// include the related "Genre" object
				.Where(c => c.DateTime > DateTime.Now && !c.IsCanceled);

			// If there is a search query provided, we need to extend this query for upcoming co-op sessions
			if (!String.IsNullOrEmpty(query))
			{
				// Search for query in host name, game name, genre name, or venue
				upcomingCoops = upcomingCoops
					.Where(c => 
						c.Host.Name.Contains(query) ||
						c.Game.Name.Contains(query) ||
						c.Game.Genre.Name.Contains(query) ||
						c.Venue.Contains(query));
			}

			// Load attendances for future co-op sessions for the current user
			var userId = User.Identity.GetUserId();
			var attendances = _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Coop.DateTime > DateTime.Now) // get attendances for future co-op sessions only
				.ToList() // immediately execute query
				.ToLookup(a => a.CoopId); // convert the list to a data structure that allows us to quickly look up attendances by coop ID. *NOTE: A "LookUp" is like a dictionary - internally it uses a hash table to quickly look up objects

			// Create view model
			var viewModel = new CoopsViewModel
			{
				UpcomingCoops = upcomingCoops,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming Co-op sessions",
				SearchTerm = query, // autopopulate search box w/ the value of any query, if present
				Attendances = attendances
			};

			return View("Coops", viewModel);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}