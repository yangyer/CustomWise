namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MetaData")]
    public class MetaData 
        : BaseMetaData {
        
        [ForeignKey(nameof(Specification))]
        public override int? SpecificationId { get; set; }
        [ForeignKey(nameof(Artifact))]
        public override int? ArtifactId { get; set; }
        [ForeignKey(nameof(MetaDataDefinition))]
        public override int MetaDataDefinitionId { get; set; }
        public virtual MetaDataDefinition MetaDataDefinition { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual Artifact Artifact { get; set; }

        public MetaData()
            : base() { }
    }
}
