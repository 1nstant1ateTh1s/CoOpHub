using CoOpHub.Repositories;

namespace CoOpHub.Persistence
{
	public interface IUnitOfWork
	{
		IAttendanceRepository Attendances { get; }
		ICoopRepository Coops { get; }
		IFollowingRepository Followings { get; }
		IGameRepository Games { get; }

		void Complete();
	}
}