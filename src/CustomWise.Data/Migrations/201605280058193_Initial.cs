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
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        ArtifactSystemTypeID = c.Int(nullable: false),
                        ArtifactTypeID = c.Int(),
                        ParentID = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArtifactSystemTypes", t => t.ArtifactSystemTypeID)
                .ForeignKey("dbo.ArtifactTypes", t => t.ArtifactTypeID)
                .ForeignKey("dbo.Artifacts", t => t.ParentID)
                .Index(t => t.ArtifactSystemTypeID)
                .Index(t => t.ArtifactTypeID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.ArtifactSystemTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ArtifactTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(nullable: false, maxLength: 256),
                        SystemType = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArtifactTypes", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.ArtifactTypeMetadataDefinitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 256),
                        ValueType = c.String(nullable: false, maxLength: 960),
                        DefaultValue = c.String(maxLength: 960),
                        AllowOverride = c.Boolean(nullable: false),
                        AllowReferenceOverride = c.Boolean(nullable: false),
                        InputTemplate = c.String(),
                        Required = c.Boolean(nullable: false),
                        ReferenceRequired = c.Boolean(nullable: false),
                        Localized = c.Boolean(nullable: false),
                        ArtifactTypeID = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArtifactTypes", t => t.ArtifactTypeID)
                .Index(t => t.ArtifactTypeID);
            
            CreateTable(
                "dbo.ArtifactMetadata",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        ArtifactID = c.Int(nullable: false),
                        MetadataDefinitionId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artifacts", t => t.ArtifactID)
                .ForeignKey("dbo.ArtifactTypeMetadataDefinitions", t => t.MetadataDefinitionId)
                .Index(t => t.ArtifactID)
                .Index(t => t.MetadataDefinitionId);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId)
                .Index(t => t.SpecificationId);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceID = c.String(),
                        SpecificationSystemTypeID = c.Int(nullable: false),
                        SpecificationTypeID = c.Int(),
                        ParentID = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Specifications", t => t.ParentID)
                .ForeignKey("dbo.SpecificationSystemTypes", t => t.SpecificationSystemTypeID)
                .ForeignKey("dbo.SpecificationTypes", t => t.SpecificationTypeID)
                .Index(t => t.SpecificationSystemTypeID)
                .Index(t => t.SpecificationTypeID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.SpecificationMetadata",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        SpecificationID = c.Int(nullable: false),
                        MetadataDefinitionID = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SpecificationTypeMetadataDefinitions", t => t.MetadataDefinitionID)
                .ForeignKey("dbo.Specifications", t => t.SpecificationID)
                .Index(t => t.SpecificationID)
                .Index(t => t.MetadataDefinitionID);
            
            CreateTable(
                "dbo.SpecificationTypeMetadataDefinitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 256),
                        ValueType = c.String(nullable: false, maxLength: 960),
                        DefaultValue = c.String(maxLength: 960),
                        AllowOverride = c.Boolean(nullable: false),
                        AllowReferenceOverride = c.Boolean(nullable: false),
                        InputTemplate = c.String(),
                        Required = c.Boolean(nullable: false),
                        ReferenceRequired = c.Boolean(nullable: false),
                        Localized = c.Boolean(nullable: false),
                        SpecificationTypeID = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SpecificationTypes", t => t.SpecificationTypeID)
                .Index(t => t.SpecificationTypeID);
            
            CreateTable(
                "dbo.SpecificationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(nullable: false, maxLength: 256),
                        SystemType = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SpecificationTypes", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.SpecificationSystemTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "SpecificationTypeID", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Specifications", "SpecificationSystemTypeID", "dbo.SpecificationSystemTypes");
            DropForeignKey("dbo.Specifications", "ParentID", "dbo.Specifications");
            DropForeignKey("dbo.SpecificationMetadata", "SpecificationID", "dbo.Specifications");
            DropForeignKey("dbo.SpecificationMetadata", "MetadataDefinitionID", "dbo.SpecificationTypeMetadataDefinitions");
            DropForeignKey("dbo.SpecificationTypeMetadataDefinitions", "SpecificationTypeID", "dbo.SpecificationTypes");
            DropForeignKey("dbo.SpecificationTypes", "ParentID", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Artifacts", "ParentID", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ArtifactTypeID", "dbo.ArtifactTypes");
            DropForeignKey("dbo.ArtifactTypes", "ParentID", "dbo.ArtifactTypes");
            DropForeignKey("dbo.ArtifactTypeMetadataDefinitions", "ArtifactTypeID", "dbo.ArtifactTypes");
            DropForeignKey("dbo.ArtifactMetadata", "MetadataDefinitionId", "dbo.ArtifactTypeMetadataDefinitions");
            DropForeignKey("dbo.ArtifactMetadata", "ArtifactID", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ArtifactSystemTypeID", "dbo.ArtifactSystemTypes");
            DropIndex("dbo.SpecificationTypes", new[] { "ParentID" });
            DropIndex("dbo.SpecificationTypeMetadataDefinitions", new[] { "SpecificationTypeID" });
            DropIndex("dbo.SpecificationMetadata", new[] { "MetadataDefinitionID" });
            DropIndex("dbo.SpecificationMetadata", new[] { "SpecificationID" });
            DropIndex("dbo.Specifications", new[] { "ParentID" });
            DropIndex("dbo.Specifications", new[] { "SpecificationTypeID" });
            DropIndex("dbo.Specifications", new[] { "SpecificationSystemTypeID" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.ArtifactMetadata", new[] { "MetadataDefinitionId" });
            DropIndex("dbo.ArtifactMetadata", new[] { "ArtifactID" });
            DropIndex("dbo.ArtifactTypeMetadataDefinitions", new[] { "ArtifactTypeID" });
            DropIndex("dbo.ArtifactTypes", new[] { "ParentID" });
            DropIndex("dbo.Artifacts", new[] { "ParentID" });
            DropIndex("dbo.Artifacts", new[] { "ArtifactTypeID" });
            DropIndex("dbo.Artifacts", new[] { "ArtifactSystemTypeID" });
            DropTable("dbo.SpecificationSystemTypes");
            DropTable("dbo.SpecificationTypes");
            DropTable("dbo.SpecificationTypeMetadataDefinitions");
            DropTable("dbo.SpecificationMetadata");
            DropTable("dbo.Specifications");
            DropTable("dbo.Configurations");
            DropTable("dbo.ArtifactMetadata");
            DropTable("dbo.ArtifactTypeMetadataDefinitions");
            DropTable("dbo.ArtifactTypes");
            DropTable("dbo.ArtifactSystemTypes");
            DropTable("dbo.Artifacts");
        }
    }
}
