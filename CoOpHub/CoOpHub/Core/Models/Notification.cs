using System;
using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Core.Models
{
	public class Notification
	{
		public int Id { get; private set; }
		public DateTime DateTime { get; private set; }
		public NotificationType Type { get; private set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalVenue { get; set; }
		public Game OriginalGame { get; set; }
		public int? OriginalGameId { get; set; } /* "navigation" property */
		public Coop Coop { get; set; }

		[Required]
		public int CoopId { get; private set; } /* "navigation" property */

		/// <summary>
		/// Default constructor
		/// </summary>
		protected Notification()
		{
		}

		/// <summary>
		/// Custom constructor
		/// </summary>
		/// <param name="coop">The co-op session this notification is associated to.</param>
		/// <param name="type">The type of the notification.</param>
		private Notification(Coop coop, NotificationType type)
		{
			// Check if parameters are null
			if (coop == null)
			{
				throw new ArgumentNullException("coop");
			}

			Coop = coop;
			Type = type;
			DateTime = DateTime.Now;
		}

		/// <summary>
		/// Static Factory method responsible for creating a notification for a Co-op session that was just created.
		/// </summary>
		/// <returns></returns>
		public static Notification CoopCreated(Coop coop)
		{
			// Call the private constructor we already have
			return new Notification(coop, NotificationType.CoopCreated);
		}

		/// <summary>
		/// Static Factory method responsible for creating a notification for a Co-op session that was just updated.
		/// </summary>
		/// <returns></returns>
		public static Notification CoopUpdated(Coop newCoop, DateTime originalDateTime, string originalVenue, int? originalGame)
		{
			// Call the private constructor we already have
			var notification = new Notification(newCoop, NotificationType.CoopUpdated);
			notification.OriginalDateTime = originalDateTime;
			notification.OriginalVenue = originalVenue;
			notification.OriginalGameId = originalGame;

			return notification;
		}

		/// <summary>
		/// Static Factory method responsible for creating a notification for a Co-op session that was just canceled.
		/// </summary>
		/// <returns></returns>
		public static Notification CoopCanceled(Coop coop)
		{
			// Call the private constructor we already have
			return new Notification(coop, NotificationType.CoopCanceled);
		}
	}
}