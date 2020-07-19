namespace MyLittlePetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCategories", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCategories", "Image");
        }
    }
}
