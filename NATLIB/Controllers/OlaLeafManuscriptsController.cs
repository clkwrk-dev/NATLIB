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
    [Authorize(Roles = "Administrator, Librarian, Local User")]
    public class OlaLeafManuscriptsController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OlaLeafManuscripts
        public IQueryable<OlaLeafManuscript> GetOlaLeafManuscripts()
        {
            return db.OlaLeafManuscripts;
        }

        // GET: api/OlaLeafManuscripts/5
        [ResponseType(typeof(OlaLeafManuscript))]
        public IHttpActionResult GetOlaLeafManuscript(int id)
        {
            OlaLeafManuscript olaLeafManuscript = db.OlaLeafManuscripts.Find(id);
            if (olaLeafManuscript == null)
            {
                return NotFound();
            }

            return Ok(olaLeafManuscript);
        }

        // PUT: api/OlaLeafManuscripts/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOlaLeafManuscript(int id, OlaLeafManuscript olaLeafManuscript)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != olaLeafManuscript.Id)
            {
                return BadRequest();
            }

            // Check if the user is allowed to change items' availabilities.
            if ((!User.IsInRole("Administrator"))
                && (olaLeafManuscript.Availability != db.OlaLeafManuscripts.AsNoTracking().Where(o => o.Id == id).Select(o => o.Availability).Single()))
            {
                return BadRequest("You don't have permission to change the item's availability.");
            }

            db.Entry(olaLeafManuscript).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OlaLeafManuscriptExists(id))
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

        // POST: api/OlaLeafManuscripts
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(OlaLeafManuscript))]
        public IHttpActionResult PostOlaLeafManuscript(OlaLeafManuscript olaLeafManuscript)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OlaLeafManuscripts.Add(olaLeafManuscript);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = olaLeafManuscript.Id }, olaLeafManuscript);
        }

        // DELETE: api/OlaLeafManuscripts/5
        [Authorize(Roles = "Administrator, Librarian")]
        [ResponseType(typeof(OlaLeafManuscript))]
        public IHttpActionResult DeleteOlaLeafManuscript(int id)
        {
            OlaLeafManuscript olaLeafManuscript = db.OlaLeafManuscripts.Find(id);
            if (olaLeafManuscript == null)
            {
                return NotFound();
            }

            db.OlaLeafManuscripts.Remove(olaLeafManuscript);
            db.SaveChanges();

            return Ok(olaLeafManuscript);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OlaLeafManuscriptExists(int id)
        {
            return db.OlaLeafManuscripts.Count(e => e.Id == id) > 0;
        }
    }
}