using CoOpHub.Core.Repositories;

namespace CoOpHub.Core
{
	public interface IUnitOfWork
	{
		IAttendanceRepository Attendances { get; }
		ICoopRepository Coops { get; }
		IFollowingRepository Followings { get; }
		IGameRepository Games { get; }
		IApplicationUserRepository Users { get; }
		INotificationRepository Notifications { get; }
		IUserNotificationRepository UserNotifications { get; }

		void Complete();
	}
}