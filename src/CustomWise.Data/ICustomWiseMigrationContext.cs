namespace CustomWise.Data {
    using System.Data.Entity;
    using Entities;

    /// <summary>
    /// Defines the CutomWise migration context
    /// </summary>
    public interface ICustomWiseMigrationContext {
        DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        DbSet<Artifact> Artifacts { get; set; }
        DbSet<ArtifactType> ArtifactTypes { get; set; }
        DbSet<Configuration> Configurations { get; set; }
        DbSet<MetaData> MetaData { get; set; }
        DbSet<MetaDataVersion> MetaDataVersions { get; set; }
        DbSet<MetaDataDefinition> MetaDataDefinitions { get; set; }
        DbSet<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<SpecificationType> SpecificationTypes { get; set; }
        DbSet<SpecificationVersion> SpecificationVersions { get; set; }

        int SaveChanges();
    }
}