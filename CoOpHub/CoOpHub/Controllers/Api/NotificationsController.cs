using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private ApplicationDbContext _context;

		public NotificationsController()
		{
			_context = new ApplicationDbContext();
		}

		public IEnumerable<Notification> GetNewNotifications()
		{
			// Get list of user notifications ...
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.UserId == userId) // ... for currently logged in user
				.Select(un => un.Notification) // ... select the actual notification that has the details 
				.Include(n => n.Coop.Host) // ... eager load the Coop & Host that goes with this notification
				.ToList();

			return notifications;
		}
	}
}
