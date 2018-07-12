/* Immediately Invoked Function Expression (IIFE) */
/* Revealing Module pattern */
/// Responsible for "attendances" data access 
var AttendanceService = function () {
	// PRIVATE METHODS:
	var _createAttendance = function (coopId, done, fail) {
		// Make ajax call to api to attend this co-op session
		$.post("/api/attendances", { coopId: coopId })
			.done(done)
			.fail(fail);
	};

	var _deleteAttendance = function (coopId, done, fail) {
		// Make ajax call to api to no longer attend this co-op session
		$.ajax({
			url: "/api/attendances/" + coopId,
			method: "DELETE"
		})
			.done(done)
			.fail(fail);
	};

	// PUBLIC:
	return {
		createAttendance: _createAttendance,
		deleteAttendance: _deleteAttendance
	}
}();