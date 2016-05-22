namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MetaData")]
    public class MetaData 
        : BaseMetaData {
        
        [ForeignKey(nameof(MetaDataDefinition))]
        public override int MetaDataDefinitionId { get; set; }
        public virtual MetaDataDefinition MetaDataDefinition { get; set; }
        public virtual ICollection<Specification> Specification { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Artifact> Artifact { get; set; } = new HashSet<Artifact>();

        public MetaData()
            : base() { }
    }
}
