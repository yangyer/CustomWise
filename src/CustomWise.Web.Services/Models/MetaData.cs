using CustomWise.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class MetaData
        : BaseEntity {

        public int Id { get; set; }
        public int SpecificationId { get; set; }
        public int MetaDataTypeId { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        public MetaData()
            : base() { }
    }
}