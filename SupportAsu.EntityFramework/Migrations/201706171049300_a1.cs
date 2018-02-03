namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Purchases", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "Note", c => c.String());
        }
    }
}
