namespace CoOpHub.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulateGamesTable : DbMigration
	{
		public override void Up()
		{
			// Games table - up
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('NBA 2K19', 22)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Mario Kart 8', 21)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Portal 2', 16)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('DnD', 17)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Gwent', 19)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Diablo 3', 18)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Total War: Warhammer 2', 20)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Monster Hunter: World', 18)");
			Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Rocket League', 22)");
		}

		public override void Down()
		{
			// Games table - down
			Sql("DELETE FROM Games WHERE Id IN (2, 3, 4, 5, 6, 7, 8, 9, 10)");
		}
	}
}
