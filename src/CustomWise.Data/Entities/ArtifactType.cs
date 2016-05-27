using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Data.Entities {
    public class ArtifactType : BaseEntity {

        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }

        public virtual ICollection<Artifact> Artifacts { get; set; } = new HashSet<Artifact>();

        public ArtifactType()
            : base() { }
    }
}
