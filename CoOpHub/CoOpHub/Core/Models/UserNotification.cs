using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoOpHub.Core.Models
{
	/// <summary>
	/// Association class between User & Notification classes
	/// </summary>
	public class UserNotification
	{
		// Composite primary key definition:
		[Key]
		[Column(Order = 1)]
		public string UserId { get; private set; }

		[Key]
		[Column(Order = 2)]
		public int NotificationId { get; private set; }

		// Navigation properties to navigate to related objects:
		public ApplicationUser User { get; private set; }
		public Notification Notification { get; private set; }

		// Other properties:
		public bool IsRead { get; private set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		protected UserNotification()
		{
		}

		/// <summary>
		/// Custom constructor
		/// </summary>
		/// <param name="user"></param>
		/// <param name="notification"></param>
		public UserNotification(ApplicationUser user, Notification notification)
		{
			// Check if parameters are null
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}

			User = user;
			Notification = notification;
		}

		/// <summary>
		/// Mark user notification as read.
		/// </summary>
		public void Read()
		{
			// Set IsRead property to true
			IsRead = true;
		}
	}
}