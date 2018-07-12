/* Immediately Invoked Function Expression (IIFE) */
/* Revealing Module pattern */
/// Responsible for "following" data access 
var FollowingService = function () {
	// PRIVATE METHODS:
	var _createFollowing = function (followeeId, done, fail) {
		// Make ajax call to api to follow this host
		$.post("/api/followings", { followeeId: followeeId })
			.done(done)
			.fail(fail);
	};

	var _deleteFollowing = function (followeeId, done, fail) {
		// Make ajax call to api to unfollow this host
		$.ajax({
			url: "/api/followings/",
			method: "DELETE",
			data: {
				followeeId: followeeId
			}
		})
			.done(done)
			.fail(fail);
	};

	// PUBLIC:
	return {
		createFollowing: _createFollowing,
		deleteFollowing: _deleteFollowing
	}
}();