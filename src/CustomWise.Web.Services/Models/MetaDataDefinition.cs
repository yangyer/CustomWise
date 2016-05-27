using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class MetaDataDefinition
        : BaseEntity {

        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public IEnumerable<SpecificationMetadata> MetaData { get; set; } = new List<SpecificationMetadata>();
        public IEnumerable<MetaDataDefinitionDetail> MetaDataDefinitionDetails { get; set; } = new List<MetaDataDefinitionDetail>();

        public MetaDataDefinition()
            : base() { }
    }
}