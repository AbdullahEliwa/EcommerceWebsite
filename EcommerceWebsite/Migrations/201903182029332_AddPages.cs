namespace EcommerceWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false),
                        Slug = c.String(maxLength: 50),
                        Sorting = c.Byte(nullable: false),
                        HasSideBar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pages");
        }
    }
}
