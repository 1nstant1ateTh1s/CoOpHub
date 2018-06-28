﻿using CoOpHub.Dtos;
using CoOpHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace CoOpHub.Controllers
{
	[Authorize]
	public class FollowingsController : ApiController
	{
		private ApplicationDbContext _context;

		public FollowingsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto dto)
		{
			var userId = User.Identity.GetUserId();

			// Check if following already exists in db
			if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
			{
				return BadRequest("Following already exists.");
			}

			var following = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_context.Followings.Add(following);
			_context.SaveChanges();

			return Ok();
		}
	}
}
