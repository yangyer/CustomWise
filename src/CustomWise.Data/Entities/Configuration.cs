using Sophcon;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomWise.Data.Entities {

    public class Configuration 
        : BaseEntity {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Specification))]
        public int SpecificationId { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        public Specification Specification { get; set; }

        public Configuration()
            : base() { }
    }
}
