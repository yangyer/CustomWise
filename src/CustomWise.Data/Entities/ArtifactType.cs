namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ArtifactType 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }
        public virtual ICollection<Artifact> Artifacts { get; set; } = new HashSet<Artifact>();

        public ArtifactType()
            : base() { }
    }
}
