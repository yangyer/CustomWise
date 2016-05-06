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

        internal ICustomWiseMigrationContext _context;

        public MigrationSeedConfig(ICustomWiseMigrationContext migrationContext) {
            _context = migrationContext;
        }

        void Seed() {
            var priceLevelMetaDataDefinitionDetails = _context.MetaDataDefinitions.Single(detail => detail.Name == "Pricing").MetaDataDefinitionDetails.ToList();
            var colorMetaDataDefinitionDetails = _context.MetaDataDefinitions.Single(mdd => mdd.Name == "Color").MetaDataDefinitionDetails.ToList();
            var specificationTypes = _context.SpecificationTypes.ToList();
            var artifactTypes = _context.ArtifactTypes.ToList();
            var pricingLevelData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/pricing-levels.csv"));
            var modelFeaturesData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-features.csv"));
            var modelFeaturePricingData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-feature-pricing.csv"));
            var modelFeaturePricingF24Data = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-feature-pricing-f24.csv"));
            var modelColorAreasData = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/model-color-areas.csv"));
            var gPallets = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-color-pallets.csv"));
            var uPallets = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/upholstery-color-pallets.csv"));
            var gColors = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/gelcoat-colors.csv"));
            var uColors = CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath("~/Migrations/upholstery-colors.csv"));
            var myDefinition = new MetaDataDefinition {
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

            var models = modelFeaturesData.Select(r => r["Model"]).Distinct().Select(modelName => new Specification {
                DisplayName = modelName,
                ItemTypeId = specificationTypes.Single(t => t.SystemName == "root").Id,
                IsActive = true,
                MetaData = GenerateModelMetaData(myDefinition),
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            }).ToList();

            _context.Specifications.AddRange(models);
            _context.SaveChanges();

            var features = (from m in models
                            join f in modelFeaturesData on m.DisplayName equals f["Model"]
                            select new Specification {
                                DisplayName = f["Feature Name"],
                                IsActive = true,
                                ItemTypeId = specificationTypes.Single(i => i.SystemName == "item").Id,
                                Parent = m,
                                ParentId = m.Id,
                                MetaData = modelFeaturePricingData.Where(mfp => mfp["Model"] == m.DisplayName && mfp["Feature Id"] == f["Feature Id"]).SelectMany(r => GenerateFeaturePriceMetaData(priceLevelMetaDataDefinitionDetails, r)).ToList(),
                                CreatedBy = "system",
                                CreatedDate = DateTime.Now,
                                ModifiedBy = "system",
                                ModifiedDate = DateTime.Now
                            }).ToList();

            _context.Specifications.AddRange(features.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var sections = (from m in models
                            join ca in modelColorAreasData on m.DisplayName equals ca["Model"]
                            select new Specification {
                                DisplayName = ca["Section"],
                                IsActive = true,
                                ItemTypeId = specificationTypes.Single(i => i.SystemName == "group").Id,
                                Parent = m,
                                ParentId = m.Id,
                                CreatedBy = "system",
                                CreatedDate = DateTime.Now,
                                ModifiedBy = "system",
                                ModifiedDate = DateTime.Now
                            }).Distinct(new SpecificationComparer()).ToList();

            _context.Specifications.AddRange(sections.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var colorAreas = (from s in sections
                              join ca in modelColorAreasData on new { Model = s.Parent.DisplayName, Section = s.DisplayName } equals new { Model = ca["Model"], Section = ca["Section"] }
                              select new Specification {
                                  DisplayName = ca["Area"],
                                  IsActive = true,
                                  ItemTypeId = specificationTypes.Single(i => i.SystemName == "group").Id,
                                  Parent = s,
                                  ParentId = s.Id,
                                  CreatedBy = "system",
                                  CreatedDate = DateTime.Now,
                                  ModifiedBy = "system",
                                  ModifiedDate = DateTime.Now
                              }).ToList();

            _context.Specifications.AddRange(colorAreas.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var colorPallets = (from ca in colorAreas
                                join cp in modelColorAreasData on new { Model = ca.Parent.Parent.DisplayName, Section = ca.Parent.DisplayName, Area = ca.DisplayName } equals new { Model = cp["Model"], Section = cp["Section"], Area = cp["Area"] }
                                select new Specification {
                                    DisplayName = cp["Pallet"],
                                    IsActive = true,
                                    ItemTypeId = specificationTypes.Single(i => i.SystemName == "group").Id,
                                    Parent = ca,
                                    ParentId = ca.Id,
                                    CreatedBy = "system",
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = "system",
                                    ModifiedDate = DateTime.Now
                                }).ToList();

            _context.Specifications.AddRange(colorPallets.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var gelColors = (from cp in colorPallets
                             join cpd in gPallets on cp.DisplayName equals cpd["Pallet Name"]
                             join c in gColors on cpd["Color Id"] equals c["Id"]
                             select new Specification {
                                 DisplayName = c["Color Name"],
                                 IsActive = true,
                                 ItemTypeId = specificationTypes.Single(i => i.SystemName == "item").Id,
                                 MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c)).ToList(),
                                 Parent = cp,
                                 ParentId = cp.Id,
                                 CreatedBy = "system",
                                 CreatedDate = DateTime.Now,
                                 ModifiedBy = "system",
                                 ModifiedDate = DateTime.Now
                             }).ToList();

            _context.Specifications.AddRange(gelColors.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var upColors = (from cp in colorPallets
                             join cpd in uPallets on cp.DisplayName equals cpd["Pallet Name"]
                             join c in uColors on cpd["Color Id"] equals c["Id"]
                             select new Specification {
                                 DisplayName = c["Color Name"],
                                 IsActive = true,
                                 ItemTypeId = specificationTypes.Single(i => i.SystemName == "item").Id,
                                 MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c)).ToList(),
                                 Parent = cp,
                                 ParentId = cp.Id,
                                 CreatedBy = "system",
                                 CreatedDate = DateTime.Now,
                                 ModifiedBy = "system",
                                 ModifiedDate = DateTime.Now
                             }).ToList();

            _context.Specifications.AddRange(upColors.Flatten(s => s.SubItems));
            _context.SaveChanges();
        }

        // **
        internal MetaData GenerateColorMetaData(MetaDataDefinitionDetail detail, IDictionary<string, string> record) {
            return new MetaData {
                Key = detail.Name,
                Value = record[detail.Name],
                MetaDataDefinitionId = detail.MetaDataDefinitionId,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now,
            };
        }
        // **
        internal MetaData[] GenerateFeaturePriceMetaData(IEnumerable<MetaDataDefinitionDetail> priceMetaDetails, IDictionary<string, string> record) {
            return priceMetaDetails.Select(detail => new MetaData {
                Key = detail.Name,
                Value = record[detail.Name],
                MetaDataDefinitionId = detail.MetaDataDefinitionId,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            }).ToArray();
        }
        // **
        internal IList<MetaData> GenerateModelMetaData(MetaDataDefinition modelMetaDataDefinition) {
            return new List<MetaData> {
                new MetaData {
                    Id    = 1,
                    Key   = "Model Year",
                    Value = "2016",
                    MetaDataDefinition = modelMetaDataDefinition,
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public static void Seed(ICustomWiseMigrationContext context) {
            if (context.Specifications.Count() < 1) {
                var instance = new MigrationSeedConfig(context);
                instance.Seed();
            }
        }

    }

    public class SpecificationComparer : IEqualityComparer<Specification> {
        public bool Equals(Specification x, Specification y) => x.DisplayName == y.DisplayName && x.Parent?.DisplayName == y.Parent?.DisplayName;
        public int GetHashCode(Specification obj) => obj.DisplayName.GetHashCode();
    }
}