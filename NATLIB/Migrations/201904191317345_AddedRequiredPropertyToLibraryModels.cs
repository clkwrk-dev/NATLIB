namespace NATLIB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredPropertyToLibraryModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "DatePublished", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Books", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Availability", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.GovernmentPublications", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.GovernmentPublications", "DatePublished", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GovernmentPublications", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.GovernmentPublications", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.GovernmentPublications", "Availability", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "Creator", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "CoverDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Magazines", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "Availability", c => c.String(nullable: false));
            AlterColumn("dbo.Newspapers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Newspapers", "Creator", c => c.String(nullable: false));
            AlterColumn("dbo.Newspapers", "CoverDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Newspapers", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.Newspapers", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Newspapers", "Availability", c => c.String(nullable: false));
            AlterColumn("dbo.OlaLeafManuscripts", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.OlaLeafManuscripts", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.OlaLeafManuscripts", "Availability", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OlaLeafManuscripts", "Availability", c => c.String());
            AlterColumn("dbo.OlaLeafManuscripts", "Author", c => c.String());
            AlterColumn("dbo.OlaLeafManuscripts", "Title", c => c.String());
            AlterColumn("dbo.Newspapers", "Availability", c => c.String());
            AlterColumn("dbo.Newspapers", "Image", c => c.String());
            AlterColumn("dbo.Newspapers", "Publisher", c => c.String());
            AlterColumn("dbo.Newspapers", "CoverDate", c => c.DateTime());
            AlterColumn("dbo.Newspapers", "Creator", c => c.String());
            AlterColumn("dbo.Newspapers", "Name", c => c.String());
            AlterColumn("dbo.Magazines", "Availability", c => c.String());
            AlterColumn("dbo.Magazines", "Image", c => c.String());
            AlterColumn("dbo.Magazines", "Publisher", c => c.String());
            AlterColumn("dbo.Magazines", "CoverDate", c => c.DateTime());
            AlterColumn("dbo.Magazines", "Creator", c => c.String());
            AlterColumn("dbo.Magazines", "Name", c => c.String());
            AlterColumn("dbo.GovernmentPublications", "Availability", c => c.String());
            AlterColumn("dbo.GovernmentPublications", "Image", c => c.String());
            AlterColumn("dbo.GovernmentPublications", "Publisher", c => c.String());
            AlterColumn("dbo.GovernmentPublications", "DatePublished", c => c.DateTime());
            AlterColumn("dbo.GovernmentPublications", "Title", c => c.String());
            AlterColumn("dbo.Books", "Summary", c => c.String());
            AlterColumn("dbo.Books", "Availability", c => c.String());
            AlterColumn("dbo.Books", "Image", c => c.String());
            AlterColumn("dbo.Books", "Publisher", c => c.String());
            AlterColumn("dbo.Books", "Language", c => c.String());
            AlterColumn("dbo.Books", "ISBN", c => c.String());
            AlterColumn("dbo.Books", "Genre", c => c.String());
            AlterColumn("dbo.Books", "DatePublished", c => c.DateTime());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
