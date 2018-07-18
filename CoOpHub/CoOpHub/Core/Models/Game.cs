using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Core.Models
{
	public class Game
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		public Genre Genre { get; set; }

		[Required]
		public int GenreId { get; set; }
	}
}