using NUnit.Framework;
using System;
using System.Transactions;

namespace CoOpHub.IntegrationTests
{
	public class Isolated : Attribute, ITestAction
	{
		private TransactionScope _transactionScope;

		public ActionTargets Targets
		{
			get { return ActionTargets.Test; } // means this attribute can only be applied to Test methods
		}

		public void BeforeTest(TestDetails testDetails)
		{
			_transactionScope = new TransactionScope();
		}

		public void AfterTest(TestDetails testDetails)
		{
			// Any data added to the integration test database will be rolled back after testing is done.
			_transactionScope.Dispose();
		}
	}
}
