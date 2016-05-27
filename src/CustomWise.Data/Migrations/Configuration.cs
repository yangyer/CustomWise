namespace CustomWise.Data.Migrations {
    using Entities;
    using Sophcon.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomWise.Data.CustomWiseModel> {

        protected override void Seed(CustomWise.Data.CustomWiseModel context) {
            context.SpecificationSystemTypes.AddOrUpdate(CreateSpecificationSystemTypes());
            context.ArtifactSystemTypes.AddOrUpdate(CreateArtifactSystemTypes());
            context.SaveChanges();
        }
        
        #region Seed Methods

        private SpecificationSystemType[] CreateSpecificationSystemTypes() {
            var entities = new SpecificationSystemType[] {
                new SpecificationSystemType { Name = "Root" },
                new SpecificationSystemType { Name = "Group" },
                new SpecificationSystemType { Name = "Choice Group" },
                new SpecificationSystemType { Name = "Item" },
                new SpecificationSystemType { Name = "Reference Item", }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        private ArtifactSystemType[] CreateArtifactSystemTypes() {
            var entities = new ArtifactSystemType[] {
                new ArtifactSystemType { Name = "Root" },
                new ArtifactSystemType { Name = "Group" },
                new ArtifactSystemType { Name = "Choice Group" },
                new ArtifactSystemType { Name = "Item" },
                new ArtifactSystemType { Name = "Reference Item" }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        #endregion
    }
}
