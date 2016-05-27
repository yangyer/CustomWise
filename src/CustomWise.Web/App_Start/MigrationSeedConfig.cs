using CustomWise.Data;
using CustomWise.Data.Entities;
using Sophcon.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Toolkit.Csv;

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

        internal ICustomWiseContext _context;

        public MigrationSeedConfig(ICustomWiseContext migrationContext) {
            _context = migrationContext;
        }

        internal IDictionary<string, string>[] ParseCsv(string fileName) {
            return CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath($"~/Migrations/{fileName}"));
        }

        void Seed() {
            //var priceLevelMetaDataDefinitionDetails = _context.MetaDataDefinitions.Single(detail => detail.Name == "Pricing").MetaDataDefinitionDetails.ToList();
            //var colorMetaDataDefinitionDetails      = _context.MetaDataDefinitions.Single(mdd => mdd.Name == "Color").MetaDataDefinitionDetails.ToList();
            var specificationTypes                  = _context.SpecificationTypes.ToList();
            var artifactTypes                       = _context.ArtifactTypes.ToList();
            var artifactSystemTypes                 = _context.ArtifactSystemTypes.ToList();


            var modelfeaturesSource       = ParseCsv("model-features.csv")              .Select(r => new { Raw = r, Model = r["Model"], FeatureId = r["Feature Id"], FeatureName = r["Feature Name"], Price = r["Price"] });
            var modelFeaturePricingSource = ParseCsv("model-feature-pricing.csv")       .Select(r => new { Raw = r, Model = r["Model"], PriceLevelId = r["Price Level Id"], FeatureId = r["Feature Id"], Price = r["Price"] });
            var modelColorAreasSource     = ParseCsv("model-color-areas.csv")           .Select(r => new { Raw = r, Model = r["Model"], Section = r["Section"], Area = r["Area"], Pallet = r["Pallet"] });
            var pricingLevelSource        = ParseCsv("pricing-levels.csv")              .Select(r => new { Raw = r, PriceLevelId = r["Price Level Id"], PriceLevelName = r["Price Level Name"], Active = r["Active"] });
            var gelPallets                = ParseCsv("gelcoat-color-pallets.csv")       .Select(r => new { Raw = r, PalletName = r["Pallet Name"], ColorId = r["Color Id"] });
            var upholPallets              = ParseCsv("upholstery-color-pallets.csv")    .Select(r => new { Raw = r, PalletName = r["Pallet Name"], ColorId = r["Color Id"] });
            var gelColors                 = ParseCsv("gelcoat-colors.csv")              .Select(r => new { Raw = r, Id = r["Id"], Section = r["Section"], HexColor = r["Hex Color"], ColorName = r["Color Name"] });
            var upholColors               = ParseCsv("upholstery-colors.csv")           .Select(r => new { Raw = r, Id = r["Id"], Section = r["Section"], HexColor = r["Hex Color"], ColorName = r["Color Name"], TextureName = r["Texture Name"], SwatchNumber = r["Swatch Number"] });

            #region new code
            var modelNames = modelfeaturesSource.Select(mf => mf.Model).Distinct().OrderBy(name => name).ToList();
            var sections = modelColorAreasSource.Select(mc => mc.Section).Distinct().ToList();
            var colorPalletArtifacts =
                (from section in sections
                 select new Artifact {
                     ID = section.IndexOf(section, StringComparison.Ordinal) + 1,
                     DisplayName = section,
                     ArtifactSystemType = artifactSystemTypes.First(ast => ast.Name.Equals("group", StringComparison.OrdinalIgnoreCase)),
                     ArtifactType = artifactTypes.First(at => at.Name.Equals("color area", StringComparison.OrdinalIgnoreCase)),
                     IsActive = true,
                     Order = section.IndexOf(section, StringComparison.Ordinal),
                     //SubItems = 
                     //   (from ca in gelPallets
                     //    where ca.)
                 }).ToList();

            var rootSpecifications =
                (from modelName in modelNames
                 select new Specification {
                     Order = modelNames.IndexOf(modelName),
                     DisplayName = modelName,
                     // MetaData
                     SubItems = new List<Specification> {
                         new Specification {
                             DisplayName = "Options",
                             SpecificationSystemType = null, // Need to inject system type here
                             SpecificationType = null,
                             SubItems = (from mf in modelfeaturesSource
                                         where mf.Model == modelName
                                         select new Specification {
                                         }).ToList()
                         }
                     }
                 });
            #endregion

            #region old code
            /*
            var version = new { CurrentVersionNumber = 1 };
            _context.SaveChanges();

            #region Version Funcs
            //Func<Artifact, ArtifactVersion> generateArtifactVersion = (artifact) => {
            //    return new ArtifactVersion {
            //        ArtifactId = artifact.Id,
            //        VersionNumber = version.CurrentVersionNumber.ToString(),
            //        ArtifactReferenceId = artifact.ArtifactReferenceId,
            //        Action = SophconEntityState.Added.ToString(),
            //        IsActive = artifact.IsActive,
            //        DisplayName = artifact.DisplayName,
            //        ItemTypeId = artifact.ItemTypeId,
            //        Order = artifact.Order,
            //        ParentID = artifact.ParentID,
            //        CreatedBy = artifact.CreatedBy,
            //        CreatedDate = artifact.CreatedDate,
            //        ModifiedBy = artifact.ModifiedBy,
            //        ModifiedDate = artifact.ModifiedDate
            //    };
            //};
            //Func<Specification, SpecificationVersion> generateSpecificationVersion = (spec) => {
            //    return new SpecificationVersion {
            //        SpecificationId = spec.Id,
            //        VersionNumber = version.CurrentVersionNumber.ToString(),
            //        ArtifactReferenceId = spec.ArtifactReferenceId,
            //        Action = SophconEntityState.Added.ToString(),
            //        IsActive = spec.IsActive,
            //        DisplayName = spec.DisplayName,
            //        ItemTypeId = spec.ItemTypeId,
            //        Order = spec.Order,
            //        ParentID = spec.ParentID,
            //        CreatedBy = spec.CreatedBy,
            //        CreatedDate = spec.CreatedDate,
            //        ModifiedBy = spec.ModifiedBy,
            //        ModifiedDate = spec.ModifiedDate
            //    };
            //};
            //Func<MetaData, MetaDataVersion> generateMetaDataVersion = (metaData) => {
            //    return new MetaDataVersion {
            //        MetaDataId = metaData.Id,
            //        Action = SophconEntityState.Added.ToString(),
            //        VersionNumber = version.CurrentVersionNumber.ToString(),
            //        Key = metaData.Key,
            //        Value = metaData.Value,
            //        MetaDataDefinitionId = metaData.MetaDataDefinitionId,
            //        CreatedBy = metaData.CreatedBy,
            //        CreatedDate = metaData.CreatedDate,
            //        ModifiedBy = metaData.ModifiedBy,
            //        ModifiedDate = metaData.ModifiedDate
            //    };
            //};
            #endregion

            #region Root Entities
            var modelYearDefinition = new MetaDataDefinition {
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
            _context.MetaDataDefinitions.Add(modelYearDefinition);
            _context.SaveChanges();

            var featureArtifactRoot = new Artifact {
                DisplayName = "Features",
                IsActive = true,
                ArtifactSystemTypeID = artifactTypes.Single(i => i.SystemName == "root").ID,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            };
            _context.Artifacts.Add(featureArtifactRoot);
            _context.SaveChanges();
            //_context.ArtifactVersions.Add(generateArtifactVersion(featureArtifactRoot));

            var colorPalletArtifactRoot = new Artifact {
                DisplayName = "Color Pallets",
                IsActive = true,
                ArtifactTypeID = artifactTypes.Single(i => i.SystemName == "root").ID,
                CreatedBy = "system",
                CreatedDate = DateTime.Now,
                ModifiedBy = "system",
                ModifiedDate = DateTime.Now
            };
            _context.Artifacts.Add(colorPalletArtifactRoot);
            _context.SaveChanges();
            //_context.ArtifactVersions.Add(generateArtifactVersion(colorPalletArtifactRoot));

            #endregion

            #region Models
            var models =
                    (from modelName in modelfeaturesSource.Select(f => f.Model).Distinct()
                     select new Specification {
                         DisplayName = modelName,
                         SpecificationTypeID = specificationTypes.Single(t => t.SystemName == "root").ID,
                         IsActive = true,
                         MetaData = GenerateModelMetaData(modelYearDefinition),
                         CreatedBy = "system",
                         CreatedDate = DateTime.Now,
                         ModifiedBy = "system",
                         ModifiedDate = DateTime.Now
                     }).ToList();

            _context.Specifications.AddRange(models);
            _context.SaveChanges();

            //var modelSpecificationVersions = models.Flatten(s => s.SubItems).Select(generateSpecificationVersion).ToList();
            //_context.SpecificationVersions.AddRange(modelSpecificationVersions);
            //_context.MetaDataVersions.AddRange(models.SelectMany(m => m.MetaData.Select(generateMetaDataVersion)).ToList());
            //_context.SaveChanges();
            #endregion

            #region Model Features
            // artifacts
            var featureArtifacts =
                    (from f in modelfeaturesSource.Select(mf => new { mf.FeatureName, mf.FeatureId }).Distinct()
                     select new Artifact {
                         DisplayName = f.FeatureName,
                         IsActive = true,
                         ArtifactTypeID = artifactTypes.Single(i => i.SystemName == "item").ID,
                         ParentID = featureArtifactRoot.ID,
                         MetaData =
                            (from m in models
                             join p in modelFeaturePricingSource on new { Model = m.DisplayName, FeatureId = f.FeatureId } equals new { Model = p.Model, FeatureId = p.FeatureId }
                             select GenerateFeaturePriceMetaData(priceLevelMetaDataDefinitionDetails, p.Raw)).SelectMany(meta => meta).ToList(),
                         CreatedBy = "system",
                         CreatedDate = DateTime.Now,
                         ModifiedBy = "system",
                         ModifiedDate = DateTime.Now
                     }).ToList();

            _context.Artifacts.AddRange(featureArtifacts.Flatten(s => s.SubItems));
            _context.SaveChanges();

            // ref-specification
            var modelFeatureGroupSpec =
                (from m in models
                 select new Specification {
                     IsActive = true,
                     DisplayName = "Options",
                     SpecificationTypeID = specificationTypes.Single(i => i.SystemName == "group").ID,
                     Parent = m,
                     ParentID = m.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Specifications.AddRange(modelFeatureGroupSpec);
            _context.SaveChanges();

            var features =
                (from m in modelFeatureGroupSpec
                 join f in modelfeaturesSource on m.Parent.DisplayName equals f.Model
                 join a in featureArtifacts on f.FeatureName equals a.DisplayName
                 select new Specification {
                     ArtifactReferenceID = a.ID.ToString(),
                     IsActive = true,
                     DisplayName = a.DisplayName,
                     SpecificationTypeID = specificationTypes.Single(i => i.SystemName == "ref_item").ID,
                     MetaData = a.MetaData,
                     Parent = m,
                     ParentID = m.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Specifications.AddRange(features.Flatten(s => s.SubItems));
            _context.SaveChanges();

            // version
            //_context.SpecificationVersions.AddRange(modelFeatureGroupSpec.Select(generateSpecificationVersion));
            //_context.SaveChanges();

            //var featureArtifactVersions = featureArtifacts.Flatten(s => s.SubItems).Select(generateArtifactVersion).ToList();
            //_context.ArtifactVersions.AddRange(featureArtifactVersions);
            //_context.SaveChanges();

            //var featureArtifactMetaDataVersions = featureArtifacts.Flatten(s => s.SubItems).SelectMany(s => s.MetaData).Select(generateMetaDataVersion);
            //_context.MetaDataVersions.AddRange(featureArtifactMetaDataVersions);
            //_context.SaveChanges();

            //var featureSpecificationVersions = features.Flatten(s => s.SubItems).Select(generateSpecificationVersion).ToList();
            //_context.SpecificationVersions.AddRange(featureSpecificationVersions);
            //_context.SaveChanges();
            #endregion

            #region Sections & Areas
            var sections =
                    (from m in models
                     join ca in modelColorAreasSource.Select(s => new { s.Section, s.Model }).Distinct() on m.DisplayName equals ca.Model
                     select new Specification {
                         DisplayName = ca.Section,
                         IsActive = true,
                         SpecificationTypeID = specificationTypes.Single(i => i.SystemName == "group").ID,
                         Parent = m,
                         ParentID = m.ID,
                         CreatedBy = "system",
                         CreatedDate = DateTime.Now,
                         ModifiedBy = "system",
                         ModifiedDate = DateTime.Now
                     }).ToList();

            _context.Specifications.AddRange(sections.Flatten(s => s.SubItems));
            _context.SaveChanges();

            var colorAreas =
                (from s in sections
                 join ca in modelColorAreasSource on new { Model = s.Parent.DisplayName, Section = s.DisplayName } equals new { Model = ca.Model, Section = ca.Section }
                 select new Specification {
                     DisplayName = ca.Area,
                     IsActive = true,
                     SpecificationTypeID = specificationTypes.Single(i => i.SystemName == "group").ID,
                     Parent = s,
                     ParentID = s.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Specifications.AddRange(colorAreas.Flatten(s => s.SubItems));
            _context.SaveChanges();
            #endregion

            #region Pallets & Colors
            // pallet artifacts
            var colorPalletArtificats =
                (from pallet in modelColorAreasSource.Select(r => r.Pallet).Distinct()
                 select new Artifact {
                     DisplayName = pallet,
                     IsActive = true,
                     ArtifactTypeID = specificationTypes.Single(i => i.SystemName == "group").ID,
                     Parent = colorPalletArtifactRoot,
                     ParentID = colorPalletArtifactRoot.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Artifacts.AddRange(colorPalletArtificats.Flatten(s => s.SubItems));
            _context.SaveChanges();

            // pallet specifications
            var colorPallets =
                (from ca in colorAreas
                 join cp in modelColorAreasSource on new { Model = ca.Parent.Parent.DisplayName, Section = ca.Parent.DisplayName, Area = ca.DisplayName } equals new { Model = cp.Model, Section = cp.Section, Area = cp.Area }
                 join a in colorPalletArtificats on cp.Pallet equals a.DisplayName
                 select new Specification {
                     ArtifactReferenceID = a.ID.ToString(),
                     IsActive = true,
                     SpecificationTypeID = specificationTypes.Single(i => i.SystemName == "ref_item").ID,
                     Parent = ca,
                     ParentID = ca.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Specifications.AddRange(colorPallets.Flatten(s => s.SubItems));
            _context.SaveChanges();

            // color artifacts
            var gelcoatColorArtifacts =
                (from cp in colorPalletArtificats
                 join p in gelPallets on cp.DisplayName equals p.PalletName
                 join c in gelColors on p.ColorId equals c.Id
                 select new Artifact {
                     DisplayName = c.ColorName,
                     IsActive = true,
                     ArtifactTypeID = specificationTypes.Single(i => i.SystemName == "item").ID,
                     MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c.Raw)).ToList(),
                     Parent = cp,
                     ParentID = cp.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Artifacts.AddRange(gelcoatColorArtifacts.Flatten(s => s.SubItems));
            _context.SaveChanges();

            //var gelcoatColors =
            //    (from cp in colorPallets
            //     join p in gelPallets on cp.DisplayName equals p.PalletName
            //     join c in gelColors on p.ColorId equals c.Id
            //     select new Specification {
            //         DisplayName = c.ColorName,
            //         IsActive = true,
            //         ItemTypeId = specificationTypes.Single(i => i.SystemName == "item").Id,
            //         MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c.Raw)).ToList(),
            //         Parent = cp,
            //         ParentID = cp.ID,
            //         CreatedBy = "system",
            //         CreatedDate = DateTime.Now,
            //         ModifiedBy = "system",
            //         ModifiedDate = DateTime.Now
            //     }).ToList();

            //_context.Specifications.AddRange(gelcoatColors.Flatten(s => s.SubItems));
            //_context.SaveChanges();

            var upholColorArtifacts =
                (from cp in colorPalletArtificats
                 join p in upholPallets on cp.DisplayName equals p.PalletName
                 join c in upholColors on p.ColorId equals c.Id
                 select new Artifact {
                     DisplayName = c.ColorName,
                     IsActive = true,
                     ArtifactTypeID = specificationTypes.Single(i => i.SystemName == "item").ID,
                     MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c.Raw)).ToList(),
                     Parent = cp,
                     ParentID = cp.ID,
                     CreatedBy = "system",
                     CreatedDate = DateTime.Now,
                     ModifiedBy = "system",
                     ModifiedDate = DateTime.Now
                 }).ToList();

            _context.Artifacts.AddRange(upholColorArtifacts.Flatten(s => s.SubItems));
            _context.SaveChanges();

            //var upColors =
            //    (from cp in colorPallets
            //     join p in upholPallets on cp.DisplayName equals p.PalletName
            //     join c in upholColors on p.ColorId equals c.Id
            //     select new Specification {
            //         DisplayName = c.ColorName,
            //         IsActive = true,
            //         ItemTypeId = specificationTypes.Single(i => i.SystemName == "item").Id,
            //         MetaData = colorMetaDataDefinitionDetails.Select(detail => GenerateColorMetaData(detail, c.Raw)).ToList(),
            //         Parent = cp,
            //         ParentID = cp.ID,
            //         CreatedBy = "system",
            //         CreatedDate = DateTime.Now,
            //         ModifiedBy = "system",
            //         ModifiedDate = DateTime.Now
            //     }).ToList();

            //_context.Specifications.AddRange(upColors.Flatten(s => s.SubItems));
            //_context.SaveChanges(); 

            // version
            //var colorPalletArtifactVersions = colorPalletArtificats.Flatten(s => s.SubItems).Select(generateArtifactVersion).ToList();
            //_context.ArtifactVersions.AddRange(colorPalletArtifactVersions);
            //_context.SaveChanges();

            //var gelColorArtifactMetaDataVersions = gelcoatColorArtifacts.Flatten(s => s.SubItems).SelectMany(s => s.MetaData).Select(generateMetaDataVersion);
            //_context.MetaDataVersions.AddRange(gelColorArtifactMetaDataVersions);
            //_context.SaveChanges();

            //var upholColorArtifactMetaDataVersions = upholColorArtifacts.Flatten(s => s.SubItems).SelectMany(s => s.MetaData).Select(generateMetaDataVersion);
            //_context.MetaDataVersions.AddRange(upholColorArtifactMetaDataVersions);
            //_context.SaveChanges();

            //var colorPalletSpecificationVersions = colorPallets.Flatten(s => s.SubItems).Select(generateSpecificationVersion).ToList();
            //_context.SpecificationVersions.AddRange(colorPalletSpecificationVersions);
            //_context.SaveChanges();
            #endregion
            */
            #endregion
        }

        // **
        //internal SpecificationMetadata GenerateColorMetaData(MetaDataDefinitionDetail detail, IDictionary<string, string> record) {
        //    return new SpecificationMetadata {
        //        Key = detail.Name,
        //        Value = record[detail.Name],
        //        MetaDataDefinitionId = detail.MetaDataDefinitionId,
        //        CreatedBy = "system",
        //        CreatedDate = DateTime.Now,
        //        ModifiedBy = "system",
        //        ModifiedDate = DateTime.Now,
        //    };
        //}
        //// **
        //internal SpecificationMetadata[] GenerateFeaturePriceMetaData(IEnumerable<MetaDataDefinitionDetail> priceMetaDetails, IDictionary<string, string> record) {
        //    return priceMetaDetails.Select(detail => new SpecificationMetadata {
        //        Key = detail.Name,
        //        Value = record[detail.Name],
        //        MetaDataDefinitionId = detail.MetaDataDefinitionId,
        //        CreatedBy = "system",
        //        CreatedDate = DateTime.Now,
        //        ModifiedBy = "system",
        //        ModifiedDate = DateTime.Now
        //    }).ToArray();
        //}
        //// **
        //internal IList<SpecificationMetadata> GenerateModelMetaData(MetaDataDefinition modelMetaDataDefinition) {
        //    return new List<SpecificationMetadata> {
        //        new SpecificationMetadata {
        //            ID    = 1,
        //            Key   = "Model Year",
        //            Value = "2016",
        //            MetaDataDefinition = modelMetaDataDefinition,
        //            CreatedBy = "system",
        //            CreatedDate = DateTime.Now,
        //            ModifiedBy = "system",
        //            ModifiedDate = DateTime.Now
        //        }
        //    };
        //}

        public static void Seed(ICustomWiseContext context) {
            if (context.Specifications.Count() < 1) {
                var instance = new MigrationSeedConfig(context);
                instance.Seed();
            }
        }

    }
}