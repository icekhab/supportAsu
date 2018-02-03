namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "State", c => c.String());
            DropColumn("dbo.Equipments", "IsBreak");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "IsBreak", c => c.Boolean(nullable: false));
            DropColumn("dbo.Equipments", "State");
        }
    }
}
