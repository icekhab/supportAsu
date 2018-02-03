namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPurchaseAddNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Note");
        }
    }
}
