﻿
namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public class SpecificationType 
        : BaseEntity {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }
        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public SpecificationType() 
            : base() { }
    }
}