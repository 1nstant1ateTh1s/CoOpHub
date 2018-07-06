namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNotificationColumnNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notifications", name: "Coop_Id", newName: "CoopId");
            RenameColumn(table: "dbo.Notifications", name: "OriginalGame_Id", newName: "OriginalGameId");
            RenameIndex(table: "dbo.Notifications", name: "IX_OriginalGame_Id", newName: "IX_OriginalGameId");
            RenameIndex(table: "dbo.Notifications", name: "IX_Coop_Id", newName: "IX_CoopId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notifications", name: "IX_CoopId", newName: "IX_Coop_Id");
            RenameIndex(table: "dbo.Notifications", name: "IX_OriginalGameId", newName: "IX_OriginalGame_Id");
            RenameColumn(table: "dbo.Notifications", name: "OriginalGameId", newName: "OriginalGame_Id");
            RenameColumn(table: "dbo.Notifications", name: "CoopId", newName: "Coop_Id");
        }
    }
}
