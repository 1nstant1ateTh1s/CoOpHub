using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoOpHub.Core.Models
{
	public class Coop
	{
		public int Id { get; set; }
		public bool IsCanceled { get; private set; }
		public ApplicationUser Host { get; set; }
		public string HostId { get; set; }		/* "navigation" property */
		public DateTime DateTime { get; set; }
		public string Venue { get; set; }
		public Game Game { get; set; }
		public int GameId { get; set; }			/* "navigation" property */

		// Navigation properties:
		public ICollection<Attendance> Attendances { get; private set; }

		public Coop()
		{
			Attendances = new Collection<Attendance>();
		}

		/// <summary>
		/// Cancel the co-op session.
		/// </summary>
		public void Cancel()
		{
			// Update IsCanceled property to true
			IsCanceled = true;

			// Send out notification to all attendees when a co-op session is canceled:
			// Create the notification to send out
			var notification = Notification.CoopCanceled(this);

			// Notify ea. attendee of the notification
			foreach (var attendee in Attendances.Select(a => a.Attendee))
			{
				attendee.Notify(notification);
			}
		}

		public void Modify(DateTime dateTime, string venue, int game)
		{
			// Send out notification to all attendees when a co-op session is updated:
			// Create the notification to send out
			var notification = Notification.CoopUpdated(this, DateTime, Venue, GameId);

			// Update properties to new values:
			Venue = venue;
			DateTime = dateTime;
			GameId = game;

			// Notify ea. attendee of the notification
			foreach (var attendee in Attendances.Select(a => a.Attendee))
			{
				attendee.Notify(notification);
			}
		}
	}
}