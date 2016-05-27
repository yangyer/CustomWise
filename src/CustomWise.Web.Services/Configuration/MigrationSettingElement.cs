using System.Configuration;

namespace CustomWise.Web.Services.Configuration {
    public class MigrationSettingElement : ConfigurationElement {
        [ConfigurationProperty("automaticMigrationsEnabled", DefaultValue = false, IsRequired = false)]
        public bool AutomaticMigrationsEnabled {
            get {
                return (bool)this["automaticMigrationsEnabled"];
            }
        }

        [ConfigurationProperty("automaticMigrationDataLossAllowed", DefaultValue = false, IsRequired = false)]
        public bool AutomaticMigrationDataLossAllowed {
            get {
                return (bool)this["automaticMigrationDataLossAllowed"];
            }
        }
    }
}
