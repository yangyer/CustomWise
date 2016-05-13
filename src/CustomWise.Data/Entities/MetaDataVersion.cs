namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Sophcon.Data.Versioning;

    [Table(nameof(MetaDataVersion) + "s")]
    public class MetaDataVersion
        : BaseMetaData,
        IVersionable {

        public override int Id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        [ForeignKey(nameof(MetaData))]
        public int MetaDataId { get; set; }
        [MaxLength(256), Required(AllowEmptyStrings = false)]
        public string VersionNumber { get; set; }
        [ForeignKey(nameof(VersionHeader))]
        public int VersionHeaderId { get; set; }
        [Required, MaxLength(64)]
        public string Action { get; set; }
        public virtual MetaData MetaData { get; set; }
        public virtual VersionHeader VersionHeader { get; set; }

        public MetaDataVersion() 
            : base() { }
    }
}
