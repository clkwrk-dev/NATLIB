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
    public class GovernmentPublicationsController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GovernmentPublications
        public IQueryable<GovernmentPublication> GetGovernmentPublications()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                return db.GovernmentPublications;
            }

            return db.GovernmentPublications.Where(b => b.Availability == "Public");
        }

        // GET: api/GovernmentPublications/5
        [ResponseType(typeof(GovernmentPublication))]
        public IHttpActionResult GetGovernmentPublication(int id)
        {
            GovernmentPublication governmentPublication = new GovernmentPublication();

            if (User.IsInRole("Administrator") || User.IsInRole("Librarian") || User.IsInRole("Local User"))
            {
                governmentPublication = db.GovernmentPublications.Find(id);
                if (governmentPublication == null)
                {
                    return NotFound();
                }

                return Ok(governmentPublication);
            }

            governmentPublication = db.GovernmentPublications.Where(b => b.Availability == "Public" && b.Id == id).FirstOrDefault();
            if (governmentPublication == null)
            {
                return NotFound();
            }

            return Ok(governmentPublication);
        }

        // PUT: api/GovernmentPublications/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGovernmentPublication(int id, GovernmentPublication governmentPublication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != governmentPublication.Id)
            {
                return BadRequest();
            }

            // Check if the user is allowed to change items' availabilities.
            if ((!User.IsInRole("Administrator"))
                && (governmentPublication.Availability != db.GovernmentPublications.AsNoTracking().Where(o => o.Id == id).Select(o => o.Availability).Single()))
            {
                return BadRequest("You don't have permission to change the item's availability.");
            }

            db.Entry(governmentPublication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GovernmentPublicationExists(id))
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

        // POST: api/GovernmentPublications
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(GovernmentPublication))]
        public IHttpActionResult PostGovernmentPublication(GovernmentPublication governmentPublication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GovernmentPublications.Add(governmentPublication);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = governmentPublication.Id }, governmentPublication);
        }

        // DELETE: api/GovernmentPublications/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(GovernmentPublication))]
        public IHttpActionResult DeleteGovernmentPublication(int id)
        {
            GovernmentPublication governmentPublication = db.GovernmentPublications.Find(id);
            if (governmentPublication == null)
            {
                return NotFound();
            }

            db.GovernmentPublications.Remove(governmentPublication);
            db.SaveChanges();

            return Ok(governmentPublication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GovernmentPublicationExists(int id)
        {
            return db.GovernmentPublications.Count(e => e.Id == id) > 0;
        }
    }
}