namespace CoOpHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceledToCoop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coops", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coops", "IsCanceled");
        }
    }
}
