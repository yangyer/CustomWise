using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {

    [Table(nameof(SpecificationMetadata))]
    public class SpecificationMetadata : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        [ForeignKey(nameof(Specification))]
        public int SpecificationID { get; set; }
        public virtual Specification Specification { get; set; }

        [ForeignKey(nameof(MetadataDefinition))]
        public int MetadataDefinitionID { get; set; }
        public virtual SpecificationTypeMetadataDefinition MetadataDefinition { get; set; }

        public SpecificationMetadata()
            : base() { }
    }
}
