namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseDetails", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseDetails", "Name", c => c.String());
        }
    }
}
