﻿namespace CustomWise.Data.Entities {
    using Base;
    using Versioning;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    [Table(nameof(MetaDataVersion) + "s")]
    public class MetaDataVersion
        : BaseMetaData,
        IEntityVersion {

        [Key, Column(Order = 1), ForeignKey(nameof(MetaData))]
        public override int Id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        [Key, Column(Order = 2), MaxLength(256), Required(AllowEmptyStrings = false)]
        public string VersionNumber { get; set; }
        [Required, MaxLength(64)]
        public string Action { get; set; }
        public virtual MetaData MetaData { get; set; }

        public MetaDataVersion() 
            : base() { }
    }
}