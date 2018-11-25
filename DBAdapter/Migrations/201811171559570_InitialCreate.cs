namespace KMA.APZRPMJ2018.RequestSimulator.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Path = c.String(nullable: false),
                        NumberOfChars = c.Long(nullable: false),
                        NumberOfWords = c.Long(nullable: false),
                        NumberOfStrings = c.Long(nullable: false),
                        DateRequest = c.DateTime(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserGuid", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "UserGuid" });
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
        }
    }
}
