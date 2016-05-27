using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {
    
    [Table("MetaDataDefinitions")]
    public class MetaDataDefinition 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public bool SystemType { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentDefinition))]
        public MetaDataDefinition ParentDefinition { get; set; }

        public virtual ICollection<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; } = new HashSet<MetaDataDefinitionDetail>();
        
        public MetaDataDefinition() 
            : base() { }
    }
}
