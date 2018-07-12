using CoOpHub.Models;
using CoOpHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	// Coops MVC controller
	public class CoopsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CoopsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public ActionResult Search(CoopsViewModel viewModel)
		{
			return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
		}

		[Authorize]
		public ActionResult Create()
		{
			var viewModel = new CoopFormViewModel
			{
				Heading = "Add a Co-op Session",
				Games = _context.Games.ToList()
			};

			return View("CoopForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CoopFormViewModel viewModel)
		{
			// Check that model state is valid
			if (!ModelState.IsValid)
			{
				// Set the Games property of view model
				viewModel.Games = _context.Games.ToList();

				return View("CoopForm", viewModel);
			}

			var coop = new Coop
			{
				HostId = User.Identity.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GameId = viewModel.Game,
				Venue = viewModel.Venue
			};

			_context.Coops.Add(coop);
			_context.SaveChanges();

			return RedirectToAction("Mine", "Coops");
		}

		[Authorize]
		public ActionResult Edit(int id)
		{
			var userId = User.Identity.GetUserId();
			var coop = _context.Coops.Single(c => c.Id == id && c.HostId == userId);

			var viewModel = new CoopFormViewModel
			{
				Heading = "Edit a Co-op Session",
				Id = coop.Id,
				Games = _context.Games.ToList(),
				Date = coop.DateTime.ToString("d MMM yyyy"),
				Time = coop.DateTime.ToString("HH:mm"),
				Game = coop.GameId,
				Venue = coop.Venue
			};

			return View("CoopForm", viewModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(CoopFormViewModel viewModel)
		{
			// Check that model state is valid
			if (!ModelState.IsValid)
			{
				// Set the Games property of view model
				viewModel.Games = _context.Games.ToList();

				return View("CoopForm", viewModel);
			}

			// Get existing Coop entity from DB (use eager loading to get coop + all of it's attendees)
			var userId = User.Identity.GetUserId();
			var coop = _context.Coops
				.Include(c => c.Attendances.Select(a => a.Attendee)) // include any users who were attending this gig
				.Single(c => c.Id == viewModel.Id && c.HostId == userId);

			// Update Coop
			coop.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Game);

			// Save changes
			_context.SaveChanges();

			return RedirectToAction("Mine", "Coops");
		}

		public ActionResult Details(int id)
		{
			// Get details for the specified co-op session
			var coop = _context.Coops
				.Include(c => c.Host) // use eager loading to include the related "Host" object
				.Include(c => c.Game) // use eager loading to include the related "Game" object
				.Include(c => c.Game.Genre) // use eager loading to include the related "Genre" object
				.Single(c => c.Id == id);

			// Check if co-op session was not found
			if (coop == null)
			{
				return HttpNotFound();
			}

			// Initialize view model
			var viewModel = new CoopDetailsViewModel { Coop = coop };

			// If the user is authenticated
			if (User.Identity.IsAuthenticated)
			{
				var userId = User.Identity.GetUserId();

				// Check if user is already attending this co-op session or not
				viewModel.IsAttending = _context.Attendances
					.Any(a => a.CoopId == coop.Id && a.AttendeeId == userId);

				// Check if user is already following this host or not
				viewModel.IsFollowing = _context.Followings
					.Any(f => f.FolloweeId == coop.HostId && f.FollowerId == userId);
			}

			return View("Details", viewModel);
		}

		[Authorize]
		public ActionResult Attending()
		{
			// Get list of co-op sessions user is attending
			var userId = User.Identity.GetUserId();
			var coops = _context.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Coop)
				.Include(c => c.Host)   // include the related "Host" object
				.Include(c => c.Game)	// include the related "Game" object
				.Include(c => c.Game.Genre)     // include the related "Genre" object
				.ToList();

			// Load attendances for future co-op sessions for the current user
			var attendances = _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Coop.DateTime > DateTime.Now) // get attendances for future co-op sessions only
				.ToList() // immediately execute query
				.ToLookup(a => a.CoopId); // convert the list to a data structure that allows us to quickly look up attendances by coop ID. *NOTE: A "LookUp" is like a dictionary - internally it uses a hash table to quickly look up objects

			// Build view model
			var viewModel = new CoopsViewModel()
			{
				UpcomingCoops = coops,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Co-op sessions I'm Attending",
				Attendances = attendances
			};

			return View("Coops", viewModel);
		}

		[Authorize]
		public ActionResult Mine()
		{
			// Get list of upcoming co-op sessions the currently logged in user is hosting, that have not been cancelled
			var userId = User.Identity.GetUserId();
			var coops = _context.Coops
				.Where(
					c => c.HostId == userId && 
					c.DateTime > DateTime.Now && 
					!c.IsCanceled)
				.Include(c => c.Game)   // include the related "Game" object
				.Include(c => c.Game.Genre)     // include the related "Genre" object
				.ToList();

			return View(coops);
		}
	}
}