namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MetaData")]
    public class MetaData 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Specification")]
        public int SpecificationId { get; set; }
        [ForeignKey("MetaDataType")]
        public int MetaDataTypeId { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        public MetaDataType MetaDataType { get; set; }
        public virtual Specification Specification { get; set; }

        public MetaData()
            : base() { }
    }
}
