using CoOpHub.Models;
using CoOpHub.ViewModels;
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

		public ActionResult Index()
		{
			// Eagerly load upcoming co-op sessions
			var upcomingCoops = _context.Coops
				.Include(g => g.Host)	// include the related "Host" object
				.Include(g => g.Game)	// include the related "Game" object
				.Include(g => g.Game.Genre)		// include the related "Genre" object
				.Where(g => g.DateTime > DateTime.Now);

			// Create view model
			var viewModel = new CoopsViewModel
			{
				UpcomingCoops = upcomingCoops,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming Co-op sessions"
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