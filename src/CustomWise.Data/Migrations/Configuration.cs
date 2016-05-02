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
            var artificatVersions = CreateModelCodeArtifactVersions();
            var specificationVersions = CreateModelSpecificationVersions();
            
            context.SpecificationTypes.AddOrUpdate(CreateSpecificationTypes());
            context.ArtifactTypes.AddOrUpdate(CreateArtifactTypes());
            context.MetaDataDefinitions.AddOrUpdate(CreateMetaDataDefinitions());
            context.SaveChanges();

            context.Artifacts.AddOrUpdate(CreateModelCodeArtifacts(artificatVersions));
            context.SaveChanges();

            context.Specifications.AddOrUpdate(CreateModelSpecifications(specificationVersions));
            context.SaveChanges();
        }

        #region Seed Methods
        private ArtifactVersion[] CreateModelCodeArtifactVersions() {
            var entities = new ArtifactVersion[] {
                new ArtifactVersion { Name = "Tomcat F21", Published = false },
                new ArtifactVersion { Name = "Tomcat F22", Published = false },
                new ArtifactVersion { Name = "Tomcat F24", Published = false },
                new ArtifactVersion { Name = "B52 21",     Published = false },
                new ArtifactVersion { Name = "B52 23",     Published = false }
            };

            return entities.SetId().SetAuditData();
        }

        private SpecificationVersion[] CreateModelSpecificationVersions() {
            var entities = new SpecificationVersion[] {
                new SpecificationVersion { Name = "Tomcat F21", Published = false },
                new SpecificationVersion { Name = "Tomcat F22", Published = false },
                new SpecificationVersion { Name = "Tomcat F24", Published = false },
                new SpecificationVersion { Name = "B52 21",     Published = false },
                new SpecificationVersion { Name = "B52 23",     Published = false }
            };

            return entities.SetId().SetAuditData();
        }

        private Artifact[] CreateModelCodeArtifacts(ArtifactVersion[] versions) {
            var entities = new Artifact[] {
                new Artifact { DisplayName = "Tomcat F21", IsActive = true, Deleted = false, ArtifactTypeId = 1 },
                new Artifact { DisplayName = "Tomcat F22", IsActive = true, Deleted = false, ArtifactTypeId = 1 },
                new Artifact { DisplayName = "Tomcat F24", IsActive = true, Deleted = false, ArtifactTypeId = 1 },
                new Artifact { DisplayName = "B52 21",     IsActive = true, Deleted = false, ArtifactTypeId = 1 },
                new Artifact { DisplayName = "B52 23",     IsActive = true, Deleted = false, ArtifactTypeId = 1 }
            };

            entities = entities.Select(e => {
                e.ArtifactVersion = versions.First(v => v.Name == e.DisplayName);
                return e;
            }).ToArray();

            return entities.SetId().SetAuditData();
        }

        private Specification[] CreateModelSpecifications(SpecificationVersion[] versions) {
            var entities = new Specification[] {
                new Specification { DisplayName = "Tomcat F21", IsActive = true, Deleted = false, SpecificationTypeId = 1 },
                new Specification { DisplayName = "Tomcat F22", IsActive = true, Deleted = false, SpecificationTypeId = 1 },
                new Specification { DisplayName = "Tomcat F24", IsActive = true, Deleted = false, SpecificationTypeId = 1 },
                new Specification { DisplayName = "B52 21",     IsActive = true, Deleted = false, SpecificationTypeId = 1 },
                new Specification { DisplayName = "B52 23",     IsActive = true, Deleted = false, SpecificationTypeId = 1 }
            };

            entities = entities.Select(e => {
                e.SpecificationVersion = versions.First(v => v.Name == e.DisplayName);
                return e;
            }).ToArray();

            return entities.SetId().SetAuditData();
        }

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

    static class ConfigurationExtensions {
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
