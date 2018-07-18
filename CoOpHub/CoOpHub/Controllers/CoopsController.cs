using CoOpHub.Core;
using CoOpHub.Core.Models;
using CoOpHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	// Coops MVC controller
	public class CoopsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CoopsController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
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
				Games = _unitOfWork.Games.GetGames()
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
				viewModel.Games = _unitOfWork.Games.GetGames();

				return View("CoopForm", viewModel);
			}

			var coop = new Coop
			{
				HostId = User.Identity.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GameId = viewModel.Game,
				Venue = viewModel.Venue
			};

			// Add the new co-op session
			_unitOfWork.Coops.Add(coop);

			// Complete the transaction
			_unitOfWork.Complete();

			return RedirectToAction("Mine", "Coops");
		}

		[Authorize]
		public ActionResult Edit(int id)
		{
			// Get existing co-op session entity from repository
			var coop = _unitOfWork.Coops.GetCoop(id);

			// Security checks on returned coop
			if (coop == null)
			{
				// Co-op session not found
				return HttpNotFound();
			}

			if (coop.HostId != User.Identity.GetUserId())
			{
				// Co-op session does not belong to the current user
				return new HttpUnauthorizedResult();
			}

			var viewModel = new CoopFormViewModel
			{
				Heading = "Edit a Co-op Session",
				Id = coop.Id,
				Games = _unitOfWork.Games.GetGames(),
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
				viewModel.Games = _unitOfWork.Games.GetGames();

				return View("CoopForm", viewModel);
			}

			// Get existing Co-op session entity from DB (use eager loading to get coop + all of it's attendees)
			var coop = _unitOfWork.Coops.GetCoopWithAttendees(viewModel.Id);

			// Security checks on return coop
			if (coop == null)
			{
				// Coop not found
				return HttpNotFound();
			}

			if (coop.HostId != User.Identity.GetUserId())
			{
				// Co-op session does not belong to the current user
				return new HttpUnauthorizedResult();
			}

			// Update Coop
			coop.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Game);

			// Complete the transaction
			_unitOfWork.Complete();

			return RedirectToAction("Mine", "Coops");
		}

		public ActionResult Details(int id)
		{
			// Get details for the specified co-op session
			var coop = _unitOfWork.Coops.GetCoop(id);

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
				viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(coop.Id, userId) != null; // NOTE: Using "GetAttendance(...)" method and checking for null is more reusable than a method like "IsAttending(..)"

				// Check if user is already following this host or not
				viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, coop.HostId) != null; // NOTE: Using "GetFollowing(...)" method and checking for null is more reusable than a method like "IsFollowing(..)"
			}

			return View("Details", viewModel);
		}

		[Authorize]
		public ActionResult Attending()
		{
			var userId = User.Identity.GetUserId();

			// Build view model
			var viewModel = new CoopsViewModel()
			{
				UpcomingCoops = _unitOfWork.Coops.GetCoopsUserAttending(userId),
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Co-op sessions I'm Attending",
				Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.CoopId) // .ToLookup() = convert the list to a data structure that allows us to quickly look up attendances by coop ID. *NOTE: A "LookUp" is like a dictionary - internally it uses a hash table to quickly look up objects
			};

			return View("Coops", viewModel);
		}

		[Authorize]
		public ActionResult Mine()
		{
			// Get list of upcoming co-op sessions the currently logged in user is hosting, that have not been cancelled
			var userId = User.Identity.GetUserId();
			var coops = _unitOfWork.Coops.GetUpcomingCoopsByHost(userId);

			return View(coops);
		}
	}
}