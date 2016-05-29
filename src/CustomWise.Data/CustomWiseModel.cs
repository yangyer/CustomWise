namespace CustomWise.Data {
    using Entities;
    using Sophcon.Data.EntityFramework;
    using System.Data.Entity;
    
    public class CustomWiseModel 
        : DbContext,
        IDbContext,
        ICustomWiseContext {
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<ArtifactType> ArtifactTypes { get; set; }
        public virtual DbSet<ArtifactSystemType> ArtifactSystemTypes { get; set; }
        public virtual DbSet<ArtifactTypeMetadataDefinition> ArtifactTypeDefinitionMetadata { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<SpecificationMetadata> MetaData { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationType> SpecificationTypes { get; set; }
        public virtual DbSet<SpecificationSystemType> SpecificationSystemTypes { get; set; }
        public virtual DbSet<SpecificationTypeMetadataDefinition> SpecificationTypeDefinitionMetadata { get; set; }

        // Your context has been configured to use a 'CustomWiseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CustomWise.Data.CustomWiseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomWiseModel' 
        // connection string in the application configuration file.
        public CustomWiseModel()
            : base("name=CustomWiseModel") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtifactMetadata>()
                .HasRequired(e => e.MetadataDefinition)
                .WithMany(e => e.ArtifactMetadata)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificationMetadata>()
                .HasRequired(e => e.MetadataDefinition)
                .WithMany(e => e.SpecificationMetadata)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Specification>()
                .HasRequired(e => e.SpecificationSystemType)
                .WithMany(e => e.Specifications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Specification>()
               .HasOptional(e => e.SpecificationType)
               .WithMany(e => e.Specifications)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificationTypeMetadataDefinition>()
                .HasRequired(e => e.SpecificationType)
                .WithMany(e => e.MetadataDefinitions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificationMetadata>()
                .HasRequired(e => e.Specification)
                .WithMany(e => e.MetaData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configuration>()
                .HasRequired(e => e.Specification)
                .WithMany(e => e.Configurations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artifact>()
                .HasOptional(e => e.ArtifactType)
                .WithMany(e => e.Artifacts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artifact>()
                .HasRequired(e => e.ArtifactSystemType)
                .WithMany(e => e.Artifacts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArtifactTypeMetadataDefinition>()
                .HasRequired(e => e.ArtifactType)
                .WithMany(e => e.MetadataDefinitions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArtifactMetadata>()
                .HasRequired(e => e.Artifact)
                .WithMany(e => e.MetaData)
                .WillCascadeOnDelete(false);
        }
    }
}