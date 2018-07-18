namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OriginalDateTime = c.DateTime(),
                        OriginalVenue = c.String(),
                        Coop_Id = c.Int(nullable: false),
                        OriginalGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coops", t => t.Coop_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.OriginalGame_Id)
                .Index(t => t.Coop_Id)
                .Index(t => t.OriginalGame_Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NotificationId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "OriginalGame_Id", "dbo.Games");
            DropForeignKey("dbo.Notifications", "Coop_Id", "dbo.Coops");
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "OriginalGame_Id" });
            DropIndex("dbo.Notifications", new[] { "Coop_Id" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
