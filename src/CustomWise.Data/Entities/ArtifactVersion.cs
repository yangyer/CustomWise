﻿namespace CustomWise.Data.Entities {
    using Base;
    using Sophcon.Data.Versioning;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(ArtifactVersion) + "s")]
    public class ArtifactVersion 
        : BaseItemDefinition,
        IVersionable {

        public override int Id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        [MaxLength(256), Required(AllowEmptyStrings = false)]
        public string VersionNumber { get; set; }
        [Required, MaxLength(64)]
        public string Action { get; set; }

        [ForeignKey(nameof(Artifact))]
        public int ArtifactId { get; set; }
        public virtual Artifact Artifact { get; set; }

        public ArtifactVersion()
            : base() { }
    }
}
