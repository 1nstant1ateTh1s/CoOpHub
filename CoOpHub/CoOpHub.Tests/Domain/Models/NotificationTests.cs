using CoOpHub.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoOpHub.Tests.Domain.Models
{
	[TestClass]
	public class NotificationTests
	{
		[TestMethod]
		public void CoopCanceled_WhenCalled_ShouldReturnANotificationForACanceledCoop()
		{
			// Arrange
			var coop = new Coop();

			// Act
			var notification = Notification.CoopCanceled(coop);

			// Assert
			// Again, here, we have two assertions, but that doesn't mean we're
			// violating the single responsibility principle. We're verifying 
			// one logical fact: that upon calling Notification.CoopCanceled()
			// we'll get a notification object for the canceled co-op session. This notification
			// object should be in the right state, meaning its type should be
			// CoopCanceled and its coop should be the coop for each we created 
			// the notification. 
			notification.Type.Should().Be(NotificationType.CoopCanceled);
			notification.Coop.Should().Be(coop);
		}
	}
}
