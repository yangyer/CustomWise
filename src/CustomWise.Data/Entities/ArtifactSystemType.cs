﻿using Sophcon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWise.Data.Entities {
    public class ArtifactSystemType : BaseEntity {

        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }

        public ICollection<Artifact> Artifacts { get; set; }

        public ArtifactSystemType() 
            : base() {

        }
    }
}
