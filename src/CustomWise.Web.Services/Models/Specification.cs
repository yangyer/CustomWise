using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class Specification
        : BaseEntity {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public int ItemTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public int ReferenceID { get; set; }
        public virtual SpecificationType ItemType { get; set; }
        public virtual ICollection<SpecificationMetadata> MetaData { get; set; } = new HashSet<SpecificationMetadata>();
        public virtual ICollection<Specification> SubItems { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public Specification() 
            : base() { }
    }
}
