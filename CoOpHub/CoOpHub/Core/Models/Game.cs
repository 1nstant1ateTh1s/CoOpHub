﻿namespace CoOpHub.Core.Models
{
	public class Game
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Genre Genre { get; set; }
		public int GenreId { get; set; }
	}
}