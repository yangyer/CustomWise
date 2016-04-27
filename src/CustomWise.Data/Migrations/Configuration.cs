namespace CustomWise.Data.Migrations {
    using Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomWise.Data.CustomWiseModel> {
        private readonly string _system = "system";
        private readonly DateTime _now = DateTime.Now;

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

            context.RecordTypes.AddOrUpdate(new SpecificationType[] {
                new SpecificationType { Id = 1, DisplayName = "Root",              SystemName = "root",         CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now },
                new SpecificationType { Id = 2, DisplayName = "Group",             SystemName = "group",        CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now },
                new SpecificationType { Id = 3, DisplayName = "Item",              SystemName = "item",         CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now },
                new SpecificationType { Id = 4, DisplayName = "Reference Item",    SystemName = "ref_item",     CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now }
            });

            context.MetaDataTypes.AddOrUpdate(new MetaDataType[] {
                new MetaDataType { Id = 1, Name = "Color",      CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now },
                new MetaDataType { Id = 1, Name = "Image",      CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now },
                new MetaDataType { Id = 1, Name = "Pricing",    CreatedBy = _system, CreatedDate = _now, ModifiedBy = _system, ModifiedDate = _now }
            });
        }
    }
}
