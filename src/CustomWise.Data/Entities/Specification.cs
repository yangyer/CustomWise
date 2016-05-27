using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {

    public class Specification 
        : BaseEntity {

        [Key]
        public int ID { get; set; }
        [MaxLength(256)]
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public string ArtifactReferenceID { get; set; }
        
        [ForeignKey(nameof(SpecificationSystemType))]
        public int SpecificationSystemTypeId { get; set; }
        public SpecificationSystemType SpecificationSystemType { get; set; }

        [ForeignKey(nameof(SpecificationType))]
        public int SpecificationTypeID { get; set; }
        public virtual SpecificationType SpecificationType { get; set; }

        [ForeignKey(nameof(Parent))]
        public virtual int? ParentID { get; set; }
        public virtual Specification Parent { get; set; }

        public virtual ICollection<SpecificationMetadata> MetaData { get; set; } = new HashSet<SpecificationMetadata>();
        public virtual ICollection<Specification> SubItems { get; set; } = new HashSet<Specification>();

        public Specification()
            : base() {
        }
    }
}
