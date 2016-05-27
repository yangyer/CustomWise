using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {

    public class Artifact : BaseEntity {
        [Key]
        public virtual int ID { get; set; }
        [MaxLength(256)]
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public string ArtifactReferenceId { get; set; }

        [ForeignKey(nameof(ArtifactType))]
        public int ArtifactSystemTypeID { get; set; }
        public ArtifactSystemType ArtifactSystemType { get; set; }

        [ForeignKey(nameof(ArtifactType))]
        public int ArtifactTypeID { get; set; }
        public virtual ArtifactType ArtifactType { get; set; }

        [ForeignKey(nameof(Parent))]
        public virtual int? ParentID { get; set; }
        public virtual Artifact Parent { get; set; }

        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Artifact> SubItems { get; set; } = new HashSet<Artifact>();

        public Artifact()
            : base() { }
    }
}
