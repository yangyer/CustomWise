namespace CustomWise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artifacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentArtifactId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        RecordTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artifacts", t => t.ParentArtifactId)
                .ForeignKey("dbo.RecordTypes", t => t.RecordTypeId, cascadeDelete: true)
                .Index(t => t.ParentArtifactId)
                .Index(t => t.RecordTypeId);
            
            CreateTable(
                "dbo.MetaData",
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
                        Artifact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId, cascadeDelete: true)
                .ForeignKey("dbo.Artifacts", t => t.Artifact_Id)
                .Index(t => t.SpecificationId)
                .Index(t => t.Artifact_Id);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentSpecificationId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        RecordTypeId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Specifications", t => t.ParentSpecificationId)
                .ForeignKey("dbo.RecordTypes", t => t.RecordTypeId, cascadeDelete: true)
                .ForeignKey("dbo.SpecificationVersions", t => t.SpecificationVersionId, cascadeDelete: true)
                .Index(t => t.ParentSpecificationId)
                .Index(t => t.RecordTypeId)
                .Index(t => t.SpecificationVersionId);
            
            CreateTable(
                "dbo.Configurations",
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
                .ForeignKey("dbo.Specifications", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId);
            
            CreateTable(
                "dbo.RecordTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetaDataTypeId = c.Int(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        SystemName = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaDataTypes", t => t.MetaDataTypeId, cascadeDelete: true)
                .Index(t => t.MetaDataTypeId);
            
            CreateTable(
                "dbo.MetaDataTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationLocals",
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
                .ForeignKey("dbo.Specifications", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationVersions",
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
            DropForeignKey("dbo.Artifacts", "RecordTypeId", "dbo.RecordTypes");
            DropForeignKey("dbo.Artifacts", "ParentArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.MetaData", "Artifact_Id", "dbo.Artifacts");
            DropForeignKey("dbo.MetaData", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "SpecificationVersionId", "dbo.SpecificationVersions");
            DropForeignKey("dbo.SpecificationLocals", "Id", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "RecordTypeId", "dbo.RecordTypes");
            DropForeignKey("dbo.RecordTypes", "MetaDataTypeId", "dbo.MetaDataTypes");
            DropForeignKey("dbo.Specifications", "ParentSpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropIndex("dbo.SpecificationLocals", new[] { "Id" });
            DropIndex("dbo.RecordTypes", new[] { "MetaDataTypeId" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.Specifications", new[] { "SpecificationVersionId" });
            DropIndex("dbo.Specifications", new[] { "RecordTypeId" });
            DropIndex("dbo.Specifications", new[] { "ParentSpecificationId" });
            DropIndex("dbo.MetaData", new[] { "Artifact_Id" });
            DropIndex("dbo.MetaData", new[] { "SpecificationId" });
            DropIndex("dbo.Artifacts", new[] { "RecordTypeId" });
            DropIndex("dbo.Artifacts", new[] { "ParentArtifactId" });
            DropTable("dbo.SpecificationVersions");
            DropTable("dbo.SpecificationLocals");
            DropTable("dbo.MetaDataTypes");
            DropTable("dbo.RecordTypes");
            DropTable("dbo.Configurations");
            DropTable("dbo.Specifications");
            DropTable("dbo.MetaData");
            DropTable("dbo.Artifacts");
        }
    }
}
