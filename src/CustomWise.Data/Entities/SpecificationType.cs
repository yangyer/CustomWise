namespace CustomWise.Data.Entities {
    using Base;
    using System.Collections.Generic;

    public class SpecificationType 
        : BaseItemType {
      
        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public SpecificationType() 
            : base() { }
    }
}
