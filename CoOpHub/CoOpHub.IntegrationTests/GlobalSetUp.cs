using CoOpHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CoOpHub.IntegrationTests
{
	[SetUpFixture]
	class GlobalSetUp
	{
		[SetUp]
		public void SetUp()
		{
			MigrateDbToLatestVersion();
			Seed();
		}

		private static void MigrateDbToLatestVersion()
		{
			// Get Code-First configurations to bring integration testing database to latest version
			var configuration = new CoOpHub.Migrations.Configuration();

			// Migrate integration testing database to latest version
			var migrator = new DbMigrator(configuration);
			migrator.Update();
		}

		/// <summary>
		/// Populate the integration test database w/ required data.
		/// </summary>
		public void Seed()
		{
			// Add a couple users to integration test database
			var context = new ApplicationDbContext();

			// But only create new users if none already exist
			if (context.Users.Any())
			{
				return;
			}

			context.Users.Add(new Core.Models.ApplicationUser { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
			context.Users.Add(new Core.Models.ApplicationUser { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
			context.SaveChanges();
		}
	}
}
