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
    [Authorize]
    public class UploadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uploads

        public ActionResult Index(string searchString)
        {
            var uid = User.Identity.GetUserId();
            var uploads = db.Uploads.Where(u => u.UserId == uid);
                        
            var searchUploads = from u in db.Uploads
                         select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchUploads = searchUploads.Where(u => u.Title.Contains(searchString) || u.Description.Contains(searchString) && (u.UploadFileUrlPrivacySetting.Equals("Public") || u.UploadFileUrlPrivacySetting.Equals("UserOnly")));
                
                //need to add "UserOnly" criteria only when someone is logged in
                return View(searchUploads.ToList());
            }
            else
            {
                return View(db.Uploads.Where(u => u.UserId == uid));
            }
            
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
        public ActionResult Create(int? ProjectID, string returnUrl)
        {
            var uid = User.Identity.GetUserId();
                        
            List<Project> pList = db.project.Where(p => p.UserId == uid).ToList();

            pList.Insert(0, db.project.Find(1));

            ViewBag.ProjectList = new SelectList(pList, "ProjectID", "Title", ProjectID ?? 1);
            ViewBag.returnUrl = returnUrl;
            return View();            
        }
        
        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadsID,Title,Description,UploadFileUrl,UploadFileUrlPrivacySetting,FileType,ProjectID")] Uploads uploads, HttpPostedFileBase file, string returnUrl)
        {
            if (ModelState.IsValid && file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                string fileURL = Server.MapPath("~/FileURL/" + filename);
                file.SaveAs(fileURL);
                uploads.UploadFileUrl = "FileURL/" + filename;
                uploads.DateCreated = DateTime.Now;
                var uid = User.Identity.GetUserId();
                uploads.UserId = uid;

                db.Uploads.Add(uploads);
                db.SaveChanges();
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // GET: Uploads/Edit/5
        public ActionResult Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uploads uploads = db.Uploads.Find(id);

            var uid = User.Identity.GetUserId();

            ViewBag.ProjectList = new SelectList(db.project.Where(p => p.UserId == uid), "ProjectID", "Title", uploads.ProjectID);
            
            if (uploads == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = returnUrl;
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,UploadsID,Title,Description,DateCreated,UploadFileUrl,UploadFileUrlPrivacySetting,FileType")] Uploads uploads, HttpPostedFileBase file, string returnUrl)
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
                if(returnUrl!=null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", uploads.UserId);
            return View(uploads);
        }

        // GET: Uploads/Delete/5
        public ActionResult Delete(int? id, string returnUrl)
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
            ViewBag.returnUrl = returnUrl;
            return View(uploads);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Uploads uploads = db.Uploads.Find(id);
            db.Uploads.Remove(uploads);
            db.SaveChanges();
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
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
