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
    [Authorize]
    public class NewspapersController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Newspapers
        public IQueryable<Newspaper> GetNewspapers()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                return db.Newspapers;
            }

            return db.Newspapers.Where(b => b.Availability == "Public");
        }

        // GET: api/Newspapers/5
        [ResponseType(typeof(Newspaper))]
        public IHttpActionResult GetNewspaper(int id)
        {
            Newspaper newspaper = new Newspaper();

            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                newspaper = db.Newspapers.Find(id);
                if (newspaper == null)
                {
                    return NotFound();
                }

                return Ok(newspaper);
            }

            newspaper = db.Newspapers.Where(b => b.Availability == "Public" && b.Id == id).FirstOrDefault();
            if (newspaper == null)
            {
                return NotFound();
            }

            return Ok(newspaper);
        }

        // PUT: api/Newspapers/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewspaper(int id, Newspaper newspaper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newspaper.Id)
            {
                return BadRequest();
            }

            // Check if the user is allowed to change items' availabilities.
            if ((!User.IsInRole("Administrator"))
                && (newspaper.Availability != db.Newspapers.AsNoTracking().Where(o => o.Id == id).Select(o => o.Availability).Single()))
            {
                return BadRequest("You don't have permission to change the item's availability.");
            }

            db.Entry(newspaper).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewspaperExists(id))
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

        // POST: api/Newspapers
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Newspaper))]
        public IHttpActionResult PostNewspaper(Newspaper newspaper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Newspapers.Add(newspaper);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newspaper.Id }, newspaper);
        }

        // DELETE: api/Newspapers/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Newspaper))]
        public IHttpActionResult DeleteNewspaper(int id)
        {
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return NotFound();
            }

            db.Newspapers.Remove(newspaper);
            db.SaveChanges();

            return Ok(newspaper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewspaperExists(int id)
        {
            return db.Newspapers.Count(e => e.Id == id) > 0;
        }
    }
}