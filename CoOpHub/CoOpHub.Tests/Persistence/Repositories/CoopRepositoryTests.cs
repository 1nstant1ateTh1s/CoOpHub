using CoOpHub.Core.Models;
using CoOpHub.Persistence;
using CoOpHub.Persistence.Repositories;
using CoOpHub.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace CoOpHub.Tests.Persistence.Repositories
{
	/// <summary>
	/// Summary description for CoopRepositoryTests
	/// </summary>
	[TestClass]
	public class CoopRepositoryTests
	{

		private CoopRepository _repository;
		private Mock<DbSet<Coop>> _mockCoops;
		private Mock<DbSet<Attendance>> _mockAttendances;

		// Use TestInitialize to run code before running each test 
		[TestInitialize()]
		public void MyTestInitialize()
		{
			// Create mock db sets
			_mockCoops = new Mock<DbSet<Coop>>();
			_mockAttendances = new Mock<DbSet<Attendance>>();

			// Create mock db context & pass to repository
			var mockContext = new Mock<IApplicationDbContext>();
			mockContext.SetupGet(c => c.Coops).Returns(_mockCoops.Object); // setup mock context to return the mock db set
			mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);  // setup mock context to return the mockAttendances db set

			_repository = new CoopRepository(mockContext.Object);
		}

		[TestMethod]
		public void GetUpcomingCoopsByHost_CoopIsInThePast_ShouldNotBeReturned()
		{
			// Setup - create a co-op session in the past
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(-1), HostId = "1" };

			// Setup - use extension method to populate mock DbSet with this coop object
			_mockCoops.SetSource(new[] { coop });

			// Action - try to get co-op session that is in the past
			var coops = _repository.GetUpcomingCoopsByHost("1");

			// Assert - coops should be empty
			coops.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingCoopsByHost_CoopIsCanceled_ShouldNotBeReturned()
		{
			// Setup - create a co-op session in the future that has been canceled
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(1), HostId = "1" };
			coop.Cancel();

			// Setup - use extension method to populate mock DbSet with this coop object
			_mockCoops.SetSource(new[] { coop });

			// Action - try to get co-op session that has been canceled
			var coops = _repository.GetUpcomingCoopsByHost("1");

			// Assert - coops should be empty
			coops.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingCoopsByHost_CoopIsForADifferentHost_ShouldNotBeReturned()
		{
			// Setup - create a co-op session in the future that is for a different host
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(1), HostId = "1" };

			// Setup - use extension method to populate mock DbSet with this coop object
			_mockCoops.SetSource(new[] { coop });

			// Action - try to get co-op session that belongs to a different host
			var coops = _repository.GetUpcomingCoopsByHost(coop.HostId + "-");

			// Assert - coops should be empty
			coops.Should().BeEmpty();
		}

		[TestMethod]
		public void GetUpcomingCoopsByHost_CoopIsForTheGivenHostAndIsInTheFuture_ShouldBeReturned()
		{
			// Setup - create a co-op session in the future that is for the current user/host
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(1), HostId = "1" };

			// Setup - use extension method to populate mock DbSet with this coop object
			_mockCoops.SetSource(new[] { coop });

			// Action - try to get co-op session
			var coops = _repository.GetUpcomingCoopsByHost(coop.HostId);

			// Assert - coops should contain the above "setup" co-op session
			coops.Should().Contain(coop);
		}

		[TestMethod]
		public void GetCoopsUserAttending_CoopIsInThePast_ShouldNotBeReturned()
		{
			// Setup - create a co-op session in the past
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(-1) };

			// Setup - create attendance
			var attendance = new Attendance() { Coop = coop, AttendeeId = "1" };

			// Setup - use extension method to populate mock DbSet with this attendance object
			_mockAttendances.SetSource(new[] { attendance });

			// Action - try to get co-op session attending
			var coops = _repository.GetCoopsUserAttending(attendance.AttendeeId);

			// Assert - coops should be empty
			coops.Should().BeEmpty();
		}

		[TestMethod]
		public void GetCoopsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
		{
			// Setup - create a co-op session
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(1) };

			// Setup - create attendance
			var attendance = new Attendance() { Coop = coop, AttendeeId = "1" };

			// Setup - use extension method to populate mock DbSet with this attendance object
			_mockAttendances.SetSource(new[] { attendance });

			// Action - try to get co-op session attending for a different user
			var coops = _repository.GetCoopsUserAttending(attendance.AttendeeId + "-");

			// Assert - coops should be empty
			coops.Should().BeEmpty();
		}

		[TestMethod]
		public void GetCoopsUserAttending_UpcomingCoopUserAttending_ShouldBeReturned()
		{
			// Setup - create a co-op session
			var coop = new Coop() { DateTime = DateTime.Now.AddDays(1) };

			// Setup - create attendance
			var attendance = new Attendance() { Coop = coop, AttendeeId = "1" };

			// Setup - use extension method to populate mock DbSet with this attendance object
			_mockAttendances.SetSource(new[] { attendance });

			// Action - try to get gig attending
			var coops = _repository.GetCoopsUserAttending(attendance.AttendeeId);

			// Assert - coops should contain the above "setup" co-op session
			coops.Should().Contain(coop);
		}
	}
}
