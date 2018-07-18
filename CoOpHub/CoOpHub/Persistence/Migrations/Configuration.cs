namespace CoOpHub.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<CoOpHub.Persistence.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;

			// Tell Entity Framework we have moved the default location for Migrations to the Persistence folder
			MigrationsDirectory = @"Persistence\Migrations";
		}

		protected override void Seed(CoOpHub.Persistence.ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}
}
