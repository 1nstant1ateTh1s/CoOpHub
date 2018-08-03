using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace CoOpHub.IntegrationTests.Extensions
{
	public static class ControllerExtensions
	{
		/// <summary>
		/// Extension method for normal Controller to handle mocking up current user.
		/// </summary>
		/// <param name="controller">The class being extended.</param>
		/// <param name="userId">Useful for writing test cases that involve more than 1 user.</param>
		/// <param name="userName">Useful for writing test cases that involve more than 1 user.</param>
		public static void MockCurrentUser(this Controller controller, string userId, string userName)
		{
			// Mock current user identity:
			var identity = new GenericIdentity(userName);
			identity.AddClaim(
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userName));
			identity.AddClaim(
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId)); // --> this claim is the user id we give to this user

			// Create generic principal object
			var principal = new GenericPrincipal(identity, null); // don't need any roles, so passing null

			// Mock "Current User" in normal MVC controllers:

			// Initialize controller context to the mock controller context
			controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
				// Initialize "HttpContext" property of controller context
				ctx.HttpContext == Mock.Of<HttpContextBase>(http =>
					// Initialize "User" property of http context to the prev. created principal obj.
					http.User == principal));
		}
	}
}
