﻿using System.ComponentModel.DataAnnotations;

namespace CoOpHub.ViewModels
{
	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}