using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrystalVortex.Models;

namespace CrystalVortex.Views.Releases
{
    public class ReleasesApiController : ApiController
    {
        private ReleaseModel db = new ReleaseModel();

        // GET: api/ReleasesApi
        public IQueryable<Release> GetReleases()
        {
            return db.Releases;
        }

        // GET: api/ReleasesApi/5
        [ResponseType(typeof(Release))]
        public async Task<IHttpActionResult> GetRelease(int id)
        {
            Release release = await db.Releases.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }

            return Ok(release);
        }

        // PUT: api/ReleasesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRelease(int id, Release release)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != release.Id)
            {
                return BadRequest();
            }

            db.Entry(release).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleaseExists(id))
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

        // POST: api/ReleasesApi
        [ResponseType(typeof(Release))]
        public async Task<IHttpActionResult> PostRelease(Release release)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Releases.Add(release);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = release.Id }, release);
        }

        // DELETE: api/ReleasesApi/5
        [ResponseType(typeof(Release))]
        public async Task<IHttpActionResult> DeleteRelease(int id)
        {
            Release release = await db.Releases.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }

            db.Releases.Remove(release);
            await db.SaveChangesAsync();

            return Ok(release);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReleaseExists(int id)
        {
            return db.Releases.Count(e => e.Id == id) > 0;
        }
    }
}