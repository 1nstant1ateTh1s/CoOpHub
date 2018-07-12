/* Immediately Invoked Function Expression (IIFE) */
/* Revealing Module pattern */
/// Responsible for handling events raised from Coops view & updating the view
var CoopsController = function (attendanceService) {
	// PRIVATE VARIABLES:
	var _attendButton;

	// PRIVATE METHODS:
	var _init = function (container) {
		///<param name="container">A selector that represents the DOM container for our coops view.</param>

		// "attend coop" click event 
		// GOOD WAY
		$(container).on("click", ".js-toggle-attendance", _toggleAttendance); // *NOTE: using .on ensures only a single instance of _toggleAttendance will be loaded in memory
		// BAD WAY
		//$(".js-toggle-attendance").click(_toggleAttendance); // *NOTE: using .click would cause a new instance of _toggleAttendance event handler to be loaded in memory for every element that matches the selector
	};

	var _toggleAttendance = function (e) {
		_attendButton = $(e.target);

		var coopId = _attendButton.attr("data-coop-id");

		// Check current button state to determine which action to toggle
		if (_attendButton.hasClass("btn-default"))
			attendanceService.createAttendance(coopId, _done, _fail); // provide references to the _done & _fail callback functions to the attendanceService.createAttendance method
		else
			attendanceService.deleteAttendance(coopId, _done, _fail); // *NOTE: an alt. approach here could be to pass an object instead of these parameters individually
	};

	var _done = function () {
		// Toggle button text & look
		var text = (_attendButton.text() == "Going") ? "Going?" : "Going";
		_attendButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
	};

	var _fail = function () {
		// Error scenario
		alert("Something failed!");
	};

	// PUBLIC:
	return {
		init: _init
	}
}(AttendanceService); // immediately invoking w/ reference to AttendanceService revealing module