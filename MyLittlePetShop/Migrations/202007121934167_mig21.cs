namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig21 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ShoppingItems", "CategoryId");
            AddForeignKey("dbo.ShoppingItems", "CategoryId", "dbo.ShoppingCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingItems", "CategoryId", "dbo.ShoppingCategories");
            DropIndex("dbo.ShoppingItems", new[] { "CategoryId" });
        }
    }
}
