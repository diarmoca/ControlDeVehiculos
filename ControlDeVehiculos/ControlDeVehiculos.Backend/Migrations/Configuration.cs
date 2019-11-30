namespace ControlDeVehiculos.Backend.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ControlDeVehiculos.Backend.Models.LocalDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ControlDeVehiculos.Backend.Models.LocalDataContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ControlDeVehiculos.Backend.Models.LocalDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
