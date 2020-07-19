namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubmitedProducts",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.ShoppingItems", "SubmitedProducts_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingItems", "SubmitedProducts_UserId");
            AddForeignKey("dbo.ShoppingItems", "SubmitedProducts_UserId", "dbo.SubmitedProducts", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingItems", "SubmitedProducts_UserId", "dbo.SubmitedProducts");
            DropIndex("dbo.ShoppingItems", new[] { "SubmitedProducts_UserId" });
            DropColumn("dbo.ShoppingItems", "SubmitedProducts_UserId");
            DropTable("dbo.SubmitedProducts");
        }
    }
}
