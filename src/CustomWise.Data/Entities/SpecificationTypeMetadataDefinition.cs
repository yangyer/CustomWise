using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class SpecificationTypeMetadataDefinition : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(256)]
        public string Key { get; set; }
        [Required, MaxLength(960)]
        public string ValueType { get; set; }
        [MaxLength(960)]
        public string DefaultValue { get; set; }
        public bool AllowOverride { get; set; }
        public bool AllowReferenceOverride { get; set; }
        public string InputTemplate { get; set; }
        public bool Required { get; set; }
        public bool ReferenceRequired { get; set; }
        public bool Localized { get; set; }

        [ForeignKey(nameof(SpecificationType))]
        public int SpecificationTypeID { get; set; }
        public SpecificationType SpecificationType { get; set; }

        public ICollection<SpecificationMetadata> SpecificationMetadata { get; set; }

        public SpecificationTypeMetadataDefinition() 
            : base() {

        }
    }
}