using CoOpHub.Core;
using CoOpHub.Core.Repositories;
using CoOpHub.Persistence.Repositories;

namespace CoOpHub.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public ICoopRepository Coops { get; private set; }
		public IAttendanceRepository Attendances { get; private set; }
		public IGameRepository Games { get; private set; }
		public IFollowingRepository Followings { get; private set; }
		public IApplicationUserRepository Users { get; private set; }
		public INotificationRepository Notifications { get; private set; }
		public IUserNotificationRepository UserNotifications { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;

			Coops = new CoopRepository(_context);
			Attendances = new AttendanceRepository(_context);
			Games = new GameRepository(_context);
			Followings = new FollowingRepository(_context);
			Users = new ApplicationUserRepository(_context);
			Notifications = new NotificationRepository(_context);
			UserNotifications = new UserNotificationRepository(_context);
		}

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}