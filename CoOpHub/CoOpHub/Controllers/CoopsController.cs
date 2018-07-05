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

			// Get existing Coop entity from DB
			var userId = User.Identity.GetUserId();
			var coop = _context.Coops.Single(c => c.Id == viewModel.Id && c.HostId == userId);

			// Update Coop properties
			coop.Venue = viewModel.Venue;
			coop.DateTime = viewModel.GetDateTime();
			coop.GameId = viewModel.Game;

			// Save changes
			_context.SaveChanges();

			return RedirectToAction("Mine", "Coops");
		}

		[Authorize]
		public ActionResult Attending()
		{
			// Get list of co-op sessions user is attending
			var userId = User.Identity.GetUserId();
			var coops = _context.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Coop)
				.Include(g => g.Host)   // include the related "Host" object
				.Include(g => g.Game)	// include the related "Game" object
				.Include(g => g.Game.Genre)     // include the related "Genre" object
				.ToList();

			// Build view model
			var viewModel = new CoopsViewModel()
			{
				UpcomingCoops = coops,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Co-op sessions I'm Attending"
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