namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SpecificationLanguage")]
    public class SpecificationLanguage
        : BaseEntity {
        [Key, Column(Order = 1)]
        [ForeignKey("Specification")]
        public int Id { get; set; }
        [Key, Column(Order = 2)]
        public string LanguageCode { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public virtual Specification Specification { get; set; }

        public SpecificationLanguage()
            : base() { }
    }
}
