using CustomWise.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class Specification
        : BaseEntity {
        public int Id { get; set; }
        public int? ParentSpecificationId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public int RecordTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public int ReferenceId { get; set; }
        public int SpecificationVersionId { get; set; }
        public virtual SpecificationType RecordType { get; set; }
        public virtual SpecificationVersion SpecificationVersion { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Specification> SubSpecifications { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public Specification() 
            : base() { }
    }
}
