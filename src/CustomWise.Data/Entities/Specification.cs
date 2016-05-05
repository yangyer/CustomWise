namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Specification 
        : BaseItemDefinition<Specification, SpecificationType> {

        [ForeignKey(nameof(Parent))]
        public override int? ParentId {
            get { return base.ParentId; }
            set { base.ParentId = value; }
        }
        public virtual ICollection<Configuration> Configurations { get; set; } = new HashSet<Configuration>();

        public Specification()
            : base() { }
    }
}
