
namespace CustomWise.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using Base;

    [Table("Configurations")]
    public class Configuration 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Specification))]
        public int SpecificationId { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        public Specification Specification { get; set; }

        public Configuration()
            : base() { }
    }
}
