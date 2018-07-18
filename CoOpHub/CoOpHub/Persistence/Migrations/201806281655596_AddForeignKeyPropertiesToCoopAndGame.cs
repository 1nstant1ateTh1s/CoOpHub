namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToCoopAndGame : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Coops", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.Coops", name: "Host_Id", newName: "HostId");
            RenameColumn(table: "dbo.Games", name: "Genre_Id", newName: "GenreId");
            RenameIndex(table: "dbo.Coops", name: "IX_Host_Id", newName: "IX_HostId");
            RenameIndex(table: "dbo.Coops", name: "IX_Game_Id", newName: "IX_GameId");
            RenameIndex(table: "dbo.Games", name: "IX_Genre_Id", newName: "IX_GenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Games", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameIndex(table: "dbo.Coops", name: "IX_GameId", newName: "IX_Game_Id");
            RenameIndex(table: "dbo.Coops", name: "IX_HostId", newName: "IX_Host_Id");
            RenameColumn(table: "dbo.Games", name: "GenreId", newName: "Genre_Id");
            RenameColumn(table: "dbo.Coops", name: "HostId", newName: "Host_Id");
            RenameColumn(table: "dbo.Coops", name: "GameId", newName: "Game_Id");
        }
    }
}
