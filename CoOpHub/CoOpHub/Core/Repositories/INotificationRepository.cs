using CoOpHub.Core.Models;
using System.Collections.Generic;

namespace CoOpHub.Core.Repositories
{
	public interface INotificationRepository
	{
		IEnumerable<Notification> GetNewNotificationsFor(string userId);
	}
}
