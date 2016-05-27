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
        [Required, MaxLength(64)]
        public string SystemName { get; set; }

        [ForeignKey(nameof(SpecificationTypeDefinition))]
        public int SpecificationTypeDefinitionID { get; set; }
        public virtual SpecificationTypeDefinition SpecificationTypeDefinition { get; set; }

        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public SpecificationType() 
            : base() {
        }
    }
}
