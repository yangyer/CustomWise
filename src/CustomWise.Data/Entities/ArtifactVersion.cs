namespace CustomWise.Data.Entities {
    using Sophcon;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ArtifactVersion 
        : BaseEntity {

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(PreviousArtifactVersion))]
        public int? PreviousArtifactVersionId { get; set; }
        [Required, StringLength(64)]
        public string Name { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual ArtifactVersion PreviousArtifactVersion { get; set; }
        public virtual ICollection<Artifact> Artifacts { get; set; } = new HashSet<Artifact>();

        public ArtifactVersion()
            : base() { }
    }
}
