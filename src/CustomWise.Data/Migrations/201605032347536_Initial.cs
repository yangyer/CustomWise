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
                        ArtifactVersionId = c.Int(nullable: false),
                        ParentArtifactId = c.Int(),
                        ArtifactTypeId = c.Int(nullable: false),
                        DisplayName = c.String(nullable: false, maxLength: 64),
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
                .ForeignKey("dbo.ArtifactTypes", t => t.ArtifactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ArtifactVersions", t => t.ArtifactVersionId, cascadeDelete: true)
                .ForeignKey("dbo.Artifacts", t => t.ParentArtifactId)
                .Index(t => t.ArtifactVersionId)
                .Index(t => t.ParentArtifactId)
                .Index(t => t.ArtifactTypeId);
            
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
                "dbo.ArtifactVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreviousArtifactVersionId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 64),
                        Published = c.Boolean(nullable: false),
                        PublishedDate = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtifactVersions", t => t.PreviousArtifactVersionId)
                .Index(t => t.PreviousArtifactVersionId);
            
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
                        ParentSpecificationId = c.Int(),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        SpecificationTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        ArtifactReferenceId = c.String(),
                        SpecificationVersionId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specifications", t => t.ParentSpecificationId)
                .ForeignKey("dbo.SpecificationTypes", t => t.SpecificationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.SpecificationVersions", t => t.SpecificationVersionId, cascadeDelete: true)
                .Index(t => t.ParentSpecificationId)
                .Index(t => t.SpecificationTypeId)
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
                "dbo.SpecificationVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreviousVersionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        Published = c.Boolean(nullable: false),
                        PublishedDate = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(nullable: false, maxLength: 64),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpecificationVersions", t => t.PreviousVersionId)
                .Index(t => t.PreviousVersionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artifacts", "ParentArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.MetaData", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Specifications", "SpecificationVersionId", "dbo.SpecificationVersions");
            DropForeignKey("dbo.SpecificationVersions", "PreviousVersionId", "dbo.SpecificationVersions");
            DropForeignKey("dbo.Specifications", "SpecificationTypeId", "dbo.SpecificationTypes");
            DropForeignKey("dbo.Specifications", "ParentSpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.Configurations", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.MetaData", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaDataDefinitionDetails", "MetaDataDefinitionId", "dbo.MetaDataDefinitions");
            DropForeignKey("dbo.MetaData", "ArtifactId", "dbo.Artifacts");
            DropForeignKey("dbo.Artifacts", "ArtifactVersionId", "dbo.ArtifactVersions");
            DropForeignKey("dbo.ArtifactVersions", "PreviousArtifactVersionId", "dbo.ArtifactVersions");
            DropForeignKey("dbo.Artifacts", "ArtifactTypeId", "dbo.ArtifactTypes");
            DropIndex("dbo.SpecificationVersions", new[] { "PreviousVersionId" });
            DropIndex("dbo.Configurations", new[] { "SpecificationId" });
            DropIndex("dbo.Specifications", new[] { "SpecificationVersionId" });
            DropIndex("dbo.Specifications", new[] { "SpecificationTypeId" });
            DropIndex("dbo.Specifications", new[] { "ParentSpecificationId" });
            DropIndex("dbo.MetaDataDefinitionDetails", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "MetaDataDefinitionId" });
            DropIndex("dbo.MetaData", new[] { "ArtifactId" });
            DropIndex("dbo.MetaData", new[] { "SpecificationId" });
            DropIndex("dbo.ArtifactVersions", new[] { "PreviousArtifactVersionId" });
            DropIndex("dbo.Artifacts", new[] { "ArtifactTypeId" });
            DropIndex("dbo.Artifacts", new[] { "ParentArtifactId" });
            DropIndex("dbo.Artifacts", new[] { "ArtifactVersionId" });
            DropTable("dbo.SpecificationVersions");
            DropTable("dbo.SpecificationTypes");
            DropTable("dbo.Configurations");
            DropTable("dbo.Specifications");
            DropTable("dbo.MetaDataDefinitionDetails");
            DropTable("dbo.MetaDataDefinitions");
            DropTable("dbo.MetaData");
            DropTable("dbo.ArtifactVersions");
            DropTable("dbo.ArtifactTypes");
            DropTable("dbo.Artifacts");
        }
    }
}
