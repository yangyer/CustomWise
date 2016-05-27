using System.Configuration;

namespace CustomWise.Web.Services.Configuration {
    public class CustomWiseSection : ConfigurationSection {
        [ConfigurationProperty("enableMigration", DefaultValue = false, IsRequired = false)]
        public bool EnableMigration { get; set; }

        [ConfigurationProperty("migrationSetting")]
        public MigrationSettingElement MigrationSetting {
            get {
                return (MigrationSettingElement)this["migrationSetting"] ?? new MigrationSettingElement();
            }
        }
    }
}
