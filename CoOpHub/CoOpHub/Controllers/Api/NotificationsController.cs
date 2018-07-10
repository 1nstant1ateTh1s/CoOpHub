using AutoMapper;
using CoOpHub.Dtos;
using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private ApplicationDbContext _context;

		public NotificationsController()
		{
			_context = new ApplicationDbContext();
		}

		public IEnumerable<NotificationDto> GetNewNotifications()
		{
			// Get list of user notifications ...
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead) // ... for currently logged in user that haven't been read yet
				.Select(un => un.Notification) // ... select the actual notification that has the details 
				.Include(n => n.Coop.Host) // ... eager load the Host that goes with this notification
				.Include(n => n.Coop.Game) // ... eager load the Game that goes with this notification
				.Include(n => n.OriginalGame) // ... eager load the OriginalGame that goes with this notification
				.ToList();

			// Using AutoMapper to map Notification to NotificationDto 
			return notifications.Select(Mapper.Map<Notification, NotificationDto>);

			//return notifications;

			// Manual mapping:
			//var notifsDto = notifications.Select(n => new NotificationDto()
			//{
			//	DateTime = n.DateTime,
			//	Coop = new CoopDto
			//	{
			//		Host = new UserDto
			//		{
			//			Id = n.Coop.Host.Id,
			//			Name = n.Coop.Host.Name
			//		},
			//		Game = new GameDto
			//		{
			//			//Genre = new GenreDto
			//			//{
			//			//	Id = n.Coop.Game.Genre.Id,
			//			//	Name = n.Coop.Game.Genre.Name
			//			//},
			//			Id = n.Coop.Game.Id,
			//			Name = n.Coop.Game.Name
			//		},
			//		Id = n.Coop.Id,
			//		DateTime = n.Coop.DateTime,
			//		IsCanceled = n.Coop.IsCanceled,
			//		Venue = n.Coop.Venue
			//	},
			//	//OriginalGame = new GameDto
			//	//{
			//	//	//Genre = new GenreDto
			//	//	//{
			//	//	//	Id = n.OriginalGame.Genre.Id,
			//	//	//	Name = n.OriginalGame.Genre.Name
			//	//	//},
			//	//	Id = n.OriginalGame.Id,
			//	//	Name = n.OriginalGame.Name
			//	//},
			//	OriginalDateTime = n.OriginalDateTime,
			//	OriginalVenue = n.OriginalVenue,
			//	Type = n.Type
			//});

			//return notifsDto;
		}

		[HttpPost]
		public IHttpActionResult MarkAsRead()
		{
			// Get list of user notifications ...
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead) // ... for currently logged in user that haven't been read yet
				.ToList();

			// Mark user notifications as having been read
			notifications.ForEach(n => n.Read());

			// Save changes
			_context.SaveChanges();

			return Ok();
		}
	}
}
