namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Number = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonDetails");
        }
    }
}
