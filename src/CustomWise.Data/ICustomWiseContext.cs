namespace CustomWise.Data {
    using Entities;
    using Sophcon.Data;
    using System.Data.Entity;

    public interface ICustomWiseContext 
        : ISophconContext {

        DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        DbSet<Artifact> Artifacts { get; set; }
        DbSet<ArtifactType> ArtifactTypes { get; set; }
        DbSet<Configuration> Configurations { get; set; }
        DbSet<MetaData> MetaData { get; set; }
        DbSet<MetaDataDefinition> MetaDataDefinitions { get; set; }
        DbSet<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<SpecificationType> SpecificationTypes { get; set; }
        DbSet<SpecificationVersion> SpecificationVersions { get; set; }
    }
}
