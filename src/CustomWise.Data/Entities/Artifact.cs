namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Artifacts")]
    public class Artifact 
        : BaseEntity {
        [Key,]
        public int Id { get; set; }
        [ForeignKey("ParentArtifact")]
        public int? ParentArtifactId { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [ForeignKey("RecordType")]
        public int RecordTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public int ReferenceId { get; set; }
        public virtual RecordType RecordType { get; set; }
        public virtual Artifact ParentArtifact { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Artifact> SubArtifacts { get; set; } = new HashSet<Artifact>();

        public Artifact()
            : base() { }
    }
}
