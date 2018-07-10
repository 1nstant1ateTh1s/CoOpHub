using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace CoOpHub.Controllers
{
	public class FolloweesController : Controller
	{
		private ApplicationDbContext _context;

		public FolloweesController()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index()
		{
			// Get hosts the current user is following
			var userId = User.Identity.GetUserId();
			var hosts = _context.Followings
				.Where(f => f.FollowerId == userId)
				.Select(f => f.Followee)
				.ToList();

			return View(hosts);
		}
	}
}