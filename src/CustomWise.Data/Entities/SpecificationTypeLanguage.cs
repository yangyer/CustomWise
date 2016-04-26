namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SpecificationTypeLanguage")]
    public class SpecificationTypeLanguage
        : BaseEntity {
        [Key, Column(Order = 1)]
        [ForeignKey("SpecificationType")]
        public int Id { get; set; }
        [Key, Column(Order = 2)]
        public string LanguageCode { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public virtual SpecificationType SpecificationType { get; set; }

        public SpecificationTypeLanguage()
            : base() { }
    }
}
