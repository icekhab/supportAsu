namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectorshedulechange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProjectorShedules", name: "UserId", newName: "ResponsibleId");
            RenameIndex(table: "dbo.ProjectorShedules", name: "IX_UserId", newName: "IX_ResponsibleId");
            AddColumn("dbo.ProjectorShedules", "AuditoryId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectorShedules", "LessonId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectorShedules", "WeekId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectorShedules", "AuditoryId");
            CreateIndex("dbo.ProjectorShedules", "LessonId");
            CreateIndex("dbo.ProjectorShedules", "WeekId");
            AddForeignKey("dbo.ProjectorShedules", "AuditoryId", "dbo.DictionaryValues", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ProjectorShedules", "LessonId", "dbo.DictionaryValues", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ProjectorShedules", "WeekId", "dbo.DictionaryValues", "Id", cascadeDelete: false);
            DropColumn("dbo.ProjectorShedules", "Auditory");
            DropColumn("dbo.ProjectorShedules", "Lesson");
            DropColumn("dbo.ProjectorShedules", "Week");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectorShedules", "Week", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectorShedules", "Lesson", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectorShedules", "Auditory", c => c.String(nullable: false));
            DropForeignKey("dbo.ProjectorShedules", "WeekId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "LessonId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "AuditoryId", "dbo.DictionaryValues");
            DropIndex("dbo.ProjectorShedules", new[] { "WeekId" });
            DropIndex("dbo.ProjectorShedules", new[] { "LessonId" });
            DropIndex("dbo.ProjectorShedules", new[] { "AuditoryId" });
            DropColumn("dbo.ProjectorShedules", "WeekId");
            DropColumn("dbo.ProjectorShedules", "LessonId");
            DropColumn("dbo.ProjectorShedules", "AuditoryId");
            RenameIndex(table: "dbo.ProjectorShedules", name: "IX_ResponsibleId", newName: "IX_UserId");
            RenameColumn(table: "dbo.ProjectorShedules", name: "ResponsibleId", newName: "UserId");
        }
    }
}
