using Sophcon;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Models {
    public class Artifact : BaseEntity {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public int ItemTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public int ReferenceID { get; set; }
        public virtual ArtifactType ItemType { get; set; }
        public virtual ICollection<SpecificationMetadata> MetaData { get; set; } = new HashSet<SpecificationMetadata>();

        public Artifact() : base() { }
    }

    public class ArtifactType : BaseEntity {
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }

        public ArtifactType() : base() { }
    }

    public class ArtifactSpecification {
        public DalEntities.Specification Specification { get; set; }
        public DalEntities.Artifact Artifact { get; set; }
    }
}