using CoOpHub.Core;
using CoOpHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index(string query = null)
		{
			// Get list of upcoming co-op sessions, providing an optional search query
			var upcomingCoops = _unitOfWork.Coops.GetUpcomingCoops(query);

			// Get attendances to future co-op sessions for the current user
			var userId = User.Identity.GetUserId();
			var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
				.ToLookup(a => a.CoopId); // .ToLookup() = convert the list to a data structure that allows us to quickly look up attendances by coop ID. *NOTE: A "LookUp" is like a dictionary - internally it uses a hash table to quickly look up objects

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