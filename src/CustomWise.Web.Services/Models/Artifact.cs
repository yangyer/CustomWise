using DalEntities = CustomWise.Data.Entities;

namespace CustomWise.Web.Services.Models {
    public class ArtifactSpecification {
        public DalEntities.Specification Specification { get; set; }
        public DalEntities.Artifact Artifact { get; set; }
    }
}