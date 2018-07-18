using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CoOpHub.Core.ViewModels
{
	public class FutureDate : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			DateTime dateTime;
			bool isValid = DateTime.TryParseExact(Convert.ToString(value), 
				"d MMM yyyy", 
				CultureInfo.CurrentCulture, 
				DateTimeStyles.None, 
				out dateTime);

			// Make sure date is in a good format & is set in the future
			return (isValid && dateTime > DateTime.Now);
		}

	}
}