using CoOpHub.Core.Models;
using System;

namespace CoOpHub.Core.Dtos
{
	public class NotificationDto
	{
		public DateTime DateTime { get; set; }
		public NotificationType Type { get; set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalVenue { get; set; }
		public GameDto OriginalGame { get; set; }
		public CoopDto Coop { get; set; }
	}
}