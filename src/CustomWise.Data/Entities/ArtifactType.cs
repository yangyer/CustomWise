using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    public class ArtifactType : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = true), MaxLength(256)]
        public string Description { get; set; }
        public bool SystemType { get; set; }

        [ForeignKey(nameof(ParentType))]
        public int? ParentID { get; set; }
        public virtual ArtifactType ParentType{ get; set; }

        public virtual ICollection<ArtifactTypeMetadataDefinition> MetadataDefinitions { get; set; }
        public virtual ICollection<Artifact> Artifacts { get; set; }

        public ArtifactType()
            : base() { }
    }
}
