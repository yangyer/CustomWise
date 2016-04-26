namespace CustomWise.Data {
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading;
    using Entities.Base;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
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

        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<MetaDataType> MetaDataTypes { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationLocal> SpecificationLocals { get; set; }
        public virtual DbSet<RecordType> RecordTypes { get; set; }
        public virtual DbSet<SpecificationVersion> SpecificationVersions { get; set; }

        public override int SaveChanges() {
            ProccessEntities(this.ChangeTracker.Entries());
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync() {
            ProccessEntities(this.ChangeTracker.Entries());
            return await base.SaveChangesAsync();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            ProccessEntities(this.ChangeTracker.Entries());
            return await base.SaveChangesAsync(cancellationToken);
        }

        internal void ProccessEntities(IEnumerable<DbEntityEntry> entries) {
            var result = Parallel.ForEach(entries, e => {
                var entity = (e.Entity as BaseEntity);

                switch (e.State) {
                    case EntityState.Added:
                        entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
                        entity.CreatedBy = entity.CreatedBy ?? "system";
                        entity.ModifiedBy = entity.ModifiedBy ?? "system";
                        break;
                    case EntityState.Deleted:
                        // do something here
                        break;
                    case EntityState.Modified:
                        entity.ModifiedDate = DateTime.Now;
                        entity.ModifiedBy = entity.ModifiedBy ?? "system";
                        break;
                    case EntityState.Unchanged:
                    case EntityState.Detached:
                    default:
                        break;
                }
            });
        }
    }
}