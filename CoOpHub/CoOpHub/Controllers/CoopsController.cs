using CoOpHub.Models;
using CoOpHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
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
				Games = _context.Games.ToList()
			};

			return View(viewModel);
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

				return View("Create", viewModel);
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
			// Get list of co-op sessions I am hosting
			var userId = User.Identity.GetUserId();
			var coops = _context.Coops
				.Where(c => c.HostId == userId && c.DateTime > DateTime.Now)
				.Include(c => c.Game)   // include the related "Game" object
				.Include(c => c.Game.Genre)     // include the related "Genre" object
				.ToList();

			return View(coops);
		}
	}
}