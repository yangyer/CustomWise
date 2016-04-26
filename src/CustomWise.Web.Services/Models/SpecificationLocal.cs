using CustomWise.Data.Entities.Base;

namespace CustomWise.Web.Services.Models {
    public class SpecificationLocal
        : BaseEntity {

        public int Id { get; set; }
        public string LocalCode { get; set; }
        public string DisplayName { get; set; }

        public SpecificationLocal()
            : base() { }
    }
}