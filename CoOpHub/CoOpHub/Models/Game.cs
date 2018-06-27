using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Models
{
	public class Game
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		public Genre Genre { get; set; }
	}
}