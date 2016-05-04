using CustomWise.Data;
using CustomWise.Data.Entities;
using Sophcon.Collections;
using Sophcon.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Toolkit.Csv;
using Sophcon.Linq;

namespace CustomWise.Web {
    public class MigrationSeedConfig {
        public static void Seed(ICustomWiseMigrationContext context) {
            SeedColors(context);
            SeedModels(context);
        }

        private static void SeedColors(ICustomWiseMigrationContext context) {
            var palletCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-color-pallets.csv"));
            var colorCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-colors.csv"));
            var rootType = context.ArtifactTypes.Single(t => t.SystemName == "root");
            var itemType = context.ArtifactTypes.Single(t => t.SystemName == "item");
            var choiceGroupType = context.ArtifactTypes.Single(t => t.SystemName == "choice_group");
            var colorMetaDataDefinitionDetails = context.MetaDataDefinitions.Single(mdd => mdd.Name == "Color").MetaDataDefinitionDetails;
            var rootArtifact = new Artifact {
                ArtifactVersion = new ArtifactVersion {
                    Id = 1,
                    Name = "Color Pallets",
                    Published = true,
                    PublishedDate = DateTime.Now,
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.Now
                },
                DisplayName = "Color Pallets",
                ArtifactType = rootType,
                IsActive = true,
                Order = 0,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            };

            Func<MetaDataDefinitionDetail, IDictionary<string, string>, MetaData> generateColorMetaData = (colorMetaDataDetail, colorRecord) => {
                return new MetaData {
                    Key = colorMetaDataDetail.Name,
                    Value = colorRecord[colorMetaDataDetail.Name],
                    MetaDataDefinitionId = colorMetaDataDetail.MetaDataDefinitionId
                };
            };
            Func<IDictionary<string, string>, Artifact> generateColorArtificat = (colorRecord) => {
                return new Artifact {
                    ArtifactVersion = rootArtifact.ArtifactVersion,
                    DisplayName = colorRecord["Color Name"],
                    ArtifactTypeId = itemType.Id,
                    IsActive = true,
                    Order = 0,
                    MetaData = colorMetaDataDefinitionDetails.Select(detail => generateColorMetaData(detail, colorRecord)).SetCreatedByModifiedByList()
                };
            };
            Func<string, Artifact> generateColorPalletArtifact = (palletName) => {
                return new Artifact {
                    ArtifactVersion = rootArtifact.ArtifactVersion,
                    DisplayName = palletName,
                    ArtifactTypeId = choiceGroupType.Id,
                    IsActive = true,
                    Order = 0,
                    SubArtifacts = colorCsvData.Select(generateColorArtificat).SetCreatedByModifiedByList()
                };
            };

            rootArtifact.SubArtifacts = palletCsvData.Select(p => p["Pallet Name"])
                                                     .Distinct()
                                                     .Select(generateColorPalletArtifact)
                                                     .SetCreatedByModifiedByList();

            context.Artifacts.AddOrUpdate(rootArtifact);
        }
        private static void SeedModels(ICustomWiseMigrationContext context) {
            var featuresCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-features.csv"));
            var featuresPricingCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-feature-pricing.csv"));
            var modelSpecificationVersions = new SpecificationVersion[] {
                new SpecificationVersion { Name = "Tomcat F21", Published = false },
                new SpecificationVersion { Name = "Tomcat F22", Published = false },
                new SpecificationVersion { Name = "Tomcat F24", Published = false },
                new SpecificationVersion { Name = "B52 21",     Published = false },
                new SpecificationVersion { Name = "B52 23",     Published = false }
            }.SetId().SetCreatedByModifiedByList();
            var rootType = context.SpecificationTypes.Single(t => t.SystemName == "root");
            var itemType = context.SpecificationTypes.Single(t => t.SystemName == "item");
            var artifactRootType = context.ArtifactTypes.Single(t => t.SystemName == "root");
            var artifactItemType = context.ArtifactTypes.Single(t => t.SystemName == "item");
            var featureRootArtifact = new Artifact {
                ArtifactVersion = new ArtifactVersion {
                    Id = 1,
                    Name = "Fetaures",
                    Published = true,
                    PublishedDate = DateTime.Now,
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.Now
                },
                DisplayName = "Color Pallets",
                ArtifactType = artifactRootType,
                IsActive = true,
                Order = 0,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            };

            var modelYearMetaDataDefinition = new MetaDataDefinition {
                Name = "Model",
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now,
                MetaDataDefinitionDetails = new MetaDataDefinitionDetail[] {
                    new MetaDataDefinitionDetail {
                        Name = "Model Year",
                        CreatedBy = "system",
                        CreatedDate = DateTime.Now,
                        ModifiedBy = "system",
                        ModifiedDate = DateTime.Now
                    }
                }
            };
            Func<MetaDataDefinition, IList<MetaData>> generateModelMetaData = (modelMetaDataDefinition) => {
                return new List<MetaData> {
                    new MetaData {
                        Id    = 1,
                        Key   = "Model Year",
                        Value = "2016",
                        MetaDataDefinition = modelYearMetaDataDefinition
                    }
                };
            };
            Func<IDictionary<string, string>, Specification> generateFeatureSpecification = (featureRecord) => {
                // create feature artifact
                var featureArtificat = new Artifact {
                    DisplayName = featureRecord["Feature Name"],
                    ArtifactType = artifactItemType,
                    IsActive = true,
                    Order = 0,
                    
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.Now
                };
                return new Specification {

                };
            };
            Func<SpecificationVersion, Specification> generateSpecification = (version) => {
                return new Specification {
                    DisplayName = version.Name,
                    SpecificationType = rootType,
                    IsActive = true,
                    Deleted = false,
                    MetaData = generateModelMetaData(modelYearMetaDataDefinition).SetCreatedByModifiedByList(),
                    SpecificationVersion = version,
                    // features
                    SubSpecifications = featuresCsvData.Select(generateFeatureSpecification).SetCreatedByModifiedByList()
                };
            };

        }
    }
}
