using CoOpHub.Controllers;
using CoOpHub.Core.Models;
using CoOpHub.Core.ViewModels;
using CoOpHub.IntegrationTests.Extensions;
using CoOpHub.Persistence;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoOpHub.IntegrationTests.Controllers
{
	[TestFixture]
	public class CoopsControllerTests
	{
		private CoopsController _controller;
		private ApplicationDbContext _context;

		[SetUp]
		public void SetUp()
		{
			// Instantiate controller w/ a real unit of work that talks to the database (which requires a real ApplicationDbContext)
			_context = new ApplicationDbContext();
			_controller = new CoopsController(new UnitOfWork(_context));
		}

		[TearDown]
		public void TearDown()
		{
			_context.Dispose();
		}

		[Test, Isolated] // use 'Isolated' attr. because this test changes the state of the db & we want to make sure the changes are rolled back.
		public void Mine_WhenCalled_ShouldReturnUpcomingCoops()
		{
			// Arrange
			// Mock the current user (w/ extension method)
			var user = _context.Users.First();
			_controller.MockCurrentUser(user.Id, user.UserName);

			var game = _context.Games.First();
			var coop = new Coop { Host = user, DateTime = DateTime.Now.AddDays(1), Game = game, Venue = "-" };

			// Add co-op session to context
			_context.Coops.Add(coop);
			_context.SaveChanges();

			// Act
			var result = _controller.Mine();

			// Assert
			(result.ViewData.Model as IEnumerable<Coop>).Should().HaveCount(1);
		}

		[Test, Isolated] // use 'Isolated' attr. because this test changes the state of the db & we want to make sure the changes are rolled back.
		public void Update_WhenCalled_ShouldUpdateTheGivenCoop()
		{
			// Arrange
			// Mock the current user (w/ extension method)
			var user = _context.Users.First();
			_controller.MockCurrentUser(user.Id, user.UserName);

			// Add co-op session to context
			var game = _context.Games.Single(g => g.Id == 1); // get a game that we know the Id for
			var coop = new Coop { Host = user, DateTime = DateTime.Now.AddDays(1), Game = game, Venue = "-" };
			_context.Coops.Add(coop);
			_context.SaveChanges();

			// Act
			var result = _controller.Update(new CoopFormViewModel
			{
				Id = coop.Id,
				Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
				Time = "20:00",
				Venue = "Venue",
				Game = 2 // Note: we will be updating the game id so we can check later and know the update was successful
			});

			// Assert
			_context.Entry(coop).Reload(); // refresh the updated co-op session from the database
			coop.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
			coop.Venue.Should().Be("Venue");
			coop.GameId.Should().Be(2);
		}
	}
}
