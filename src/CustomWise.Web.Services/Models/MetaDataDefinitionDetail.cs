using CustomWise.Data.Entities.Base;

namespace CustomWise.Web.Services.Models {
    public class MetaDataDefinitionDetail
        : BaseEntity {

        public int Id { get; set; }
        public string Name { get; set; }
        public int MetaDataDefinitionId { get; set; }

        public MetaDataDefinitionDetail()
            : base() { }
    }
}