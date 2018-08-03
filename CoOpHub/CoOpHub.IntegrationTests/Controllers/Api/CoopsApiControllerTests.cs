using CoOpHub.Controllers.Api;
using CoOpHub.Core.Models;
using CoOpHub.Persistence;
using CoOpHub.Tests.Extensions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace CoOpHub.IntegrationTests.Controllers.Api
{
	[TestFixture]
	public class CoopsApiControllerTests
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
		public void Cancel_WhenCalled_ShouldCancelTheGivenCoop()
		{
			// Arrange
			// Mock the current user (w/ extension method) **NOTE - TODO: move the controller extension methods to a separate, shared library so all test projects can reference a single .dll
			var user = _context.Users.First();
			_controller.MockCurrentUser(user.Id, user.UserName);

			// Add co-op session to context
			var game = _context.Games.Single(g => g.Id == 1); // get a game that we know the Id for
			var coop = new Coop { Host = user, DateTime = DateTime.Now.AddDays(1), Game = game, Venue = "-" };
			_context.Coops.Add(coop);
			_context.SaveChanges();

			// Act
			var result = _controller.Cancel(coop.Id);

			// Assert
			_context.Entry(coop).Reload(); // refresh the canceled co-op session from the database
			coop.IsCanceled.Should().BeTrue();
		}
	}
}
