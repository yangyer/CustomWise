namespace CustomWise.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
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
                        MetaDataDefinitionId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaDataDefinitions", t => t.MetaDataDefinitionId, cascadeDelete: true)
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
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        Action = c.String(nullable: false, maxLength: 64),
                        ArtifactId = c.Int(nullable: false),
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
                .Index(t => t.ArtifactId);
            
            CreateTable(
                "dbo.MetaDataVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        Action = c.String(nullable: false, maxLength: 64),
                        MetaDataId = c.Int(nullable: false),
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
                .Index(t => t.MetaDataId);
            
            CreateTable(
                "dbo.SpecificationVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        Action = c.String(nullable: false, maxLength: 64),
                        SpecificationId = c.Int(nullable: false),
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
                .Index(t => t.SpecificationId);
            
            CreateTable(
                "dbo.MetaDataArtifacts",
                c => new
                    {
                        MetaData_Id = c.Int(nullable: false),
                        Artifact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MetaData_Id, t.Artifact_Id })
                .ForeignKey("dbo.MetaData", t => t.MetaData_Id, cascadeDelete: true)
                .ForeignKey("dbo.Artifacts", t => t.Artifact_Id, cascadeDelete: true)
                .Index(t => t.MetaData_Id)
                .Index(t => t.Artifact_Id);
            
            CreateTable(
                "dbo.SpecificationMetaDatas",
                c => new
                    {
                        Specification_Id = c.Int(nullable: false),
                        MetaData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Specification_Id, t.MetaData_Id })
                .ForeignKey("dbo.Specifications", t => t.Specification_Id, cascadeDelete: true)
                .ForeignKey("dbo.MetaData", t => t.MetaData_Id, cascadeDelete: true)
                .Index(t => t.Specification_Id)
                .Index(t => t.MetaData_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecificationVersions", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.MetaDataVersions", "MetaDataId", "dbo.MetaData");
            DropForeignKey("dbo.ArtifactVersions", "ArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ParentId", "dbo.Artifacts");
            DropForeignKey("dbo.Specifications", "ParentId", "dbo.Specifications");
            DropForeignKey("dbo.SpecificationMetaDatas", "MetaData_Id", "dbo.MetaData");
            DropForeignKey("dbo.SpecificationMetaDatas", "Specification_Id", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "ItemTypeId", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.MetaData", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaDataDefinitionDetails", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaDataArtifacts", "Artifact_Id", "dbo.Artifacts");
            DropForeignKey("dbo.MetaDataArtifacts", "MetaData_Id", "dbo.MetaData");
            DropForeignKey("dbo.Artifacts", "ItemTypeId", "dbo.ArtifactTypes");
            DropIndex("dbo.SpecificationMetaDatas", new[] { "MetaData_Id" });
            DropIndex("dbo.SpecificationMetaDatas", new[] { "Specification_Id" });
            DropIndex("dbo.MetaDataArtifacts", new[] { "Artifact_Id" });
            DropIndex("dbo.MetaDataArtifacts", new[] { "MetaData_Id" });
            DropIndex("dbo.SpecificationVersions", new[] { "SpecificationId" });
            DropIndex("dbo.MetaDataVersions", new[] { "MetaDataId" });
            DropIndex("dbo.ArtifactVersions", new[] { "ArtifactId" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.Specifications", new[] { "ItemTypeId" });
            DropIndex("dbo.Specifications", new[] { "ParentId" });
            DropIndex("dbo.MetaDataDefinitionDetails", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.Artifacts", new[] { "ItemTypeId" });
            DropIndex("dbo.Artifacts", new[] { "ParentId" });
            DropTable("dbo.SpecificationMetaDatas");
            DropTable("dbo.MetaDataArtifacts");
            DropTable("dbo.SpecificationVersions");
            DropTable("dbo.MetaDataVersions");
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
