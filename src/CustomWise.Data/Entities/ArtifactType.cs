namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ArtifactType 
        : BaseItemType {

        public virtual ICollection<Artifact> Artifacts { get; set; } = new HashSet<Artifact>();

        public ArtifactType()
            : base() { }
    }
}
