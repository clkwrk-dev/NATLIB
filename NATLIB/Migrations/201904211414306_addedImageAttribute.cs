namespace NATLIB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImageAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OlaLeafManuscripts", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OlaLeafManuscripts", "Image");
        }
    }
}
