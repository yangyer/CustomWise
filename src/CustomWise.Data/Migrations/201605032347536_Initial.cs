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
                        ParentId = c.Int(),
                        DisplayName = c.String(maxLength: 256),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtifactTypes", t => t.ItemTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Artifacts", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ItemTypeId);
            
            CreateTable(
                "dbo.ArtifactTypes",
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
                "dbo.MetaData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(),
                        ArtifactId = c.Int(),
                        MetaDataDefinitionId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artifacts", t => t.ArtifactId)
                .ForeignKey("dbo.MetaDataDefinitions", t => t.MetaDataDefinitionId, cascadeDelete: true)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId)
                .Index(t => t.SpecificationId)
                .Index(t => t.ArtifactId)
                .Index(t => t.MetaDataDefinitionId);
            
            CreateTable(
                "dbo.MetaDataDefinitions",
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
                "dbo.MetaDataDefinitionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 24),
                        MetaDataDefinitionId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaDataDefinitions", t => t.MetaDataDefinitionId, cascadeDelete: true)
                .Index(t => t.MetaDataDefinitionId);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        DisplayName = c.String(maxLength: 256),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpecificationTypes", t => t.ItemTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Specifications", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ItemTypeId);
            
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
                "dbo.SpecificationTypes",
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
                "dbo.ArtifactVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtifactId = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        VersionHeaderId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 64),
                        ParentId = c.Int(),
                        DisplayName = c.String(maxLength: 256),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artifacts", t => t.ArtifactId, cascadeDelete: true)
                .ForeignKey("dbo.VersionHeaders", t => t.VersionHeaderId, cascadeDelete: true)
                .Index(t => t.ArtifactId)
                .Index(t => t.VersionHeaderId);
            
            CreateTable(
                "dbo.VersionHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Published = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        VersionHeaderId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 64),
                        ParentId = c.Int(),
                        DisplayName = c.String(maxLength: 256),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId, cascadeDelete: true)
                .ForeignKey("dbo.VersionHeaders", t => t.VersionHeaderId, cascadeDelete: true)
                .Index(t => t.SpecificationId)
                .Index(t => t.VersionHeaderId);
            
            CreateTable(
                "dbo.MetaDataVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetaDataId = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        VersionHeaderId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 64),
                        SpecificationId = c.Int(),
                        ArtifactId = c.Int(),
                        MetaDataDefinitionId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaData", t => t.MetaDataId, cascadeDelete: true)
                .ForeignKey("dbo.VersionHeaders", t => t.VersionHeaderId, cascadeDelete: true)
                .Index(t => t.MetaDataId)
                .Index(t => t.VersionHeaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetaDataVersions", "VersionHeaderId", "dbo.VersionHeaders");
            DropForeignKey("dbo.MetaDataVersions", "MetaDataId", "dbo.MetaData");
            DropForeignKey("dbo.ArtifactVersions", "VersionHeaderId", "dbo.VersionHeaders");
            DropForeignKey("dbo.SpecificationVersions", "VersionHeaderId", "dbo.VersionHeaders");
            DropForeignKey("dbo.SpecificationVersions", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.ArtifactVersions", "ArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ParentId", "dbo.Artifacts");
            DropForeignKey("dbo.MetaData", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "ParentId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "ItemTypeId", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.MetaData", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaDataDefinitionDetails", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaData", "ArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ItemTypeId", "dbo.ArtifactTypes");
            DropIndex("dbo.MetaDataVersions", new[] { "VersionHeaderId" });
            DropIndex("dbo.MetaDataVersions", new[] { "MetaDataId" });
            DropIndex("dbo.SpecificationVersions", new[] { "VersionHeaderId" });
            DropIndex("dbo.SpecificationVersions", new[] { "SpecificationId" });
            DropIndex("dbo.ArtifactVersions", new[] { "VersionHeaderId" });
            DropIndex("dbo.ArtifactVersions", new[] { "ArtifactId" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.Specifications", new[] { "ItemTypeId" });
            DropIndex("dbo.Specifications", new[] { "ParentId" });
            DropIndex("dbo.MetaDataDefinitionDetails", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "ArtifactId" });
            DropIndex("dbo.MetaData", new[] { "SpecificationId" });
            DropIndex("dbo.Artifacts", new[] { "ItemTypeId" });
            DropIndex("dbo.Artifacts", new[] { "ParentId" });
            DropTable("dbo.MetaDataVersions");
            DropTable("dbo.SpecificationVersions");
            DropTable("dbo.VersionHeaders");
            DropTable("dbo.ArtifactVersions");
            DropTable("dbo.SpecificationTypes");
            DropTable("dbo.Configurations");
            DropTable("dbo.Specifications");
            DropTable("dbo.MetaDataDefinitionDetails");
            DropTable("dbo.MetaDataDefinitions");
            DropTable("dbo.MetaData");
            DropTable("dbo.ArtifactTypes");
            DropTable("dbo.Artifacts");
        }
    }
}
