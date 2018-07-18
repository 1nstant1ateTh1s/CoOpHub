using CoOpHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoOpHub.Core.ViewModels
{
	public class CoopsViewModel
	{
		public IEnumerable<Coop> UpcomingCoops { get; set; }
		public bool ShowActions { get; set; }
		public string Heading { get; set; }
		public string SearchTerm { get; set; }
		public ILookup<int, Attendance> Attendances { get; set; }
	}
}