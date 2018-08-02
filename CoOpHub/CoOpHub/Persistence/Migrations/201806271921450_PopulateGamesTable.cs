namespace CoOpHub.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulateGamesTable : DbMigration
	{
		public override void Up()
		{
			// Games table - up
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('NBA 2K19', 7)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Mario Kart 8', 6)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Portal 2', 1)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('DnD', 2)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Gwent', 4)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Diablo 3', 3)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Total War: Warhammer 2', 5)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Monster Hunter: World', 3)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Rocket League', 7)");
		}

		public override void Down()
		{
			// Games table - down
			Sql("DELETE FROM Games WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
		}
	}
}
