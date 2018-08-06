using CoOpHub.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CoOpHub.Tests.Domain.Models
{
	[TestClass]
	public class ApplicationUserTests
	{
		[TestMethod]
		public void Notify_WhenCalled_ShouldAddTheNotification()
		{
			// Arrange
			var user = new ApplicationUser();
			var notification = Notification.CoopCanceled(new Coop());

			// Act
			user.Notify(notification);

			// Assert
			// We have 3 assertions here, but this does not mean this test method
			// is violating the single responibility principle. These 3 assertions
			// are highly related and we're logically verifying one fact: that
			// the user object will have one UserNotification object in the right
			// state (meaning its User and Notification properties are set properly).
			user.UserNotifications.Count.Should().Be(1);

			var userNotification = user.UserNotifications.First();
			userNotification.Notification.Should().Be(notification);
			userNotification.User.Should().Be(user);
		}
	}
}
