﻿namespace CustomWise.Data.Entities {
    using Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Versioning;

    [Table(nameof(ArtifactVersion) + "s")]
    public class ArtifactVersion 
        : BaseItemDefinition, 
        IEntityVersion {
        [ForeignKey(nameof(Artifact))]
        public override int Id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        [Key, Column(Order = 2), MaxLength(256), Required(AllowEmptyStrings = false)]
        public string VersionNumber { get; set; }
        [Required, MaxLength(64)]
        public string Action { get; set; }
        public virtual Artifact Artifact { get; set; }

        public ArtifactVersion()
            : base() { }
    }
}
