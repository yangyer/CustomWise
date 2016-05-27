using Sophcon;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class SpecificationTypeDefinitionMetadata : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(64)]
        public string ValueType { get; set; }
        [Required, MaxLength(960)]
        public string DefaultValue { get; set; }
        public bool AllowOverride { get; set; }
        public bool Required { get; set; }
        public bool Localized { get; set; }

        [ForeignKey(nameof(SpecificationTypeDefinition))]
        public int SpecificationTypeDefinitionID { get; set; }
        public SpecificationTypeDefinition SpecificationTypeDefinition { get; set; }

        public SpecificationTypeDefinitionMetadata() 
            : base() {

        }
    }
}