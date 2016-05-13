using Sophcon;
using System.Collections;
using System.Collections.Generic;

namespace CustomWise.Data.Entities {
    public class VersionHeader 
        : BaseEntity {
        public int Id { get; set; }
        public bool Published { get; set; }
        public virtual ICollection<SpecificationVersion> SpecificationVersions { get; set; } = new HashSet<SpecificationVersion>();
        public virtual ICollection<ArtifactVersion> ArtifactVersions { get; set; } = new HashSet<ArtifactVersion>();
    }
}
