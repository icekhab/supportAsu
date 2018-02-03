namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prophylaxis", "LessonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Prophylaxis", "LessonId");
            AddForeignKey("dbo.Prophylaxis", "LessonId", "dbo.DictionaryValues", "Id", cascadeDelete: true);
            DropColumn("dbo.Prophylaxis", "Lesson");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prophylaxis", "Lesson", c => c.Int(nullable: false));
            DropForeignKey("dbo.Prophylaxis", "LessonId", "dbo.DictionaryValues");
            DropIndex("dbo.Prophylaxis", new[] { "LessonId" });
            DropColumn("dbo.Prophylaxis", "LessonId");
        }
    }
}
