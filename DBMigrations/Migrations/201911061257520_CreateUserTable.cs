namespace DBMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PasswordHash = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StudentNumber = c.Int(nullable: false),
                        PCN = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastSeen = c.DateTime(nullable: false),
                        SessionExpiresAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
