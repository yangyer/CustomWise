namespace CustomWise.Data.Entities {
    using Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SpecificationVersion 
        : BaseEntity {

        [Key]
        public int Id { get; set; }
        [Required, StringLength(64)]
        public string Name { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedDate { get; set; }
        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public SpecificationVersion()
            : base() { }
    }
}
