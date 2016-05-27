using Sophcon;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class ArtifactTypeMetadataDefinition : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(64)]
        public string ValueType { get; set; }
        [Required, MaxLength(960)]
        public string DefaultValue { get; set; }
        public bool AllowOverride { get; set; }
        public bool AllowReferenceOverride { get; set; }
        public bool Required { get; set; }
        public bool ReferenceRequired { get; set; }
        public bool Localized { get; set; }

        [ForeignKey(nameof(ArtifactType))]
        public int ArtifactTypeID { get; set; }
        public ArtifactType ArtifactType { get; set; }

        public ArtifactTypeMetadataDefinition()
            : base() {

        }
    }
}