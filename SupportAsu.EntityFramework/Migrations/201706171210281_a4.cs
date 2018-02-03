namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Prophylaxis", name: "UserId", newName: "ResponsibleId");
            RenameIndex(table: "dbo.Prophylaxis", name: "IX_UserId", newName: "IX_ResponsibleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Prophylaxis", name: "IX_ResponsibleId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Prophylaxis", name: "ResponsibleId", newName: "UserId");
        }
    }
}
