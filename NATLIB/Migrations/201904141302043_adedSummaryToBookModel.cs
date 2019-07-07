namespace NATLIB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adedSummaryToBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Summary");
        }
    }
}
