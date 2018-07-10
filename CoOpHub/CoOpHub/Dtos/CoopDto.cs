using System;

namespace CoOpHub.Dtos
{
	public class CoopDto
	{
		public int Id { get; set; }
		public bool IsCanceled { get; set; }
		public UserDto Host { get; set; }
		public DateTime DateTime { get; set; }
		public string Venue { get; set; }
		public GameDto Game { get; set; }
	}
}