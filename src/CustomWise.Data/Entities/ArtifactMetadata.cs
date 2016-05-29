using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    [Table(nameof(ArtifactMetadata))]
    public class ArtifactMetadata : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        
        [ForeignKey(nameof(Artifact))]
        public int ArtifactID { get; set; }
        public virtual Artifact Artifact { get; set; }

        [ForeignKey(nameof(MetadataDefinition))]
        public int MetadataDefinitionId { get; set; }
        public virtual ArtifactTypeMetadataDefinition MetadataDefinition { get; set; }

        public ArtifactMetadata()
            : base() { }
    }
}
