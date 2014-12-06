namespace CoverMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtimestamps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Covers", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Covers", "Updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Covers", "Updated");
            DropColumn("dbo.Covers", "Created");
        }
    }
}
