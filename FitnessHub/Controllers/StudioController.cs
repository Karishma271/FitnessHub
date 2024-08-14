using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessHub.Models;

namespace FitnessHub.Controllers
{
    public class StudioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string imagesFolder = "~/Images/Studios/";

        // GET: Studio
        public ActionResult Index()
        {
            var studio = db.Studios.ToList();
            return View(studio);
        }

        // GET: Studio/Details/5
        public ActionResult Details(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // GET: Studio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Studio studio, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath(imagesFolder), fileName);
                    imageFile.SaveAs(filePath);
                    studio.ImageUrl = Path.Combine(imagesFolder, fileName);
                }
                else
                {
                    studio.ImageUrl = "/Images/Studios/default.png"; // Default image if none provided
                }

                db.Studios.Add(studio);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(studio);
        }

        // GET: Studio/Edit/5
        public ActionResult Edit(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // POST: Studio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Studio studio, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath(imagesFolder), fileName);
                    imageFile.SaveAs(filePath);
                    studio.ImageUrl = Path.Combine(imagesFolder, fileName);
                }

                db.Entry(studio).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(studio);
        }

        // GET: Studio/Delete/5
        public ActionResult Delete(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio == null)
            {
                return HttpNotFound();
            }
            return View(studio);
        }

        // POST: Studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var studio = db.Studios.Find(id);
            if (studio != null)
            {
                // Delete the image file if exists
                var imagePath = Server.MapPath(studio.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                db.Studios.Remove(studio);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
