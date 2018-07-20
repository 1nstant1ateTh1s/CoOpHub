using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace CoOpHub.Tests.Extensions
{
	public static class ApiControllerExtensions
	{
		/// <summary>
		/// Extension method for ApiController to handle mocking up current user.
		/// </summary>
		/// <param name="controller">The class being extended.</param>
		/// <param name="userId">Useful for writing test cases that involve more than 1 user.</param>
		/// <param name="userName">Useful for writing test cases that involve more than 1 user.</param>
		public static void MockCurrentUser(this ApiController controller, string userId, string userName)
		{
			// Mock current user identity:
			var identity = new GenericIdentity(userName);
			identity.AddClaim(
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userName));
			identity.AddClaim(
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId)); // --> this claim is the user id we give to this user

			// Create generic principal object
			var principal = new GenericPrincipal(identity, null); // don't need any roles, so passing null

			controller.User = principal;
		}
	}
}
