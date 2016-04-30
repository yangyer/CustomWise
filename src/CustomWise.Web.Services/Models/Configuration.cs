using Sophcon;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class Configuration
        : BaseEntity {

        public int Id { get; set; }
        public int SpecificationId { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }

        public Configuration()
            : base() { }
    }
}