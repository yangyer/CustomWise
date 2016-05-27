using Sophcon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class SpecificationVersion
        : BaseEntity {

        public int ID { get; set; }
        public string VersionNumber { get; set; }
        public int? ParentID { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        public int ItemTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int Order { get; set; }
        public int ReferenceID { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual SpecificationType ItemType { get; set; }
        public virtual ICollection<MetaData> MetaData { get; set; } = new HashSet<MetaData>();
        public virtual ICollection<Specification> Subitems { get; set; } = new HashSet<Specification>();
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public SpecificationVersion()
            : base() { }
    }
}