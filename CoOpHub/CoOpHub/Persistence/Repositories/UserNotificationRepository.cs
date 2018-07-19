using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CoOpHub.Persistence.Repositories
{
	public class UserNotificationRepository : IUserNotificationRepository
	{
		private readonly ApplicationDbContext _context;

		public UserNotificationRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get a user's notifications.
		/// </summary>
		/// <param name="userId">The user to get associated notifications for.</param>
		/// <returns>IEnumerable of user notifications.</returns>
		public IEnumerable<UserNotification> GetUserNotificationsFor(string userId)
		{
			return _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead) // ... for currently logged in user that haven't been read yet
				.ToList();
		}
	}
}