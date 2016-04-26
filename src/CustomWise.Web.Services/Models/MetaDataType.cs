using CustomWise.Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class MetaDataType
        : BaseEntity {

        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public IEnumerable<MetaData> MetaData { get; set; } = new List<MetaData>();

        public MetaDataType()
            : base() { }
    }
}