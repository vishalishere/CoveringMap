namespace CoverMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTechnology : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Covers", "Technology", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Covers", "Technology");
        }
    }
}
