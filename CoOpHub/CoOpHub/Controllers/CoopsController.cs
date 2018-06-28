using CoOpHub.Models;
using CoOpHub.ViewModels;
using Microsoft.AspNet.Identity;
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

			return RedirectToAction("Index", "Home");
		}
	}
}