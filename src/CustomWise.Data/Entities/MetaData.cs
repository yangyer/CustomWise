using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {

    [Table("MetaData")]
    public class MetaData : BaseEntity {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        [ForeignKey(nameof(MetaDataDefinition))]
        public int MetaDataDefinitionId { get; set; }
        public virtual MetaDataDefinition MetaDataDefinition { get; set; }

        public virtual ICollection<Specification> Specification { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Artifact> Artifact { get; set; } = new HashSet<Artifact>();

        public MetaData()
            : base() { }
    }
}
