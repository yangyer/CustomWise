namespace CustomWise.Data.Migrations {
    using Entities;
    using Sophcon;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomWise.Data.CustomWiseModel> {
        readonly string _system = "system";
        readonly DateTime _now = DateTime.Now;
        enum MetaDataDefinitionTypes { Color, Image, Pricing, All }
        public Configuration() {
            AutomaticMigrationsEnabled = true;
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

            return entities.SetId().SetAuditData();
        }

        private ArtifactType[] CreateArtifactTypes() {
            var entities = new ArtifactType[] {
                new ArtifactType { DisplayName = "Root",              SystemName = "root"         },
                new ArtifactType { DisplayName = "Group",             SystemName = "group"        },
                new ArtifactType { DisplayName = "Choice Group",      SystemName = "choice_group" },
                new ArtifactType { DisplayName = "Item",              SystemName = "item"         },
                new ArtifactType { DisplayName = "Reference Item",    SystemName = "ref_item"     }
            };

            return entities.SetId().SetAuditData();
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

            return entities.SetId().SetAuditData();
        }

        private MetaDataDefinitionDetail[] CreateMetaDataDefinitionDetails(MetaDataDefinitionTypes type) {
            IDictionary<MetaDataDefinitionTypes, MetaDataDefinitionDetail[]> lookup = new Dictionary<MetaDataDefinitionTypes, MetaDataDefinitionDetail[]> {
                { MetaDataDefinitionTypes.Color, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "RGB" }
                    }
                },
                { MetaDataDefinitionTypes.Image, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "ImgUrl" },
                        new MetaDataDefinitionDetail { Name = "AltText" }
                    }
                },
                {
                    MetaDataDefinitionTypes.Pricing, new MetaDataDefinitionDetail[] {
                        new MetaDataDefinitionDetail { Name = "Level" },
                        new MetaDataDefinitionDetail { Name = "Price" },
                        new MetaDataDefinitionDetail { Name = "Order" }
                    }
                }
            };

            return (type == MetaDataDefinitionTypes.All ? lookup.SelectMany(k => k.Value).ToArray() : lookup[type]).SetId().SetAuditData();
        }
        #endregion
    }

    static class EfSeedHelper {
        public static T[] SetId<T>(this IEnumerable<T> source)
            where T : BaseEntity {

            var temp = source.ToList();
            temp.ToList()
                  .ForEach((e) => {
                      var prop = e.GetType().GetProperty("Id");
                      prop.SetValue(e, temp.IndexOf(e) + 1);
                  });

            return temp.ToArray();
        }
        public static T[] SetAuditData<T>(this IEnumerable<T> source)
            where T : BaseEntity {

            var temp = source.ToList();
            var defaultFields = new List<string> { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            temp.ToList()
                  .ForEach((e) => {
                      defaultFields.ForEach(propName => {
                          var prop = e.GetType().GetProperty(propName);
                          if(prop != null) {
                              if (prop.Name.Contains("Date")) {
                                  prop.SetValue(e, DateTime.Now);
                              } else {
                                  prop.SetValue(e, "system");
                              }
                          }
                      });
                  });

            return temp.ToArray();
        }
    }
}
