using System.ComponentModel.DataAnnotations;

namespace CoOpHub.Core.ViewModels
{
	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}