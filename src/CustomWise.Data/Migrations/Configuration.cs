namespace CustomWise.Data.Migrations {
    using Entities;
    using Sophcon.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomWise.Data.CustomWiseModel> {
        public Configuration() {
            base.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CustomWise.Data.CustomWiseModel context) {
            context.SpecificationSystemTypes.AddOrUpdate(CreateSpecificationSystemTypes());
            context.ArtifactSystemTypes.AddOrUpdate(CreateArtifactSystemTypes());
            context.SaveChanges();
        }
        
        #region Seed Methods

        private SpecificationSystemType[] CreateSpecificationSystemTypes() {
            var entities = new SpecificationSystemType[] {
                new SpecificationSystemType { ID = 1, Name = "Root" },
                new SpecificationSystemType { ID = 2, Name = "Group" },
                new SpecificationSystemType { ID = 3, Name = "Choice Group" },
                new SpecificationSystemType { ID = 4, Name = "Item" },
                new SpecificationSystemType { ID = 5, Name = "Multiple Choice Group" },
                new SpecificationSystemType { ID = 6, Name = "Reference Group" },
                new SpecificationSystemType { ID = 7, Name = "Reference Choice Group" },
                new SpecificationSystemType { ID = 8, Name = "Reference Multiple Choice Group" },
                new SpecificationSystemType { ID = 9, Name = "Reference Item" }
            };

            return entities.SetCreatedByModifiedByList();
        }

        private ArtifactSystemType[] CreateArtifactSystemTypes() {
            var entities = new ArtifactSystemType[] {
                new ArtifactSystemType { ID = 1, Name = "Root" },
                new ArtifactSystemType { ID = 2, Name = "Group" },
                new ArtifactSystemType { ID = 3, Name = "Choice Group" },
                new ArtifactSystemType { ID = 4, Name = "Item" },
                new ArtifactSystemType { ID = 5, Name = "Multiple Choice Group" },
                new ArtifactSystemType { ID = 6, Name = "Reference Item" }
            };

            return entities.SetCreatedByModifiedByList();
        }

        #endregion
    }
}
