namespace CustomWise.Data.Entities.Base {
    using Sophcon;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseItemDefinition
        : BaseEntity {
        [Key, Column(Order = 1)]
        public virtual int Id { get; set; }
        public virtual int? ParentId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public int ItemTypeId { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public string ArtifactReferenceId { get; set; }
    }

    public class BaseItemDefinition<TEntity, TTypeDefinition>
        : BaseItemDefinition
        where TEntity : BaseEntity
        where TTypeDefinition : BaseEntity {
        
        public virtual TTypeDefinition ItemType { get; set; }
        [Display(Name = "Parent" + nameof(TEntity))]
        public virtual TEntity Parent { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        [Display(Name = "Sub" + nameof(TEntity))]
        public virtual ICollection<TEntity> SubItems { get; set; } = new HashSet<TEntity>();

        public BaseItemDefinition()
            : base() { }
    }
}
