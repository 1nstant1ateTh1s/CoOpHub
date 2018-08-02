using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace CoOpHub.IntegrationTests
{
	[SetUpFixture]
	class GlobalSetUp
	{
		[SetUp]
		public void SetUp()
		{
			// Get Code-First configurations to bring integration testing database to latest version
			var configuration = new CoOpHub.Migrations.Configuration();

			// Migrate integration testing database to latest version
			var migrator = new DbMigrator(configuration);
			migrator.Update();
		}
	}
}
