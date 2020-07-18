namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.ShoppingItems", "ShoppingCartItems_UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingItems", "ShoppingCartItems_UserID");
            AddForeignKey("dbo.ShoppingItems", "ShoppingCartItems_UserID", "dbo.ShoppingCartItems", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingItems", "ShoppingCartItems_UserID", "dbo.ShoppingCartItems");
            DropIndex("dbo.ShoppingItems", new[] { "ShoppingCartItems_UserID" });
            DropColumn("dbo.ShoppingItems", "ShoppingCartItems_UserID");
            DropTable("dbo.ShoppingCartItems");
        }
    }
}
