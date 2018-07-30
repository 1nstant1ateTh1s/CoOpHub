using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly IApplicationDbContext _context;

		public NotificationRepository(IApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get new notifications.
		/// </summary>
		/// <param name="userId">The user to retrieve new notifications for.</param>
		/// <returns>IEnumerable of notifications.</returns>
		public IEnumerable<Notification> GetNewNotificationsFor(string userId)
		{
			return _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead) // ... for currently logged in user that haven't been read yet
				.Select(un => un.Notification) // ... select the actual notification that has the details 
				.Include(n => n.Coop.Host) // ... eager load the Coop & Host that goes with this notification
				.Include(n => n.Coop.Game) // ... eager load the Game that goes with this notification
				.Include(n => n.OriginalGame) // ... eager load the OriginalGame that goes with this notification
				.ToList();
		}
	}
}