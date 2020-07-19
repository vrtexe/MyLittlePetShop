namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyerInfoes",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        ShoppingItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.ShoppingItems", t => t.ShoppingItem_Id)
                .Index(t => t.User_Id)
                .Index(t => t.ShoppingItem_Id);
            
            DropColumn("dbo.ShoppingCartItems", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCartItems", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.BuyerInfoes", "ShoppingItem_Id", "dbo.ShoppingItems");
            DropForeignKey("dbo.BuyerInfoes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BuyerInfoes", new[] { "ShoppingItem_Id" });
            DropIndex("dbo.BuyerInfoes", new[] { "User_Id" });
            DropTable("dbo.BuyerInfoes");
        }
    }
}
