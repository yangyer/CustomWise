using CustomWise.Data;
using CustomWise.Data.Entities;
using Sophcon;
using Sophcon.Collections;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Toolkit.Csv;

namespace CustomWise.Web {
    public static class SeedExtensions {
        public static IEnumerable<Artifact> SetOrder(this IEnumerable<Artifact> source) {
            var tempList = source.ToList();
            tempList.ForEach(item => item.Order = tempList.IndexOf(item));
            return tempList.ToArray();
        }
        public static IEnumerable<Specification> SetOrder(this IEnumerable<Specification> source) {
            var tempList = source.ToList();
            tempList.ForEach(item => item.Order = tempList.IndexOf(item));
            return tempList.ToArray();
        }
    }
    public class MigrationSeedConfig {
        ICustomWiseContext _context;

        public MigrationSeedConfig(ICustomWiseContext migrationContext) {
            _context = migrationContext;
        }

        IDictionary<string, string>[] ParseCsv(string fileName) {
            return CsvParser.ParseFileAsDictionaries(',', HttpContext.Current.Server.MapPath($"~/Migrations/{fileName}"));
        }

        void Seed() {
            var artifactSystemTypes       = _context.ArtifactSystemTypes.ToList();
            var specificationSystemTypes  = _context.SpecificationSystemTypes.ToList();

            var modelfeaturesSource       = ParseCsv("model-features.csv")                  .Select(r => new { Raw = r, Model = r["Model"], FeatureId = r["Feature Id"], FeatureName = r["Feature Name"], Price = r["Price"] });
            var modelFeaturePricingSource = ParseCsv("model-feature-pricing.csv")           .Select(r => new { Raw = r, Model = r["Model"], PriceLevelId = r["Price Level Id"], FeatureId = r["Feature Id"], Price = r["Price"] });
            var modelColorAreasSource     = ParseCsv("model-color-areas.csv")               .Select(r => new { Raw = r, Model = r["Model"], Section = r["Section"], Area = r["Area"], Pallet = r["Pallet"] });
            var pricingLevelSource        = ParseCsv("pricing-levels.csv")                  .Select(r => new { Raw = r, PriceLevelId = r["Price Level Id"], PriceLevelName = r["Price Level Name"], Active = r["Active"] });
            var gelPallets                = ParseCsv("gelcoat-color-pallets.csv")           .Select(r => new { Raw = r, PalletName = r["Pallet Name"], ColorID = r["Color Id"] });
            var upholPallets              = ParseCsv("upholstery-color-pallets.csv")        .Select(r => new { Raw = r, PalletName = r["Pallet Name"], ColorID = r["Color Id"] });
            var gelColors                 = ParseCsv("gelcoat-colors.csv")                  .Select(r => new { Raw = r, ID = r["Id"], Section = r["Section"], HexColor = r["Hex Color"], ColorName = r["Color Name"] });
            var upholColors               = ParseCsv("upholstery-colors.csv")               .Select(r => new { Raw = r, ID = r["Id"], Section = r["Section"], HexColor = r["Hex Color"], ColorName = r["Color Name"], TextureName = r["Texture Name"], SwatchNumber = r["Swatch Number"] });
            var typeMetadata              = ParseCsv("specification-type-definitions.csv")  .Select(r => new { Raw = r, ID = r["SpecificationTypeDefinition"], Name = r["Name"], Description = r["Description"], ParentID = r["ParentId"], SytemType = r["SystemType"], MetadataKey = r["MetadataKey"], ValueType = r["ValueType"], DefaultValue = r["DefaultValue"], AllowOverride = r["AllowOverride"], InputTemplate = r["InputTemplate"], Required = r["Required"], Localized = r["Localized"] });

            #region Create SpecificationType and ArtifactType
            var specificationMetaDefID = 1;
            var specificationTypes = typeMetadata
                .GroupBy(st => new { st.ID, st.Name, st.Description, st.ParentID })
                .Select(groupSt => new SpecificationType {
                     ID = int.Parse(groupSt.Key.ID),
                     Name = groupSt.Key.Name,
                     Description = groupSt.Key.Description,
                     ParentID = groupSt.Key.ParentID.Equals("0") ? null : (Nullable<int>)int.Parse(groupSt.Key.ParentID),
                     MetadataDefinitions = groupSt.Where(stm => !string.IsNullOrWhiteSpace(stm.MetadataKey))
                        .Select(stm => new SpecificationTypeMetadataDefinition {
                             ID = specificationMetaDefID++,
                             Key = stm.MetadataKey,
                             ValueType = stm.ValueType,
                             AllowOverride = bool.Parse(stm.AllowOverride),
                             DefaultValue = stm.DefaultValue,
                             InputTemplate = stm.InputTemplate,
                             Required = bool.Parse(stm.Required),
                             Localized = bool.Parse(stm.Localized)
                         }).SetCreatedByModifiedByList()
                 }).SetCreatedByModifiedByList();

            _context.SpecificationTypes.AddOrUpdate(specificationTypes);

            var artTypeMetaDefID = 1;
            var artifactTypes = typeMetadata
                .GroupBy(at => new { at.ID, at.Name, at.Description, at.ParentID })
                .Select(groupAt => new ArtifactType {
                     ID = int.Parse(groupAt.Key.ID),
                     Name = groupAt.Key.Name,
                     Description = groupAt.Key.Description,
                     ParentID = groupAt.Key.ParentID.Equals("0") ? null : (Nullable<int>)int.Parse(groupAt.Key.ParentID),
                     MetadataDefinitions = groupAt
                         .Where(atm => !string.IsNullOrWhiteSpace(atm.MetadataKey))
                         .Select(atm => new ArtifactTypeMetadataDefinition {
                             ID = artTypeMetaDefID++,
                             Key = atm.MetadataKey,
                             ValueType = atm.ValueType,
                             AllowOverride = bool.Parse(atm.AllowOverride),
                             DefaultValue = atm.DefaultValue,
                             InputTemplate = atm.InputTemplate,
                             Required = bool.Parse(atm.Required),
                             Localized = bool.Parse(atm.Localized)
                         }).SetCreatedByModifiedByList()
                 }).SetCreatedByModifiedByList();

            _context.ArtifactTypes.AddOrUpdate(artifactTypes);

            #endregion

            var artifactID = 1;
            var artifactMetadataID = 1;

            #region Feature Artifacts

            var fatureArtifacts = modelfeaturesSource
                .GroupBy(mf => new { mf.FeatureId, mf.FeatureName, mf.Price })
                .Select(groupF => new Artifact {
                    ID = artifactID++,
                    DisplayName = groupF.Key.FeatureName,
                    IsActive = true,
                    ArtifactSystemTypeID = artifactSystemTypes.Where(ast => ast.Name.Equals("item", StringComparison.OrdinalIgnoreCase)).First().ID,
                    ArtifactTypeID = artifactTypes.Where(at => at.Name.Equals("feature", StringComparison.OrdinalIgnoreCase)).First().ID,
                    MetaData = artifactTypes
                         .Where(at => at.Name.Equals("feature", StringComparison.OrdinalIgnoreCase))
                         .SelectMany(at => at.MetadataDefinitions)
                         .Where(atmd => atmd.Key.Equals("domain.mb.feature.msrp") || !string.IsNullOrWhiteSpace(atmd.DefaultValue))
                         .Select(atmd => new ArtifactMetadata { ID = artifactMetadataID++, Key = atmd.Key, Value = atmd.Key.Equals("domain.mb.feature.msrp") ? groupF.Key.Price : atmd.DefaultValue, MetadataDefinitionId = atmd.ID })
                         .SetCreatedByModifiedByList()
                }).SetOrder().SetCreatedByModifiedByList();

            _context.Artifacts.AddOrUpdate(fatureArtifacts);

            #endregion

            #region Create Gel and Upholstry color pallet Artifacts

            var colorPalletArtifacts = gelPallets
                .Concat(upholPallets)
                .GroupBy(cp => cp.PalletName)
                .Select(groupCp => new Artifact {
                    ID = artifactID++,
                    DisplayName = groupCp.Key,
                    IsActive = true,
                    ArtifactSystemTypeID = artifactSystemTypes.First(ast => ast.Name.Equals("group", StringComparison.OrdinalIgnoreCase)).ID,
                    ArtifactTypeID = artifactTypes.First(at => at.Name.Equals("color area", StringComparison.OrdinalIgnoreCase)).ID,
                    MetaData = artifactTypes
                        .Where(at => at.Name.Equals("color area", StringComparison.OrdinalIgnoreCase))
                        .SelectMany(at => at.MetadataDefinitions)
                        .Where(atmd => !string.IsNullOrWhiteSpace(atmd.DefaultValue))
                        .Select(atmd => new ArtifactMetadata { ID = artifactMetadataID++, Key = atmd.Key, MetadataDefinition = atmd, Value = atmd.DefaultValue, MetadataDefinitionId = atmd.ID }).SetCreatedByModifiedByList(),
                    SubItems = groupCp
                        .Join(gelColors, cp => cp.ColorID, c => c.ID, (cp, c) => new Artifact {
                            ID = artifactID++,
                            DisplayName = c.ColorName,
                            IsActive = true,
                            ArtifactSystemTypeID = artifactSystemTypes.Where(ast => ast.Name.Equals("item", StringComparison.OrdinalIgnoreCase)).First().ID,
                            ArtifactTypeID = artifactTypes.Where(at => at.Name.Equals("color", StringComparison.OrdinalIgnoreCase)).First().ID,
                            MetaData = artifactTypes
                                 .Where(at => at.Name.Equals("color", StringComparison.OrdinalIgnoreCase))
                                 .SelectMany(at => at.MetadataDefinitions)
                                 .Where(atmd => atmd.Key.Equals("domain.mb.color.rgb") || !string.IsNullOrWhiteSpace(atmd.DefaultValue))
                                 .Select(atmd => new ArtifactMetadata { ID = artifactMetadataID++, Key = atmd.Key, MetadataDefinition = atmd, Value = atmd.Key.Equals("domain.mb.color.rgb") ? c.HexColor : atmd.DefaultValue, MetadataDefinitionId = atmd.ID })
                                 .SetCreatedByModifiedByList(),
                        }).SetCreatedByModifiedByList().SetOrder().ToList(),
                }).SetCreatedByModifiedByList().SetOrder();

            _context.Artifacts.AddOrUpdate(colorPalletArtifacts.ToArray());

            #endregion

            var specID = 1;
            var specMetadataID = 1;
            var rootSpecifications = modelfeaturesSource
                 .GroupBy(m => m.Model)
                 .Select(groupM => new Specification {
                     ID = specID++,
                     DisplayName = groupM.Key,
                     IsActive = true,
                     SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("root", StringComparison.OrdinalIgnoreCase)).First().ID,
                     SpecificationTypeID = specificationTypes.Where(st => st.Name.Equals("model", StringComparison.OrdinalIgnoreCase)).First().ID,
                     MetaData = specificationTypes
                         .Where(st => st.Name.Equals("model", StringComparison.OrdinalIgnoreCase))
                         .SelectMany(st => st.MetadataDefinitions)
                         .Where(stmd => !string.IsNullOrWhiteSpace(stmd.DefaultValue))
                         .Select(stmd => new SpecificationMetadata { ID = specMetadataID++, Key = stmd.Key, Value = stmd.DefaultValue, MetadataDefinitionID = stmd.ID }).SetCreatedByModifiedByList(),
                     SubItems = new List<Specification> {
                         // Features
                         new Specification {
                             ID = specID++,
                             DisplayName = "Options",
                             SpecificationSystemTypeID = specificationSystemTypes.First(sst => sst.Name.Equals("multiple choice group", StringComparison.OrdinalIgnoreCase)).ID,
                             SubItems = groupM
                                .Select(mf => new Specification {
                                    ID = specID++,
                                    DisplayName = mf.FeatureName,
                                    IsActive = true,
                                    ArtifactReferenceID = fatureArtifacts.Where(af => af.DisplayName.Equals(mf.FeatureName)).First().ID.ToString(),
                                    SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("reference item", StringComparison.OrdinalIgnoreCase)).First().ID,
                                    SpecificationTypeID = specificationTypes.Where(st => st.Name.Equals("feature", StringComparison.OrdinalIgnoreCase)).First().ID,
                                    MetaData = specificationTypes
                                        .Where(st => st.Name.Equals("feature", StringComparison.OrdinalIgnoreCase))
                                        .SelectMany(st =>  st.MetadataDefinitions)
                                        .Where(stmd => stmd.Key.In("domain.mb.pricing.level.id", "domain.mb.pricing.level.price") || !string.IsNullOrWhiteSpace(stmd.DefaultValue))
                                        .Select(stmd => new SpecificationMetadata {
                                            ID = specMetadataID++,
                                            Key = stmd.Key,
                                            Value = stmd.Key.Equals("domain.mb.pricing.level.id") ? modelFeaturePricingSource.First(mfp => mfp.Model.Equals(groupM.Key) && mfp.FeatureId.Equals(mf.FeatureId)).PriceLevelId
                                                : stmd.Key.Equals("domain.mb.pricing.level.price") ? modelFeaturePricingSource.First(mfp => mfp.Model.Equals(groupM.Key) && mfp.FeatureId.Equals(mf.FeatureId)).Price
                                                : stmd.DefaultValue,
                                            MetadataDefinitionID = stmd.ID
                                        })
                                        .SetCreatedByModifiedByList()
                                }).SetOrder().SetCreatedByModifiedByList()
                         }
                        // ColorSection
                     }.Concat(modelColorAreasSource
                          .GroupBy(sec => new { sec.Model, sec.Section })
                          .Where(groupSec => groupSec.Key.Model.Equals(groupM.Key))
                          .Select(groupSec => new Specification {
                              ID = specID++,
                              DisplayName = groupSec.Key.Section,
                              IsActive = true,
                              SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("group", StringComparison.OrdinalIgnoreCase)).First().ID,
                              MetaData = specificationTypes
                                  .Where(st => st.Name.Equals("color area", StringComparison.OrdinalIgnoreCase))
                                  .SelectMany(st => st.MetadataDefinitions)
                                  .Where(stmd => !string.IsNullOrWhiteSpace(stmd.DefaultValue))
                                  .Select(stmd => new SpecificationMetadata { ID = specMetadataID++, Key = stmd.Key, Value = stmd.DefaultValue, MetadataDefinitionID = stmd.ID }).SetCreatedByModifiedByList(),
                              // ColorAreas
                              SubItems = groupSec
                                .GroupBy(ca => ca.Area)
                                .Select(groupCa => new Specification {
                                    ID = specID++,
                                    DisplayName = groupCa.Key,
                                    IsActive = true,
                                    SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("choice group", StringComparison.OrdinalIgnoreCase)).First().ID,
                                    SpecificationTypeID = specificationTypes.Where(st => st.Name.Equals("color area", StringComparison.OrdinalIgnoreCase)).First().ID,
                                    MetaData = specificationTypes
                                        .Where(st => st.Name.Equals("color area", StringComparison.OrdinalIgnoreCase))
                                        .SelectMany(st => st.MetadataDefinitions)
                                        .Where(stmd => !string.IsNullOrWhiteSpace(stmd.DefaultValue))
                                        .Select(stmd => new SpecificationMetadata { ID = specMetadataID++, Key = stmd.Key, Value = stmd.DefaultValue, MetadataDefinitionID = stmd.ID }).SetCreatedByModifiedByList(),
                                    // Colors
                                    SubItems = groupCa
                                        .GroupBy(cp => cp.Pallet)
                                        .Select(groupCp => new Specification {
                                            ID = specID++,
                                            DisplayName = groupCp.Key,
                                            IsActive = true,
                                            ArtifactReferenceID = colorPalletArtifacts.First(cpa => cpa.DisplayName.Equals(groupCp.Key)).ID.ToString(),
                                            SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("reference group", StringComparison.OrdinalIgnoreCase)).First().ID,
                                            SpecificationTypeID = specificationTypes.Where(st => st.Name.Equals("color pallet", StringComparison.OrdinalIgnoreCase)).First().ID,
                                            MetaData = specificationTypes
                                                .Where(st => st.Name.Equals("color pallet", StringComparison.OrdinalIgnoreCase))
                                                .SelectMany(st => st.MetadataDefinitions)
                                                .Where(stmd => !string.IsNullOrWhiteSpace(stmd.DefaultValue))
                                                .Select(stmd => new SpecificationMetadata { ID = specMetadataID++, Key = stmd.Key, Value = stmd.DefaultValue, MetadataDefinitionID = stmd.ID }).SetCreatedByModifiedByList(),
                                            SubItems = colorPalletArtifacts
                                                .Where(p => p.DisplayName.Equals(groupCp.Key))
                                                .SelectMany(cp => cp.SubItems)
                                                .Select(c =>
                                                    new Specification {
                                                        ID = specID++,
                                                        DisplayName = c.DisplayName,
                                                        IsActive = true,
                                                        ArtifactReferenceID = c.ID.ToString(),
                                                        SpecificationSystemTypeID = specificationSystemTypes.Where(sst => sst.Name.Equals("reference item", StringComparison.OrdinalIgnoreCase)).First().ID,
                                                        SpecificationTypeID = specificationTypes.Where(st => st.Name.Equals("color", StringComparison.OrdinalIgnoreCase)).First().ID,
                                                        MetaData = c.MetaData.Select(am => new SpecificationMetadata { ID = specMetadataID++, Key = am.Key, Value = am.Value, MetadataDefinitionID = am.MetadataDefinitionId }).SetCreatedByModifiedByList(),
                                                    }).SetOrder().SetCreatedByModifiedByList()
                                        }).SetOrder().SetCreatedByModifiedByList()
                                }).SetOrder().SetCreatedByModifiedByList()
                          })).SetOrder().SetCreatedByModifiedByList()
                 }).SetOrder().SetCreatedByModifiedByList();

            _context.Specifications.AddOrUpdate(rootSpecifications);

            try {
                _context.SaveChanges();
            }
            catch(Exception ex) {
                throw ex;
            }
        }
        
        public static void Seed(ICustomWiseContext context) {
            var instance = new MigrationSeedConfig(context);
            instance.Seed();
        }

    }
}