namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BuyerInfoes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyerInfoes", "ShoppingItem_Id", "dbo.ShoppingItems");
            DropIndex("dbo.BuyerInfoes", new[] { "User_Id" });
            DropIndex("dbo.BuyerInfoes", new[] { "ShoppingItem_Id" });
            DropTable("dbo.BuyerInfoes");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.BuyerInfoes", "ShoppingItem_Id");
            CreateIndex("dbo.BuyerInfoes", "User_Id");
            AddForeignKey("dbo.BuyerInfoes", "ShoppingItem_Id", "dbo.ShoppingItems", "Id");
            AddForeignKey("dbo.BuyerInfoes", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
