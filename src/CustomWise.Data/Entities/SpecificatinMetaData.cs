namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SpecificationMetaData")]
    public class SpecificationMetaData 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Specification")]
        public int SpecificationId { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        public virtual Specification Specification { get; set; }

        public SpecificationMetaData()
            : base() { }
    }
}
