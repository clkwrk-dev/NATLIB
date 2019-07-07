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
    public class MagazinesController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Magazines
        public IQueryable<Magazine> GetMagazines()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                return db.Magazines;
            }

            return db.Magazines.Where(b => b.Availability == "Public");
        }

        // GET: api/Magazines/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult GetMagazine(int id)
        {
            Magazine magazine = new Magazine();

            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                magazine = db.Magazines.Find(id);
                if (magazine == null)
                {
                    return NotFound();
                }

                return Ok(magazine);
            }

            magazine = db.Magazines.Where(b => b.Availability == "Public" && b.Id == id).FirstOrDefault();
            if (magazine == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }

        // PUT: api/Magazines/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMagazine(int id, Magazine magazine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != magazine.Id)
            {
                return BadRequest();
            }

            // Check if the user is allowed to change items' availabilities.
            if ((!User.IsInRole("Administrator"))
                && (magazine.Availability != db.Magazines.AsNoTracking().Where(o => o.Id == id).Select(o => o.Availability).Single()))
            {
                return BadRequest("You don't have permission to change the item's availability.");
            }

            db.Entry(magazine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagazineExists(id))
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

        // POST: api/Magazines
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult PostMagazine(Magazine magazine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Magazines.Add(magazine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = magazine.Id }, magazine);
        }

        // DELETE: api/Magazines/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult DeleteMagazine(int id)
        {
            Magazine magazine = db.Magazines.Find(id);
            if (magazine == null)
            {
                return NotFound();
            }

            db.Magazines.Remove(magazine);
            db.SaveChanges();

            return Ok(magazine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MagazineExists(int id)
        {
            return db.Magazines.Count(e => e.Id == id) > 0;
        }
    }
}