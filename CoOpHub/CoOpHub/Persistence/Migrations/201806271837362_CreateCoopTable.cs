namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCoopTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(),
                        Game_Id = c.Int(),
                        Host_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Host_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Host_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Genre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.Genre_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coops", "Host_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Coops", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Games", new[] { "Genre_Id" });
            DropIndex("dbo.Coops", new[] { "Host_Id" });
            DropIndex("dbo.Coops", new[] { "Game_Id" });
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Coops");
        }
    }
}
