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
            //SeedModels(context);
        }

        private static void SeedColors(ICustomWiseMigrationContext context) {
            var palletCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-color-pallets.csv"));
            var colorCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-colors.csv"));
            var pallets = palletCsvData.Select(record => new { PalletName = record["Pallet Name"], ColorId = record["Gelcoat Color Id"] }).ToLookup(key => key.PalletName, val => val.ColorId);
            var rootType = context.ArtifactTypes.Single(t => t.SystemName == "root");
            var itemType = context.ArtifactTypes.Single(t => t.SystemName == "item");
            var choiceGroupType = context.ArtifactTypes.Single(t => t.SystemName == "choice_group");
            var colorMetaDataDefinitionDetails = context.MetaDataDefinitions.Single(mdd => mdd.Name == "Color").MetaDataDefinitionDetails;
            var rootArtifact = new Artifact { DisplayName = "Color Pallets", ItemTypeId = rootType.Id, IsActive = true, Order = 0 }.SetCreatedByModifiedBy();
            
            Func<MetaDataDefinitionDetail, IDictionary<string, string>, MetaData> generateColorMetaData = (colorMetaDataDetail, colorRecord) => {
                return new MetaData {
                    Key = colorMetaDataDetail.Name,
                    Value = colorRecord[colorMetaDataDetail.Name],
                    MetaDataDefinitionId = colorMetaDataDetail.MetaDataDefinitionId
                };
            };
            Func<IDictionary<string, string>, Artifact> generateColorArtificat = (colorRecord) => {
                return new Artifact {
                    DisplayName = colorRecord["Color Name"],
                    ItemTypeId  = itemType.Id,
                    IsActive    = true,
                    Order       = 0,
                    MetaData    = colorMetaDataDefinitionDetails.Select(detail => generateColorMetaData(detail, colorRecord)).SetCreatedByModifiedByList()
                };
            };
            Func<string, Artifact> generateColorPalletArtifact = (palletName) => {
                var palletColorIds = pallets[palletName];
                return new Artifact {
                    DisplayName = palletName,
                    ItemTypeId = choiceGroupType.Id,
                    IsActive = true,
                    Order = 0,
                    SubItems = colorCsvData.Where(colorRecord => palletColorIds.Contains(colorRecord["Id"])).Select(generateColorArtificat).SetCreatedByModifiedByList()
                };
            };
            Func<Artifact, ArtifactVersion> generateArtifactVersion = (artifact) => {
                return new ArtifactVersion {
                    Id                  = artifact.Id,
                    VersionNumber       = "1",
                    ArtifactReferenceId = artifact.ArtifactReferenceId,
                    Action              = "Insert",
                    IsActive            = artifact.IsActive,
                    DisplayName         = artifact.DisplayName,
                    ItemTypeId          = artifact.ItemTypeId,
                    Order               = artifact.Order,
                    ParentId            = artifact.ParentId,
                    CreatedBy           = artifact.CreatedBy,
                    CreatedDate         = artifact.CreatedDate,
                    ModifiedBy          = artifact.ModifiedBy,
                    ModifiedDate        = artifact.ModifiedDate
                };
            };
            Func<MetaData, MetaDataVersion> generateMetaDataVersion = (metaData) => {
                return new MetaDataVersion {
                    Id                   = metaData.Id,
                    Action               = "Insert",
                    VersionNumber        = "1",
                    ArtifactId           = metaData.ArtifactId,
                    SpecificationId      = metaData.SpecificationId,
                    Key                  = metaData.Key,
                    Value                = metaData.Value,
                    MetaDataDefinitionId = metaData.MetaDataDefinitionId,
                    CreatedBy            = metaData.CreatedBy,
                    CreatedDate          = metaData.CreatedDate,
                    ModifiedBy           = metaData.ModifiedBy,
                    ModifiedDate         = metaData.ModifiedDate
                };
            };

            rootArtifact.SubItems = pallets.Select(g => g.Key).Select(generateColorPalletArtifact)
                                                              .SetCreatedByModifiedByList();

            context.Artifacts.AddOrUpdate(a => new { a.DisplayName, a.ParentId, a.ItemTypeId }, rootArtifact);
            context.SaveChanges();

            var artifactVersions = new List<ArtifactVersion>();
            if (rootArtifact.Id > 0) {
                artifactVersions.Add(generateArtifactVersion(rootArtifact));
            }
            artifactVersions.AddRange(rootArtifact.SubItems.Flatten(a => a.SubItems)
                                                           .Where(a => a.Id > 0) // only create version for those artifact that have been saved (Id > 0)
                                                           .Select(generateArtifactVersion));
            var metaDataVersions = rootArtifact.SubItems.Flatten(a => a.SubItems)
                                                        .SelectMany(a => a.MetaData)
                                                        .Where(m => m.Id > 0) // only create version for those metadata that have been saved (Id > 0)
                                                        .Select(generateMetaDataVersion);

            context.ArtifactVersions.AddOrUpdate(artifactVersions.ToArray());
            context.MetaDataVersions.AddOrUpdate(metaDataVersions.ToArray());
        }
        //private static void SeedModels(ICustomWiseMigrationContext context) {
        //    var featuresCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-features.csv"));
        //    var featuresPricingCsvData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-feature-pricing.csv"));
        //    var rootType = context.SpecificationTypes.Single(t => t.SystemName == "root");
        //    var itemType = context.SpecificationTypes.Single(t => t.SystemName == "item");
        //    var artifactRootType = context.ArtifactTypes.Single(t => t.SystemName == "root");
        //    var artifactItemType = context.ArtifactTypes.Single(t => t.SystemName == "item");
        //    var featureRootArtifact = new Artifact {
        //        ArtifactVersion = new ArtifactVersion {
        //            Id = 1,
        //            Name = "Fetaures",
        //            Published = true,
        //            PublishedDate = DateTime.Now,
        //            CreatedBy = "system",
        //            CreatedDate = DateTime.Now,
        //            ModifiedBy = "system",
        //            ModifiedDate = DateTime.Now
        //        },
        //        DisplayName = "Color Pallets",
        //        ArtifactType = artifactRootType,
        //        IsActive = true,
        //        Order = 0,
        //        CreatedBy = "system",
        //        CreatedDate = DateTime.Now,
        //        ModifiedBy = "system",
        //        ModifiedDate = DateTime.Now
        //    };

        //    var modelYearMetaDataDefinition = new MetaDataDefinition {
        //        Name = "Model",
        //        CreatedBy = "system",
        //        CreatedDate = DateTime.Now,
        //        ModifiedBy = "system",
        //        ModifiedDate = DateTime.Now,
        //        MetaDataDefinitionDetails = new MetaDataDefinitionDetail[] {
        //            new MetaDataDefinitionDetail {
        //                Name = "Model Year",
        //                CreatedBy = "system",
        //                CreatedDate = DateTime.Now,
        //                ModifiedBy = "system",
        //                ModifiedDate = DateTime.Now
        //            }
        //        }
        //    };
        //    Func<MetaDataDefinition, IList<MetaData>> generateModelMetaData = (modelMetaDataDefinition) => {
        //        return new List<MetaData> {
        //            new MetaData {
        //                Id    = 1,
        //                Key   = "Model Year",
        //                Value = "2016",
        //                MetaDataDefinition = modelYearMetaDataDefinition
        //            }
        //        };
        //    };
        //    Func<IDictionary<string, string>, Specification> generateFeatureSpecification = (featureRecord) => {
        //        // create feature artifact
        //        var featureArtificat = new Artifact {
        //            DisplayName  = featureRecord["Feature Name"],
        //            ArtifactType = artifactItemType,
        //            IsActive     = true,
        //            Order        = 0,

        //            CreatedBy    = "system",
        //            CreatedDate  = DateTime.Now,
        //            ModifiedBy   = "system",
        //            ModifiedDate = DateTime.Now
        //        };
        //        return new Specification {

        //        };
        //    };
        //    Func<SpecificationVersion, Specification> generateModelSpecification = (version) => {
        //        return new Specification {
        //            DisplayName          = version.Name,
        //            SpecificationType    = rootType,
        //            IsActive             = true,
        //            Deleted              = false,
        //            MetaData             = generateModelMetaData(modelYearMetaDataDefinition).SetCreatedByModifiedByList(),
        //            SpecificationVersion = version,
        //            // features
        //            SubSpecifications    = featuresCsvData.Select(generateFeatureSpecification).SetCreatedByModifiedByList()
        //        };
        //    };
        //    Func<string, SpecificationVersion> generateModelSpecificationVersion = (modelName) => {
        //        return new SpecificationVersion {
        //            Name = modelName,
        //            Published = true,
        //            PublishedDate = DateTime.Now,
        //            CreatedBy = "system",
        //            CreatedDate = DateTime.Now,
        //            ModifiedBy = "system",
        //            ModifiedDate = DateTime.Now
        //        };
        //    };

        //    var modelSpecifications = featuresCsvData.Select(k => k["Model"])
        //                                             .Distinct()
        //                                             .Select(generateModelSpecificationVersion)
        //                                             .Select(generateModelSpecification);
        //}
    }
}
