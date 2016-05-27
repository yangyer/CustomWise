namespace CustomWise.Data.Migrations {
    using Entities;
    using Sophcon.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomWise.Data.CustomWiseModel> {
        enum MetaDataDefinitionTypes { Color, Image, Pricing, All }

        public Configuration() {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CustomWise.Data.CustomWiseModel context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.SpecificationTypes.AddOrUpdate(CreateSpecificationTypes());
            context.ArtifactTypes.AddOrUpdate(CreateArtifactTypes());
            context.MetaDataDefinitions.AddOrUpdate(CreateMetaDataDefinitions());
            context.SaveChanges();

            //if(!Debugger.IsAttached) {
            //    Debugger.Launch();
            //}
        }
        
        #region Seed Methods

        private SpecificationType[] CreateSpecificationTypes() {
            var entities = new SpecificationType[] {
                new SpecificationType { Name = "Root",              SystemName = "root"         },
                new SpecificationType { Name = "Group",             SystemName = "group"        },
                new SpecificationType { Name = "Choice Group",      SystemName = "choice_groBup" },
                new SpecificationType { Name = "Item",              SystemName = "item"         },
                new SpecificationType { Name = "Reference Item",    SystemName = "ref_item"     }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        private ArtifactType[] CreateArtifactTypes() {
            var entities = new ArtifactType[] {
                new ArtifactType { Name = "Root",              SystemName = "root"         },
                new ArtifactType { Name = "Group",             SystemName = "group"        },
                new ArtifactType { Name = "Choice Group",      SystemName = "choice_group" },
                new ArtifactType { Name = "Item",              SystemName = "item"         },
                new ArtifactType { Name = "Reference Item",    SystemName = "ref_item"     }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        private MetaDataDefinition[] CreateMetaDataDefinitions() {
            var entities = new MetaDataDefinition[] {
                new MetaDataDefinition {
                    Name = "Color",
                    MetaDataDefinitionDetails = CreateMetaDataDefinitionDetails(MetaDataDefinitionTypes.Color)
                },
                new MetaDataDefinition {
                    Name = "Image",
                    MetaDataDefinitionDetails = CreateMetaDataDefinitionDetails(MetaDataDefinitionTypes.Image)
                },
                new MetaDataDefinition {
                    Name = "Pricing",
                    MetaDataDefinitionDetails = CreateMetaDataDefinitionDetails(MetaDataDefinitionTypes.Pricing)
                }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        private MetaDataDefinitionDetail[] CreateMetaDataDefinitionDetails(MetaDataDefinitionTypes type) {
            IDictionary<MetaDataDefinitionTypes, MetaDataDefinitionDetail[]> lookup = new Dictionary<MetaDataDefinitionTypes, MetaDataDefinitionDetail[]> {
                { MetaDataDefinitionTypes.Color, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "Hex Color" }
                    }
                },
                { MetaDataDefinitionTypes.Image, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "Img Url" },
                        new MetaDataDefinitionDetail { Name = "Alt Text" }
                    }
                },
                {
                    MetaDataDefinitionTypes.Pricing, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "Price Level Id" },
                        new MetaDataDefinitionDetail { Name = "Price" }
                    }
                }
            };

            return (type == MetaDataDefinitionTypes.All ? lookup.SelectMany(k => k.Value).ToArray() : lookup[type]).SetId().SetCreatedByModifiedByList();
        }
        #endregion
    }
}
