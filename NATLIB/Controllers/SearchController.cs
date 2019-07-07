using NATLIB.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace NATLIB.Controllers
{
    public class SearchController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ArrayList GetSearchResults(string q, string searchBy)
        {
            ArrayList searchArray = new ArrayList();

            if (searchBy == "Author")
            {
                List<Book> book = db.Books.Where(b => b.Author == q && b.Availability == "Public").ToList();
                List<Magazine> magazine = db.Magazines.Where(m => m.Publisher == q && m.Availability == "Public").ToList();
                List<Newspaper> newspaper = db.Newspapers.Where(n => n.Publisher == q && n.Availability == "Public").ToList();
                List<GovernmentPublication> govPub = db.GovernmentPublications.Where(g => g.Publisher == q && g.Availability == "Public").ToList();

                if (User.IsInRole("Local User") || User.IsInRole("Administrator") || User.IsInRole("Librarian"))
                {
                    List<OlaLeafManuscript> olaLeaf = db.OlaLeafManuscripts.Where(o => o.Author == q && o.Availability == "Public").ToList();
                    searchArray.Add(olaLeaf);
                }

                searchArray.Add(book);
                searchArray.Add(magazine);
                searchArray.Add(newspaper);
                searchArray.Add(govPub);
            }

            if (searchBy == "Title")
            {
                List<Book> book = db.Books.Where(b => b.Title == q && b.Availability == "Public").ToList();
                List<Magazine> magazine = db.Magazines.Where(m => m.Name == q && m.Availability == "Public").ToList();
                List<Newspaper> newspaper = db.Newspapers.Where(n => n.Name == q && n.Availability == "Public").ToList();
                List<GovernmentPublication> govPub = db.GovernmentPublications.Where(g => g.Title == q && g.Availability == "Public").ToList();

                if (User.IsInRole("Local User") || User.IsInRole("Administrator") || User.IsInRole("Librarian"))
                {
                    List<OlaLeafManuscript> olaLeaf = db.OlaLeafManuscripts.Where(o => o.Title == q && o.Availability == "Public").ToList();
                    searchArray.Add(olaLeaf);
                }

                searchArray.Add(book);
                searchArray.Add(magazine);
                searchArray.Add(newspaper);
                searchArray.Add(govPub);
            }

            if (searchBy == "Year")
            {
                try
                {
                    List<Book> book = db.Books.Where(b => b.DatePublished.Value.Year == Convert.ToDateTime(q).Year && b.Availability == "Public").ToList();
                    List<Magazine> magazine = db.Magazines.Where(m => m.CoverDate.Value.Year == Convert.ToDateTime(q).Year && m.Availability == "Public").ToList();
                    List<Newspaper> newspaper = db.Newspapers.Where(n => n.CoverDate.Value.Year == Convert.ToDateTime(q).Year && n.Availability == "Public").ToList();
                    List<GovernmentPublication> govPub = db.GovernmentPublications.Where(g => g.DatePublished.Value.Year == Convert.ToDateTime(q).Year && g.Availability == "Public").ToList();
                    //List<OlaLeafManuscript> olaLeaf = db.OlaLeafManuscripts.Where(o => o.Availability.Value.Year == Convert.ToDateTime(q).Year).ToList();

                    searchArray.Add(book);
                    searchArray.Add(magazine);
                    searchArray.Add(newspaper);
                    searchArray.Add(govPub);
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            if (searchBy == "Category")
            {
                List<Book> book = db.Books.Where(b => b.Genre == q).ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m. == q && m.Availability == "Public").ToList();
                //List<Newspaper> newspaper = db.Newspapers.Where(n => n.Publisher == q && n.Availability == "Public").ToList();
                //List<GovernmentPublication> govPub = db.GovernmentPublications.Where(g => g.Publisher == q && g.Availability == "Public").ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m. == q).ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m.Publisher == q).ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m.Publisher == q).ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m.Publisher == q).ToList();
                //List<Magazine> magazine = db.Magazines.Where(m => m.Publisher == q).ToList();
            }
            
            return searchArray;
        }
    }
}
