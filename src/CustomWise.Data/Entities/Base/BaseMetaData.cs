
namespace CustomWise.Data.Entities.Base {
    using Sophcon;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseMetaData
        : BaseEntity {
        [Key, Column(Order = 1)]
        public virtual int Id { get; set; }
        public virtual int? SpecificationId { get; set; }
        public virtual int? ArtifactId { get; set; }
        public virtual int MetaDataDefinitionId { get; set; }
        [Required, MaxLength(64)]
        public string Key { get; set; }
        [Required, MaxLength(256)]
        public string Value { get; set; }
        
        public BaseMetaData()
            : base() { }
    }
}
