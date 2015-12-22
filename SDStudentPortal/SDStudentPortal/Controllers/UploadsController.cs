using Microsoft.AspNet.Identity;
using SDStudentPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SDStudentPortal.Controllers
{
    public class UploadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uploads
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            var uploads = db.Uploads.Where(u => u.UserId==uid);
            return View(uploads.ToList());
        }

        // GET: Uploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uploads uploads = db.Uploads.Find(id);
            if (uploads == null)
            {
                return HttpNotFound();
            }
            return View(uploads);
        }

        // GET: Uploads/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadsID,Title,Description,UploadFileUrl,UploadFileUrlPrivacySetting")] Uploads uploads, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                string fileURL = Server.MapPath("~/FileURL/" + filename);
                file.SaveAs(fileURL);
                uploads.UploadFileUrl = "FileURL/" + filename;
                uploads.DateCreated = DateTime.Now;
                db.Uploads.Add(uploads);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // GET: Uploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uploads uploads = db.Uploads.Find(id);
            if (uploads == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UploadsID,Title,Description,DateCreated,UploadFileUrl,UploadFileUrlPrivacySetting")] Uploads uploads, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                string fileURL = Server.MapPath("~/FileURL/" + filename);
                file.SaveAs(fileURL);
                uploads.UploadFileUrl = "FileURL/" + filename;

                var uid = User.Identity.GetUserId();
                uploads.UserId = uid;
                db.Entry(uploads).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // GET: Uploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uploads uploads = db.Uploads.Find(id);
            if (uploads == null)
            {
                return HttpNotFound();
            }
            return View(uploads);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uploads uploads = db.Uploads.Find(id);
            db.Uploads.Remove(uploads);
            db.SaveChanges();
            return RedirectToAction("Index");
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
