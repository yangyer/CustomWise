using CustomWise.Data;
using CustomWise.Web.Services.Configuration;
using Sophcon.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using DtoEntities = CustomWise.Web.Services.Models;

namespace CustomWise.Web.Services {
    public class CustomWiseMigration {
        static readonly string _sectionName = "customWise";
        CustomWiseSection _customWiseSection;
        Lazy<ICustomWiseContext> _contextLazy;

        public CustomWiseMigration(ICustomWiseContext context) : this(() => context) { }

        public CustomWiseMigration(Func<ICustomWiseContext> contextFactory) {
            _contextLazy = new Lazy<ICustomWiseContext>(contextFactory);
            _Init();
        }

        internal void _Init() {
            _customWiseSection = ConfigurationManager.GetSection(CustomWiseMigration._sectionName) as CustomWiseSection;
            if (_customWiseSection == null) {
                throw new ConfigurationErrorsException($"Section { _sectionName } was not found.");
            }
        }

        internal void _SeedData(Action<ICustomWiseContext> seedCallBack) {
            if (!this._customWiseSection.EnableMigration) {
                return;
            }

            var migrationConfig = new CustomWise.Data.Migrations.Configuration();
            migrationConfig.AutomaticMigrationsEnabled = this._customWiseSection.MigrationSetting.AutomaticMigrationsEnabled;
            migrationConfig.AutomaticMigrationDataLossAllowed = this._customWiseSection.MigrationSetting.AutomaticMigrationDataLossAllowed;
            var customWiseMigrator = new DbMigrator(migrationConfig);

            customWiseMigrator.Update();

            seedCallBack?.Invoke(_contextLazy.Value);
            // Expose Repositories reathere then the conext here to the .Web application
            // repositories will have the business logic to perform checks.
        }

        public static void SeedData(Action<ICustomWiseContext> seedCallBack) {
            using(var ctx = new CustomWiseModel()) {
                new CustomWiseMigration(ctx)._SeedData(seedCallBack);
            }
        }
    }
}
