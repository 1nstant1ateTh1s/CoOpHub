namespace CoOpHub.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulateGamesAndGenresTable : DbMigration
	{
		public override void Up()
		{
			// Genres table - Up
			//Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Puzzle')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Tabletop')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Action/RPG')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'CCG')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Strategy')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Racing')");
			//Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Sports')");
			Sql("INSERT INTO Genres (Name) VALUES ('Puzzle')");
			Sql("INSERT INTO Genres (Name) VALUES ('Tabletop')");
			Sql("INSERT INTO Genres (Name) VALUES ('Action/RPG')");
			Sql("INSERT INTO Genres (Name) VALUES ('CCG')");
			Sql("INSERT INTO Genres (Name) VALUES ('Strategy')");
			Sql("INSERT INTO Genres (Name) VALUES ('Racing')");
			Sql("INSERT INTO Genres (Name) VALUES ('Sports')");

			// Games table - up
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (1, 'NBA 2K19', 7)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (2, 'Mario Kart 8', 6)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (3, 'Portal 2', 1)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (4, 'DnD', 2)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (5, 'Gwent', 4)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (6, 'Diablo 3', 3)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (7, 'Total War: Warhammer 2', 5)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (8, 'Monster Hunter: World', 3)");
			//Sql("INSERT INTO Games (Id, Name, Genre_Id) VALUES (9, 'Rocket League', 7)");

			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('NBA 2K19', 7)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Mario Kart 8', 6)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Portal 2', 1)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('DnD', 2)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Gwent', 4)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Diablo 3', 3)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Total War: Warhammer 2', 5)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Monster Hunter: World', 3)");
			//Sql("INSERT INTO Games (Name, Genre_Id) VALUES ('Rocket League', 7)");
		}

		public override void Down()
		{
			// Games table - down
			//Sql("DELETE FROM Games WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");

			// Genres table - Down
			Sql("DELETE FROM Genres WHERE Id IN (16, 17, 18, 19, 20, 21, 22)");
		}
	}
}
