using CoOpHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	public class FolloweesController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public FolloweesController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			// Get hosts the current user is following
			var userId = User.Identity.GetUserId();
			var hosts = _unitOfWork.Users.GetArtistsFollowedBy(userId);

			return View(hosts);
		}
	}
}