using CoOpHub.Core.Models;
using CoOpHub.Persistence;
using CoOpHub.Persistence.Repositories;
using CoOpHub.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace CoOpHub.Tests.Persistence.Repositories
{
	[TestClass]
	public class NotificationRepositoryTests
	{
		private NotificationRepository _repository;
		private Mock<DbSet<UserNotification>> _mockNotifications;

		[TestInitialize]
		public void TestInitialize()
		{
			// Create mock db set
			_mockNotifications = new Mock<DbSet<UserNotification>>();

			// Create mock db context & pass to repository
			var mockContext = new Mock<IApplicationDbContext>();
			mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object); // setup mock context to return the mock db set

			_repository = new NotificationRepository(mockContext.Object);
		}

		[TestMethod]
		public void GetNewNotificationsFor_NotificationIsRead_ShouldNotBeReturned()
		{
			// Setup - create a notification
			var notification = Notification.CoopCanceled(new Coop()); // the notification obj
			var user = new ApplicationUser { Id = "1" }; // the user associated with the notification obj
			var userNotification = new UserNotification(user, notification); // the user's notification
			userNotification.Read(); // set the user's notification to read

			// Setup - use extension method to populate mock DbSet with this Notification object
			_mockNotifications.SetSource(new[] { userNotification });

			// Act - try to get notification
			var notifications = _repository.GetNewNotificationsFor(user.Id);

			// Assert - notifications should be empty
			notifications.Should().BeEmpty();
		}

		[TestMethod]
		public void GetNewNotificationsFor_NotificationIsForADifferentUser_ShouldNotBeReturned()
		{
			// Setup - create a notification
			var notification = Notification.CoopCanceled(new Coop()); // the notification obj
			var user = new ApplicationUser { Id = "1" }; // the user associated with the notification obj
			var userNotification = new UserNotification(user, notification); // the user's notification

			// Setup - use extension method to populate mock DbSet with this Notification object
			_mockNotifications.SetSource(new[] { userNotification });

			// Act - try to get notification that belongs to a different user
			var notifications = _repository.GetNewNotificationsFor(user.Id + "-");

			// Assert - notifications should be empty
			notifications.Should().BeEmpty();
		}

		[TestMethod]
		public void GetNewNotificationsFor_NewNotificationForTheGivenUser_ShouldBeReturned()
		{
			// Setup - create a notification
			var notification = Notification.CoopCanceled(new Coop()); // the notification obj
			var user = new ApplicationUser { Id = "1" }; // the user associated with the notification obj
			var userNotification = new UserNotification(user, notification); // the user's notification

			// Setup - use extension method to populate mock DbSet with this Notification object
			_mockNotifications.SetSource(new[] { userNotification });

			// Act - try to get notification
			var notifications = _repository.GetNewNotificationsFor(user.Id);

			// Assert - notifications should have 1 notification
			notifications.Should().HaveCount(1);

			// Assert - 1st notification should be the above "setup" notification
			notifications.First().Should().Be(notification);
		}
	}
}
