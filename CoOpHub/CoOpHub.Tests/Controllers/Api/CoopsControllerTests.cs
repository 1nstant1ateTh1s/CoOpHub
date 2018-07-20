﻿using CoOpHub.Controllers.Api;
using CoOpHub.Core;
using CoOpHub.Core.Models;
using CoOpHub.Core.Repositories;
using CoOpHub.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace CoOpHub.Tests.Controllers.Api
{
	[TestClass]
	public class CoopsControllerTests
	{
		private string _userId;
		private Mock<ICoopRepository> _mockRepository;
		private CoopsController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			// Create mock repository
			_mockRepository = new Mock<ICoopRepository>();

			// Create a moq unit of work
			var mockUoW = new Mock<IUnitOfWork>(); // because we are unit testing the controller, we don't want to couple to the real UnitOfWork which uses EntityFramework to talk to the db
			mockUoW.SetupGet(u => u.Coops).Returns(_mockRepository.Object); // * --> when we access the Coops property of UoW, we get the mock repository *

			// Create the controller (*references the Api controller)
			_controller = new CoopsController(mockUoW.Object); // the Object property is the actual mock implementation of IUnitOfWork

			// Set controller user w/ the generic principal object by calling extension method, passing in the userid/username we want to use
			_userId = "1";
			_controller.MockCurrentUser(_userId, "user1@domain.com");
		}

		/* NOTE: Tests are named according to Roy Osherove's naming convention as outlined in the book: The Art of Unit Testing. 

			Part 1: The name of the method under test
			Part 2: The condition being tested
			Part 3: The expected result

			Ex --> public void Method_Condition_Result() { }

		*/

		[TestMethod]
		public void Cancel_NoCoopWithGivenIdExists_ShouldReturnNotFound()
		{
			// Action
			var result = _controller.Cancel(1);

			// Assertion
			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void Cancel_CoopIsCanceled_ShouldReturnNotFound()
		{
			// Setup
			var coop = new Coop();
			coop.Cancel();

			_mockRepository.Setup(r => r.GetCoopWithAttendees(1)).Returns(coop);

			// Action
			var result = _controller.Cancel(1);

			// Assertion
			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void Cancel_UserCancelingAnotherUsersCoop_ShouldReturnUnauthorized()
		{
			// Setup
			var coop = new Coop { HostId = _userId + "-" };

			_mockRepository.Setup(r => r.GetCoopWithAttendees(1)).Returns(coop);

			// Action
			var result = _controller.Cancel(1);

			// Assertion
			result.Should().BeOfType<UnauthorizedResult>();
		}

		[TestMethod]
		public void Cancel_ValidRequest_ShouldReturnOk()
		{
			// Setup
			var coop = new Coop { HostId = _userId };

			_mockRepository.Setup(r => r.GetCoopWithAttendees(1)).Returns(coop);

			// Action
			var result = _controller.Cancel(1);

			// Assertion
			result.Should().BeOfType<OkResult>();

		}
	}
}
