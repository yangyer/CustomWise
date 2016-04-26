using CustomWise.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class SpecificationVersion
        : BaseEntity {

        public int Id { get; set; }
        [Required, StringLength(64)]
        public string Name { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedDate { get; set; }

        public SpecificationVersion()
            : base() { }
    }
}