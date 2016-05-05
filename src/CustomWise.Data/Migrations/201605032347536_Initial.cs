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
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                        Id = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        Action = c.String(nullable: false, maxLength: 64),
                        ParentId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.VersionNumber })
                .ForeignKey("dbo.Artifacts", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MetaDataVersions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
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
                .PrimaryKey(t => new { t.Id, t.VersionNumber })
                .ForeignKey("dbo.MetaData", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SpecificationVersions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        VersionNumber = c.String(nullable: false, maxLength: 256),
                        Action = c.String(nullable: false, maxLength: 64),
                        ParentId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.VersionNumber })
                .ForeignKey("dbo.Specifications", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecificationVersions", "Id", "dbo.Specifications");
            DropForeignKey("dbo.MetaDataVersions", "Id", "dbo.MetaData");
            DropForeignKey("dbo.ArtifactVersions", "Id", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ParentId", "dbo.Artifacts");
            DropForeignKey("dbo.MetaData", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "ParentId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "ItemTypeId", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.MetaData", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaDataDefinitionDetails", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaData", "ArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ItemTypeId", "dbo.ArtifactTypes");
            DropIndex("dbo.SpecificationVersions", new[] { "Id" });
            DropIndex("dbo.MetaDataVersions", new[] { "Id" });
            DropIndex("dbo.ArtifactVersions", new[] { "Id" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.Specifications", new[] { "ItemTypeId" });
            DropIndex("dbo.Specifications", new[] { "ParentId" });
            DropIndex("dbo.MetaDataDefinitionDetails", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "ArtifactId" });
            DropIndex("dbo.MetaData", new[] { "SpecificationId" });
            DropIndex("dbo.Artifacts", new[] { "ItemTypeId" });
            DropIndex("dbo.Artifacts", new[] { "ParentId" });
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
