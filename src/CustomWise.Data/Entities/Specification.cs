namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Specification 
        : BaseEntity {
        [Key,]
        public int Id { get; set; }
        [ForeignKey(nameof(ParentSpecification))]
        public int? ParentSpecificationId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [ForeignKey(nameof(SpecificationType))]
        public int SpecificationTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public string ArtifactReferenceId { get; set; }
        [ForeignKey(nameof(SpecificationVersion))]
        public int SpecificationVersionId { get; set; }
        public virtual SpecificationType SpecificationType { get; set; }
        public virtual Specification ParentSpecification { get; set; }
        public virtual SpecificationVersion SpecificationVersion { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Specification> SubSpecifications { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public Specification()
            : base() { }
    }
}
