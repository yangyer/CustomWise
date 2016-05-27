using Sophcon;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class Configuration
        : BaseEntity {

        public int ID { get; set; }
        public int SpecificationID { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        public Configuration()
            : base() { }
    }
}