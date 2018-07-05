using System;
using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Models
{
	public class Coop
	{
		public int Id { get; set; }

		public bool IsCanceled { get; set; }

		public ApplicationUser Host { get; set; }

		[Required]
		public string HostId { get; set; }		/* "navigation" property */

		public DateTime DateTime { get; set; }

		[Required]
		[StringLength(255)]
		public string Venue { get; set; }

		public Game Game { get; set; }

		[Required]
		public int GameId { get; set; }			/* "navigation" property */
	}
}