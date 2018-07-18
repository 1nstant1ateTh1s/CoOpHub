namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForCoopsGamesAndGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coops", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Coops", "Host_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Coops", new[] { "Game_Id" });
            DropIndex("dbo.Coops", new[] { "Host_Id" });
            DropIndex("dbo.Games", new[] { "Genre_Id" });
            AlterColumn("dbo.Coops", "Venue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Coops", "Game_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Coops", "Host_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Games", "Genre_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Coops", "Game_Id");
            CreateIndex("dbo.Coops", "Host_Id");
            CreateIndex("dbo.Games", "Genre_Id");
            AddForeignKey("dbo.Coops", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Coops", "Host_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Coops", "Host_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Coops", "Game_Id", "dbo.Games");
            DropIndex("dbo.Games", new[] { "Genre_Id" });
            DropIndex("dbo.Coops", new[] { "Host_Id" });
            DropIndex("dbo.Coops", new[] { "Game_Id" });
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Games", "Genre_Id", c => c.Int());
            AlterColumn("dbo.Games", "Name", c => c.String());
            AlterColumn("dbo.Coops", "Host_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Coops", "Game_Id", c => c.Int());
            AlterColumn("dbo.Coops", "Venue", c => c.String());
            CreateIndex("dbo.Games", "Genre_Id");
            CreateIndex("dbo.Coops", "Host_Id");
            CreateIndex("dbo.Coops", "Game_Id");
            AddForeignKey("dbo.Games", "Genre_Id", "dbo.Genres", "Id");
            AddForeignKey("dbo.Coops", "Host_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Coops", "Game_Id", "dbo.Games", "Id");
        }
    }
}
