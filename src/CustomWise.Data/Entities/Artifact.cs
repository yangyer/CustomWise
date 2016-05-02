namespace CustomWise.Data.Entities {
    using Sophcon;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Artifact 
        : BaseEntity {
        [Key,]
        public int Id { get; set; }
        [ForeignKey(nameof(ArtifactVersion))]
        public int ArtifactVersionId { get; set; }
        [ForeignKey(nameof(ParentArtifact))]
        public int? ParentArtifactId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [ForeignKey(nameof(ArtifactType))]
        public int ArtifactTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public string ArtifactReferenceId { get; set; }
        public virtual ArtifactVersion ArtifactVersion { get; set; }
        public virtual ArtifactType ArtifactType { get; set; }
        public virtual Artifact ParentArtifact { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Artifact> SubArtifacts { get; set; } = new HashSet<Artifact>();

        public Artifact()
            : base() { }
    }
}
