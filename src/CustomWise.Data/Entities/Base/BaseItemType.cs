namespace CustomWise.Data.Entities.Base {
    using Sophcon;
    using System.ComponentModel.DataAnnotations;

    public class BaseItemType
        : BaseEntity {

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }

        public BaseItemType()
            : base() { }
    }
}
