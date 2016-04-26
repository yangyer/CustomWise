namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract, Table("Specification")]
    public class Specification 
        : BaseEntity {
        [Key,]
        public int Id { get; set; }
        [ForeignKey("ParentSpecification")]
        public int? ParentSpecificationId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [ForeignKey("SpecificationType")]
        public int SpecificationTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        [ForeignKey("SpecificationVersion")]
        public int SpecificationVersionId { get; set; }
        public virtual SpecificationType SpecificationType { get; set; }
        public virtual Specification ParentSpecification { get; set; }
        public virtual SpecificationVersion SpecificationVersion { get; set; }
        public virtual ICollection<SpecificationMetaData> SpecificationMetaData { get; set; } = new HashSet<SpecificationMetaData>();
        public virtual ICollection<SpecificationLanguage> SpecificationLanguages { get; set; } = new HashSet<SpecificationLanguage>();
        public virtual ICollection<Specification> SubSpecifications { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public Specification()
            : base() { }
    }
}
