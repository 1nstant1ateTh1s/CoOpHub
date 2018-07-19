using AutoMapper;
using CoOpHub.Core;
using CoOpHub.Core.Dtos;
using CoOpHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace CoOpHub.Controllers.Api
{
	[Authorize]
	public class NotificationsController : ApiController
	{
		private readonly IUnitOfWork _unitOfWork;

		public NotificationsController(IUnitOfWork unitOfWork)
		{
			// Dependency Inversion - no reliance on entity framework !!
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<NotificationDto> GetNewNotifications()
		{
			// Get list of user notifications for the current user ...
			var userId = User.Identity.GetUserId();
			var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);

			// Using AutoMapper to map Notification to NotificationDto 
			return notifications.Select(Mapper.Map<Notification, NotificationDto>);

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
			var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);

			// Mark user notifications as having been read
			notifications.ForEach(n => n.Read());

			_unitOfWork.Complete();

			return Ok();
		}
	}
}
