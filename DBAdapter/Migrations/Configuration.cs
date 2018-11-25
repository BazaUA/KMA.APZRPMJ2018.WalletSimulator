using System.Data.Entity.Migrations;

namespace KMA.APZRPMJ2018.RequestSimulator.DBAdapter.Migrations
{
    internal sealed class
        Configuration : DbMigrationsConfiguration<RequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RequestContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}