using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class SpecificationType : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = true), MaxLength(256)]
        public string Description { get; set; }
        public bool SystemType { get; set; }

        [ForeignKey(nameof(ParentType))]
        public int? ParentID { get; set; }
        public virtual SpecificationType ParentType { get; set; }
        
        public virtual ICollection<SpecificationTypeMetadataDefinition> MetadataDefinitions { get; set; }
        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();


        public SpecificationType() 
            : base() {
        }
    }
}
