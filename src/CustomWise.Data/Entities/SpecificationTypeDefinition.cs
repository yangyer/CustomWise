using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class SpecificationTypeDefinition : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public bool SystemType { get; set; }

        [ForeignKey(nameof(ParentDefinition))]
        public int ParentID { get; set; }
        public virtual SpecificationTypeDefinition ParentDefinition { get; set; }

        public virtual ICollection<SpecificationTypeDefinitionMetadata> SpecificationTypeDefinitionMetadata { get; set; }

        public SpecificationTypeDefinition() 
            : base() {
        }
    }
}
