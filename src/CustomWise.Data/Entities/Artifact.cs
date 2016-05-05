namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Artifact 
        : BaseItemDefinition<Artifact, ArtifactType> {

        [ForeignKey(nameof(Parent))]
        public override int? ParentId {
            get { return base.ParentId; }
            set { base.ParentId = value; }
        }

        public Artifact()
            : base() { }
    }
}
