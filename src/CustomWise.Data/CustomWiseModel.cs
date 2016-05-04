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

    public class CustomWiseModel 
        : DbContext,
        ICustomWiseContext {
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<ArtifactType> ArtifactTypes { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<MetaDataDefinition> MetaDataDefinitions { get; set; }
        public virtual DbSet<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationType> SpecificationTypes { get; set; }
        public virtual DbSet<SpecificationVersion> SpecificationVersions { get; set; }

        private List<Action<IEnumerable<BaseEntity>, IPreSavePipeline>> _preSaveEvents;
        private List<Action<IEnumerable<BaseEntity>, IPostSavePipeline>> _postSaveEvents;

        // Your context has been configured to use a 'CustomWiseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CustomWise.Data.CustomWiseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomWiseModel' 
        // connection string in the application configuration file.
        public CustomWiseModel()
            : base("name=CustomWiseModel") {
            _preSaveEvents = new List<Action<IEnumerable<BaseEntity>, IPreSavePipeline>>();
            _postSaveEvents = new List<Action<IEnumerable<BaseEntity>, IPostSavePipeline>>();
        }
        
        /// <summary>
        /// Registers the pre save.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="preSave">The pre save.</param>
        public void RegisterPreSave(params Action<IEnumerable<BaseEntity>, IPreSavePipeline>[] preSave) {
            // only add if is not already in local list
            _preSaveEvents.AddRange(preSave.Where(s => !_preSaveEvents.Contains(s)));
        }

        /// <summary>
        /// Registers the post save.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="postSave">The pre save.</param>
        public void RegisterPostSave(params Action<IEnumerable<BaseEntity>, IPostSavePipeline>[] postSave) {
            // only add if is not already in local list
            _postSaveEvents.AddRange(postSave.Where(s => !_postSaveEvents.Contains(s)));
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="preSave">Action to perform before save is called.</param>
        /// <param name="postSave">Action to perform after successfully call save.</param>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public async Task<int> SaveChangesAsync(Action<IEnumerable<BaseEntity>, IPreSavePipeline> preSave, Action<IEnumerable<BaseEntity>, IPostSavePipeline> postSave) {
            var cancellationTokenSource = new CancellationTokenSource();
            return await InternalSaveAsync(cancellationTokenSource.Token, preSave, postSave);
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public override int SaveChanges() {
            return InternalSave();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public override async Task<int> SaveChangesAsync() {
            return await InternalSaveAsync(CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Internals the save asynchronous.
        /// </summary>
        /// <returns></returns>
        internal async Task<int> InternalSaveAsync(CancellationToken cancellationToken, Action<IEnumerable<BaseEntity>, IPreSavePipeline> customPreSave = null, Action<IEnumerable<BaseEntity>, IPostSavePipeline> customPostSave = null) {
            var pipeLineState = PipeLineState.Empty();
            var pre = new List<Action<IEnumerable<BaseEntity>, IPreSavePipeline>>(_preSaveEvents);
            var post = new List<Action<IEnumerable<BaseEntity>, IPostSavePipeline>>(_postSaveEvents);
            var entities = GetDirtyEntities();

            if (customPreSave != null) {
                pre.Add(customPreSave);
            }
            if(customPostSave != null) {
                post.Add(customPostSave);
            }

            foreach (var preSave in pre) {
                // pass true to CancellationToken to cancel process; else just return the passed in cancellation token
                cancellationToken = pipeLineState.ExitSave ? new CancellationToken(pipeLineState.ExitSave) : cancellationToken;
                if (pipeLineState.ExitPreSave) {
                    break;
                }
                preSave(entities, pipeLineState);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach(var postSave in post){
                if(pipeLineState.ExitPostSave) {
                    break;
                }
                postSave(entities, pipeLineState);
            }
            
            return result;
        }

        /// <summary>
        /// Internals the save.
        /// </summary>
        /// <returns></returns>
        internal int InternalSave(Action<IEnumerable<BaseEntity>, IPreSavePipeline> customPreSave = null, Action<IEnumerable<BaseEntity>, IPostSavePipeline> customPostSave = null) {
            var pipeLineState = PipeLineState.Empty();
            var pre = new List<Action<IEnumerable<BaseEntity>, IPreSavePipeline>>(_preSaveEvents);
            var post = new List<Action<IEnumerable<BaseEntity>, IPostSavePipeline>>(_postSaveEvents);
            var entities = GetDirtyEntities();

            if (customPreSave != null) {
                pre.Add(customPreSave);
            }
            if (customPostSave != null) {
                post.Add(customPostSave);
            }

            foreach (var preSave in pre) {
                if (pipeLineState.ExitPreSave) {
                    break;
                }
                preSave(entities, pipeLineState);
            }

            var result = 0;

            try {
                result = pipeLineState.ExitSave ? 0 : base.SaveChanges();
            } catch (Exception ex) {
                throw ex;
            }

            foreach (var postSave in post) {
                if (pipeLineState.ExitPostSave) {
                    break;
                }
                postSave(entities, pipeLineState);
            }

            return result;
        }

        /// <summary>
        /// Gets the dirty entities.
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<BaseEntity> GetDirtyEntities() {
            var stateManager = new Sophcon.StateManager();
            return ChangeTracker.Entries()
                .Where(e => !e.State.In(EntityState.Unchanged, EntityState.Detached))
                .Select(e => {
                    var bEntity = e.Entity as BaseEntity;
                    stateManager.SetState(bEntity, (SophconEntityState)((int)e.State));
                    return bEntity;
                });
        }
    }
}