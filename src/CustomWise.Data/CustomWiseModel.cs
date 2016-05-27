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
    }
}