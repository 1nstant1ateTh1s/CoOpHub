using CoOpHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoOpHub.ViewModels
{
	public class CoopFormViewModel
	{
		[Required]
		public string Venue { get; set; }

		[Required]
		[FutureDate]
		public string Date { get; set; }

		[Required]
		[ValidTime]
		public string Time { get; set; }

		[Required]
		public int Game { get; set; }

		public IEnumerable<Game> Games { get; set; }

		public DateTime GetDateTime()
		{
			return DateTime.Parse(string.Format("{0} {1}", Date, Time));
		}
	}
}