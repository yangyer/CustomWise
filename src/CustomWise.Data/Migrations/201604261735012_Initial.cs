namespace CustomWise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configuration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specification", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId);
            
            CreateTable(
                "dbo.Specification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentSpecificationId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        SpecificationTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        SpecificationVersionId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specification", t => t.ParentSpecificationId)
                .ForeignKey("dbo.SpecificationType", t => t.SpecificationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.SpecificationVersion", t => t.SpecificationVersionId, cascadeDelete: true)
                .Index(t => t.ParentSpecificationId)
                .Index(t => t.SpecificationTypeId)
                .Index(t => t.SpecificationVersionId);
            
            CreateTable(
                "dbo.SpecificationLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.Specification", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationMetaData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specification", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId);
            
            CreateTable(
                "dbo.SpecificationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        SystemName = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationTypeLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LanguageCode = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.LanguageCode })
                .ForeignKey("dbo.SpecificationType", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationVersion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Published = c.Boolean(nullable: false),
                        PublishedDate = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Configuration", "SpecificationId", "dbo.Specification");
            DropForeignKey("dbo.Specification", "SpecificationVersionId", "dbo.SpecificationVersion");
            DropForeignKey("dbo.Specification", "SpecificationTypeId", "dbo.SpecificationType");
            DropForeignKey("dbo.SpecificationTypeLanguage", "Id", "dbo.SpecificationType");
            DropForeignKey("dbo.SpecificationMetaData", "SpecificationId", "dbo.Specification");
            DropForeignKey("dbo.SpecificationLanguage", "Id", "dbo.Specification");
            DropForeignKey("dbo.Specification", "ParentSpecificationId", "dbo.Specification");
            DropIndex("dbo.SpecificationTypeLanguage", new[] { "Id" });
            DropIndex("dbo.SpecificationMetaData", new[] { "SpecificationId" });
            DropIndex("dbo.SpecificationLanguage", new[] { "Id" });
            DropIndex("dbo.Specification", new[] { "SpecificationVersionId" });
            DropIndex("dbo.Specification", new[] { "SpecificationTypeId" });
            DropIndex("dbo.Specification", new[] { "ParentSpecificationId" });
            DropIndex("dbo.Configuration", new[] { "SpecificationId" });
            DropTable("dbo.SpecificationVersion");
            DropTable("dbo.SpecificationTypeLanguage");
            DropTable("dbo.SpecificationType");
            DropTable("dbo.SpecificationMetaData");
            DropTable("dbo.SpecificationLanguage");
            DropTable("dbo.Specification");
            DropTable("dbo.Configuration");
        }
    }
}
