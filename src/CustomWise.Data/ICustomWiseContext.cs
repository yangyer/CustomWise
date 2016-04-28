namespace CustomWise.Data {
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICustomWiseContext {
        DbSet<Artifact> Artifacts { get; set; }
        DbSet<ArtifactType> ArtifactTypes { get; set; }
        DbSet<Configuration> Configurations { get; set; }
        DbSet<MetaData> MetaData { get; set; }
        DbSet<MetaDataDefinition> MetaDataDefinitions { get; set; }
        DbSet<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; }
        DbSet<Specification> Specifications { get; set; }
        DbSet<SpecificationType> SpecificationTypes { get; set; }
        DbSet<SpecificationVersion> SpecificationVersions { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync<T>(Action<IEnumerable<T>, SaveToken> preSave, Action<IEnumerable<T>> postSave);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
