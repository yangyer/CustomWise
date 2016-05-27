using Sophcon;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class SpecificationMetadata
        : BaseEntity {

        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        public int MetaDataDefinitionId { get; set; }

        public SpecificationMetadata()
            : base() { }
    }
}