namespace CustomWise.Data {
    using Entities;
    using Sophcon.Data.EntityFramework;
    using System;
    using System.Data.Entity;

    /// <summary>
    /// Defines the CustomWise data context.
    /// </summary>
    /// <seealso cref="Sophcon.Data.EntityFramework.IDbContext" />
    /// <seealso cref="System.IDisposable" />
    public interface ICustomWiseContext 
        : IDisposable {
        //DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        DbSet<Artifact> Artifacts { get; set; }
        DbSet<ArtifactType> ArtifactTypes { get; set; }
        DbSet<ArtifactSystemType> ArtifactSystemTypes { get; set; }
        DbSet<ArtifactTypeMetadataDefinition> ArtifactTypeDefinitionMetadata { get; set; }
        DbSet<Configuration> Configurations { get; set; }
        DbSet<SpecificationMetadata> MetaData { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<SpecificationType> SpecificationTypes { get; set; }
        DbSet<SpecificationSystemType> SpecificationSystemTypes { get; set; }
        DbSet<SpecificationTypeMetadataDefinition> SpecificationTypeDefinitionMetadata { get; set; }


        int SaveChanges();
    }
}
