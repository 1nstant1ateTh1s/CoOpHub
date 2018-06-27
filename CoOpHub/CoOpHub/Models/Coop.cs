using System;
using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Models
{
	public class Coop
	{
		public int Id { get; set; }

		[Required]
		public ApplicationUser Host { get; set; }

		public DateTime DateTime { get; set; }

		[Required]
		[StringLength(255)]
		public string Venue { get; set; }

		[Required]
		public Game Game { get; set; }
	}
}