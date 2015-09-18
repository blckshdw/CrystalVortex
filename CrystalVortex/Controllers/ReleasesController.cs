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
using System.IO;

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
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string ReleaseCode)
        {
            Release release = await db.Releases.Include(r => r.Tracks).FirstOrDefaultAsync(r => r.ReleaseCode == ReleaseCode);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }
        // GET: Releases/Create
        [Route("Releases/Create")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Releases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Releases/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,ReleaseCode,Name,Description,ReleaseDate,AlbumArt,TorrentURL,TorrentMD5")] Release release)
        {
            //Lookup
            var releaseExisting = await db.Releases.Where(r => r.ReleaseCode.ToUpper() == release.ReleaseCode.ToUpper()).FirstOrDefaultAsync();
            if (releaseExisting != null)
            {
                ModelState.AddModelError("NonUniqueReleaseCode", "Release Code Already Exists!");
            }

            if (ModelState.IsValid)
            {
                release.ReleaseDate = DateTime.Now.Date;

                db.Releases.Add(release);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Releases",new { @ReleaseCode = release.ReleaseCode });
            }

            return View(release);
        }
        [Route("Releases/Edit/{ReleaseCode}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string releaseCode)
        {
            Release release = await db.Releases.Include(r => r.Tracks).FirstOrDefaultAsync(r => r.ReleaseCode == releaseCode);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("Releases/Edit/{ReleaseCode}")]
        public async Task<ActionResult> Edit(Release release)
        {
            if (ModelState.IsValid)
            {
                db.Entry(release).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return View(release);
        }

        [Route("Releases/SaveUploadedFile")]
        public ActionResult SaveUploadedFile()
        {
            string releaseCode = Request["ReleaseCode"];

            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var idx = file.FileName.LastIndexOf('.');
                        var extn = file.FileName.Substring(idx+1,file.FileName.Length - idx-1);

                        var filePath = new DirectoryInfo(Server.MapPath("~/Images/AlbumArt/"));
                        var filePathName = filePath + "\\" + releaseCode + "-Artwork."+ extn;
                        file.SaveAs(filePathName);

                        var fileUri = filePathName.Replace(filePath.FullName, "~/Images/AlbumArt/");
                        fName = VirtualPathUtility.ToAbsolute(fileUri);

                        /*
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        var fileName1 = Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                        */

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
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
