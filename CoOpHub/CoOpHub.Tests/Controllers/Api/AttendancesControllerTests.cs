using CoOpHub.Controllers.Api;
using CoOpHub.Core;
using CoOpHub.Core.Dtos;
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
	public class AttendancesControllerTests
	{
		private string _userId;
		private Mock<IAttendanceRepository> _mockRepository;
		private AttendancesController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			// Create mock repository - will be empty unless setup in test methods below
			_mockRepository = new Mock<IAttendanceRepository>();

			// Create a mock unit of work
			var mockUoW = new Mock<IUnitOfWork>(); // because we are unit testing the controller, we don't want to couple to the real UnitOfWork which uses EntityFramework to talk to the db
			mockUoW.SetupGet(a => a.Attendances).Returns(_mockRepository.Object); // * --> when we access the Attendances property of UoW, we get the mock repository *

			// Create the controller
			_controller = new AttendancesController(mockUoW.Object); // the Object property is the actual mock implementation of IUnitOfWork

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
		public void Attend_AttendanceWithGivenIdExists_ShouldReturnBadRequest()
		{
			// Setup - add an attendance to repository
			var attendance = new Attendance();

			_mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

			// Setup - create attendance dto
			var attendanceDto = new AttendanceDto()
			{
				CoopId = 1
			};

			// Action - attempt to add an attendance with same id
			var result = _controller.Attend(attendanceDto);

			// Assert - attendance already exists
			result.Should().BeOfType<BadRequestErrorMessageResult>();
		}

		[TestMethod]
		public void Attend_ValidRequest_ShouldReturnOk()
		{
			// Setup - create attendance dto
			var attendanceDto = new AttendanceDto()
			{
				CoopId = 1
			};

			// Action - make a valid request to add attendance
			var result = _controller.Attend(attendanceDto);

			// Assert - attendance added successfully
			result.Should().BeOfType<OkResult>();
		}

		[TestMethod]
		public void DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
		{
			// Action - attempt to delete an attendance that does not yet exist
			var result = _controller.DeleteAttendance(1);

			// Assert - attendance not found
			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void DeleteAttendance_ValidRequest_ShouldReturnOk()
		{
			// Setup - add an attendance to repository
			var attendance = new Attendance();

			_mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

			// Action - make a valid request to delete attendance
			var result = _controller.DeleteAttendance(1);

			// Assert - attendance deleted successfully
			result.Should().BeOfType<OkNegotiatedContentResult<int>>();
		}

		[TestMethod]
		public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
		{
			// Setup - add an attendance to repository
			var attendance = new Attendance();

			_mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

			// Action - make a valid request to delete attendance
			var result = (OkNegotiatedContentResult<int>)_controller.DeleteAttendance(1);

			// Assert - returns id of deleted attendance
			result.Content.Should().Be(1);
		}
	}
}
