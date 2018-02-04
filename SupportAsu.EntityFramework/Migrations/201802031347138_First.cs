namespace SupportAsu.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Title = c.String(nullable: false, maxLength: 60),
                        Text = c.String(nullable: false),
                        CloseDate = c.DateTime(),
                        AuditoryId = c.Int(),
                        StatusId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryValues", t => t.AuditoryId)
                .ForeignKey("dbo.DictionaryValues", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.StatusId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AuditoryId)
                .Index(t => t.StatusId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.DictionaryValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DictionaryId = c.Int(nullable: false),
                        Value = c.String(),
                        Code = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.DictionaryId, cascadeDelete: false)
                .Index(t => t.DictionaryId);
            
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Code = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        Role = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClaimTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Claims", t => t.ClaimId, cascadeDelete: false)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .Index(t => t.ClaimId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponsibleId = c.Int(nullable: false),
                        Title = c.String(maxLength: 60),
                        Text = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        MainTaskId = c.Int(),
                        AuditoryId = c.Int(),
                        StatusId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryValues", t => t.AuditoryId)
                .ForeignKey("dbo.Tasks", t => t.MainTaskId)
                .ForeignKey("dbo.Users", t => t.ResponsibleId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.ResponsibleId)
                .Index(t => t.MainTaskId)
                .Index(t => t.AuditoryId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.CommentClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Claims", t => t.ItemId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.CommentTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.ItemId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AuditoryId = c.Int(nullable: false),
                        State = c.String(),
                        Note = c.String(),
                        Number = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryValues", t => t.AuditoryId, cascadeDelete: false)
                .Index(t => t.AuditoryId);
            
            CreateTable(
                "dbo.ProjectorShedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuditoryId = c.Int(nullable: false),
                        ResponsibleId = c.Int(nullable: false),
                        Teacher = c.String(nullable: false),
                        LessonId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryValues", t => t.AuditoryId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.DayId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.LessonId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ResponsibleId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.WeekId, cascadeDelete: false)
                .Index(t => t.AuditoryId)
                .Index(t => t.ResponsibleId)
                .Index(t => t.LessonId)
                .Index(t => t.DayId)
                .Index(t => t.WeekId);
            
            CreateTable(
                "dbo.Prophylaxis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuditoryId = c.Int(nullable: false),
                        ResponsibleId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DictionaryValues", t => t.AuditoryId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.DayId, cascadeDelete: false)
                .ForeignKey("dbo.DictionaryValues", t => t.LessonId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ResponsibleId, cascadeDelete: false)
                .Index(t => t.AuditoryId)
                .Index(t => t.ResponsibleId)
                .Index(t => t.DayId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Count = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: false)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.TaskExecutors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.ViewedClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Claims", t => t.ItemId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewedClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.ViewedClaims", "ItemId", "dbo.Claims");
            DropForeignKey("dbo.TaskExecutors", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskExecutors", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Prophylaxis", "ResponsibleId", "dbo.Users");
            DropForeignKey("dbo.Prophylaxis", "LessonId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Prophylaxis", "DayId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Prophylaxis", "AuditoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "WeekId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "ResponsibleId", "dbo.Users");
            DropForeignKey("dbo.ProjectorShedules", "LessonId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "DayId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ProjectorShedules", "AuditoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Equipments", "AuditoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.CommentTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.CommentTasks", "ItemId", "dbo.Tasks");
            DropForeignKey("dbo.CommentClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.CommentClaims", "ItemId", "dbo.Claims");
            DropForeignKey("dbo.ClaimTasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "StatusId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Tasks", "ResponsibleId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "MainTaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "AuditoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.ClaimTasks", "ClaimId", "dbo.Claims");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Claims", "StatusId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Claims", "CategoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.Claims", "AuditoryId", "dbo.DictionaryValues");
            DropForeignKey("dbo.DictionaryValues", "DictionaryId", "dbo.Dictionaries");
            DropIndex("dbo.ViewedClaims", new[] { "ItemId" });
            DropIndex("dbo.ViewedClaims", new[] { "UserId" });
            DropIndex("dbo.TaskExecutors", new[] { "TaskId" });
            DropIndex("dbo.TaskExecutors", new[] { "UserId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.Prophylaxis", new[] { "LessonId" });
            DropIndex("dbo.Prophylaxis", new[] { "DayId" });
            DropIndex("dbo.Prophylaxis", new[] { "ResponsibleId" });
            DropIndex("dbo.Prophylaxis", new[] { "AuditoryId" });
            DropIndex("dbo.ProjectorShedules", new[] { "WeekId" });
            DropIndex("dbo.ProjectorShedules", new[] { "DayId" });
            DropIndex("dbo.ProjectorShedules", new[] { "LessonId" });
            DropIndex("dbo.ProjectorShedules", new[] { "ResponsibleId" });
            DropIndex("dbo.ProjectorShedules", new[] { "AuditoryId" });
            DropIndex("dbo.Equipments", new[] { "AuditoryId" });
            DropIndex("dbo.CommentTasks", new[] { "ItemId" });
            DropIndex("dbo.CommentTasks", new[] { "UserId" });
            DropIndex("dbo.CommentClaims", new[] { "ItemId" });
            DropIndex("dbo.CommentClaims", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "StatusId" });
            DropIndex("dbo.Tasks", new[] { "AuditoryId" });
            DropIndex("dbo.Tasks", new[] { "MainTaskId" });
            DropIndex("dbo.Tasks", new[] { "ResponsibleId" });
            DropIndex("dbo.ClaimTasks", new[] { "TaskId" });
            DropIndex("dbo.ClaimTasks", new[] { "ClaimId" });
            DropIndex("dbo.DictionaryValues", new[] { "DictionaryId" });
            DropIndex("dbo.Claims", new[] { "CategoryId" });
            DropIndex("dbo.Claims", new[] { "StatusId" });
            DropIndex("dbo.Claims", new[] { "AuditoryId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            DropTable("dbo.ViewedClaims");
            DropTable("dbo.TaskExecutors");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Purchases");
            DropTable("dbo.Prophylaxis");
            DropTable("dbo.ProjectorShedules");
            DropTable("dbo.Equipments");
            DropTable("dbo.CommentTasks");
            DropTable("dbo.CommentClaims");
            DropTable("dbo.Tasks");
            DropTable("dbo.ClaimTasks");
            DropTable("dbo.Users");
            DropTable("dbo.Dictionaries");
            DropTable("dbo.DictionaryValues");
            DropTable("dbo.Claims");
        }
    }
}
