using Sophcon;

namespace CustomWise.Web.Services.Models {
    public class MetaDataDefinitionDetail
        : BaseEntity {

        public int ID { get; set; }
        public string Name { get; set; }
        public int MetaDataDefinitionID { get; set; }

        public MetaDataDefinitionDetail()
            : base() { }
    }
}