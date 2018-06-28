using System.ComponentModel.DataAnnotations;

namespace CoOpHub.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}