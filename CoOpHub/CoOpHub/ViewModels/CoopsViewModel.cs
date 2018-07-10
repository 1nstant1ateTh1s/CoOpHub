using CoOpHub.Models;
using System.Collections.Generic;

namespace CoOpHub.ViewModels
{
	public class CoopsViewModel
	{
		public IEnumerable<Coop> UpcomingCoops { get; set; }
		public bool ShowActions { get; set; }
		public string Heading { get; set; }
		public string SearchTerm { get; set; }
	}
}