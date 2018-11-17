using System.Data.Entity;
using KMA.APZRPMJ2018.RequestSimulator.Migrations;
using KMA.APZRPMJ2018.RequestSimulator.Models;

namespace KMA.APZRPMJ2018.RequestSimulator.Data
{
    public class RequestContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        
        public RequestContext()
            :base("RequestDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RequestContext, Configuration>(true));
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Request.RequestEntityConfiguration());
        }

        

    }
}