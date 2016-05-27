namespace CustomWise.Data.Migrations {
    using Entities;
    using Entities.Base;
    using Sophcon;
    using Sophcon.Collections;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
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
                new SpecificationType { DisplayName = "Root",              SystemName = "root"         },
                new SpecificationType { DisplayName = "Group",             SystemName = "group"        },
                new SpecificationType { DisplayName = "Choice Group",      SystemName = "choice_group" },
                new SpecificationType { DisplayName = "Item",              SystemName = "item"         },
                new SpecificationType { DisplayName = "Reference Item",    SystemName = "ref_item"     }
            };

            return entities.SetId().SetCreatedByModifiedByList();
        }

        private ArtifactType[] CreateArtifactTypes() {
            var entities = new ArtifactType[] {
                new ArtifactType { DisplayName = "Root",              SystemName = "root"         },
                new ArtifactType { DisplayName = "Group",             SystemName = "group"        },
                new ArtifactType { DisplayName = "Choice Group",      SystemName = "choice_group" },
                new ArtifactType { DisplayName = "Item",              SystemName = "item"         },
                new ArtifactType { DisplayName = "Reference Item",    SystemName = "ref_item"     }
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
