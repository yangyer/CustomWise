namespace CustomWise.Data {
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using Sophcon.Data;
    using Sophcon;
    using Sophcon.Collections;
    using Sophcon.Data.EntityFramework;
    public class CustomWiseModel 
        : DbContext,
        IDbContext,
        ICustomWiseContext {
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<ArtifactType> ArtifactTypes { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<MetaDataVersion> MetaDataVersions { get; set; }
        public virtual DbSet<MetaDataDefinition> MetaDataDefinitions { get; set; }
        public virtual DbSet<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationType> SpecificationTypes { get; set; }
        public virtual DbSet<SpecificationVersion> SpecificationVersions { get; set; }
        public virtual DbSet<VersionHeader> VersionHeaders { get; set; }

        // Your context has been configured to use a 'CustomWiseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CustomWise.Data.CustomWiseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomWiseModel' 
        // connection string in the application configuration file.
        public CustomWiseModel()
            : base("name=CustomWiseModel") {
        }

        public override async Task<int> SaveChangesAsync() {
            return await base.SaveChangesAsync();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}