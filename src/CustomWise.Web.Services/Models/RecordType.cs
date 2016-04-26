﻿using CustomWise.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CustomWise.Web.Services.Models {
    public class RecordType
        : BaseEntity {

        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string DisplayName { get; set; }
        [Required, MaxLength(64)]
        public string SystemName { get; set; }

        public RecordType()
            : base() { }
    }
}