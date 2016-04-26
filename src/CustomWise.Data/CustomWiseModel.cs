namespace CustomWise.Data {
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CustomWiseModel : DbContext {
        // Your context has been configured to use a 'CustomWiseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CustomWise.Data.CustomWiseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomWiseModel' 
        // connection string in the application configuration file.
        public CustomWiseModel()
            : base("name=CustomWiseModel") {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<SpecificationMetaData> SpecificationMetaData { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationLanguage> SpecificationLanguages { get; set; }
        public virtual DbSet<SpecificationType> SpecificationTypes { get; set; }
        public virtual DbSet<SpecificationTypeLanguage> SpecificationTypeLanguages { get; set; }
        public virtual DbSet<SpecificationVersion> SpecificationVersions { get; set; }
    }
}