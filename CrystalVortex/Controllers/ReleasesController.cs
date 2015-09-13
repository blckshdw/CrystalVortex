using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalVortex.Models;

namespace CrystalVortex.Controllers
{
    public class ReleasesController : Controller
    {
        private ReleaseModel db = new ReleaseModel();

        // GET: Releases
        public async Task<ActionResult> Index()
        {
            return View(await db.Releases.OrderByDescending(e => e.ReleaseDate).ToListAsync());
        }

        [Route("Releases/{ReleaseCode}")]
        public async Task<ActionResult> Details(string ReleaseCode)
        {
            Release release = await db.Releases.Include(r => r.Tracks).FirstOrDefaultAsync(r => r.ReleaseCode == ReleaseCode);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
