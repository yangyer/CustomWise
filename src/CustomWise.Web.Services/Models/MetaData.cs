using Sophcon;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class MetaData
        : BaseEntity {

        public int ID { get; set; }
        public int? SpecificationID { get; set; }
        public int? ArtifactID { get; set; }
        public int MetaDataDefinitionID { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        public MetaData()
            : base() { }
    }
}