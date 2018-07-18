using CoOpHub.Controllers;
using CoOpHub.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CoOpHub.Core.ViewModels
{
	public class CoopFormViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Venue { get; set; }

		[Required]
		[FutureDate]
		public string Date { get; set; }

		[Required]
		[ValidTime]
		public string Time { get; set; }

		[Required]
		public int Game { get; set; }

		public IEnumerable<Game> Games { get; set; }

		public string Heading { get; set; }

		public string Action
		{
			get
			{
				// Using lambda expression to retrieve name of "Update" action - this ensures we aren't using a "magic string" for the name of the update action.
				// *NOTE: This is a lambda expression that represents the "Update" action in the Coops controller.*
				Expression<Func<CoopsController, ActionResult>> update =
					(c => c.Update(this));

				// Lambda expression representing the "Create" action in the Coops controller.
				Expression<Func<CoopsController, ActionResult>> create =
					(c => c.Create(this));

				// Depending on value of Id property, select one of these expressions to get the method name at runtime
				// *NOTE: action variable is also an expression.*
				var action = (Id != 0) ? update : create;

				return (action.Body as MethodCallExpression).Method.Name;
			}
		}

		public DateTime GetDateTime()
		{
			return DateTime.Parse(string.Format("{0} {1}", Date, Time));
		}
	}
}