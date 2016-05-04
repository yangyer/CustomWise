namespace CustomWise.Data {
    using System;
    using System.Data.Entity.Migrations;

    public class CustomWiseMigration {
        private CustomWiseModel _context;
        public CustomWiseMigration(CustomWiseModel context) {
            _context = context;
        }

        public void RegisterMigration(Action<ICustomWiseMigrationContext> customMigration) {
            customMigration(_context);
            _context.SaveChanges();
        }

        public static CustomWiseMigration Create() {
            return new CustomWiseMigration(new CustomWiseModel());
        }
    }
}
