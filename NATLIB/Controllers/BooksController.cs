using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NATLIB.Models;

namespace NATLIB.Controllers
{
    public class BooksController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Books
        // This method does not need authorization, thus anyone can see
        // the books that are categorized as public.
        public IQueryable<Book> GetBooks()
        {
            // If the logged in user is a local user/administrator/librarian, display all the books
            // including the ones categorized as rare.
            if(User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                return db.Books;
            }

            return db.Books.Where(b => b.Availability == "Public");
        }

        // GET: api/Books/5
        // This method does not need authorization, therefore anyone can access
        // the books that are categorized as public.
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = new Book();

            // If the logged in user is a local user/administrator/librarian, grant access to all
            // the books.
            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                book = db.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }

            book = db.Books.Where(b => b.Availability == "Public" && b.Id == id).FirstOrDefault();
            if(book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            // Check if the user is allowed to change items' availabilities.
            if((!User.IsInRole("Administrator")) 
                && (book.Availability != db.Books.AsNoTracking().Where(o => o.Id == id).Select(o => o.Availability).Single()))
            {
                return BadRequest("You don't have permission to change the item's availability.");
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}