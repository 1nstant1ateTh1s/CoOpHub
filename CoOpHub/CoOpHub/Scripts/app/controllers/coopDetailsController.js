/* Immediately Invoked Function Expression (IIFE) */
/* Revealing Module pattern */
/// Responsible for handling events raised from Coop Details view & updating the view
var CoopDetailsController = function (followingService) {

	// PRIVATE VARIABLES:
	var _followButton;

	// PRIVATE METHODS:
	var _init = function (container) {
		///<param name="container">A selector that represents the DOM container for our coop details view.</param>

		// "follow host" click event
		$(container).on("click", ".js-toggle-follow", _toggleFollowing); // *NOTE: using .on ensures only a single instance of _toggleFollowing will be loaded in memory
	};

	var _toggleFollowing = function (e) {
		_followButton = $(e.target);

		var followeeId = _followButton.attr("data-user-id");

		// Check current button state to determine which action to toggle
		if (_followButton.hasClass("btn-default"))
			followingService.createFollowing(followeeId, _done, _fail); // provide references to the _done & _fail callback functions to the followingService.createFollowing method
		else
			followingService.deleteFollowing(followeeId, _done, _fail); // *NOTE: an alt. approach here could be to pass an object instead of these parameters individually
	};

	var _done = function () {
		// Toggle button text & look
		var text = (_followButton.text() == "Follow") ? "Following" : "Follow";
		_followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
	};

	var _fail = function () {
		// Error scenario
		alert("Something failed!");
	};

	// PUBLIC:
	return {
		init: _init
	}

}(FollowingService); // immediately invoking w/ reference to FollowingService revealing module