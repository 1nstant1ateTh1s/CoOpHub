using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface IUserNotificationRepository
	{
		IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
	}
}
