using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SDStudentPortal.Models
{
     [Authorize]
    public class StudentProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentProfiles
        public ActionResult Index()
        {
            var studentProfiles = db.StudentProfiles.Include(s => s.User);
            return View(studentProfiles.ToList());
        }

        // GET: StudentProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

        // GET: StudentProfiles/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: StudentProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentProfileID")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                var uid= User.Identity.GetUserId();
                studentProfile.UserId = uid;
                db.StudentProfiles.Add(studentProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(studentProfile);
        }

        // GET: StudentProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                var uid = User.Identity.GetUserId();
                var user = db.UserModels.Where(u => u.UserId == uid).SingleOrDefault();

                if (uid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(user);
            }
            else
            {
                StudentProfile studentProfile = db.StudentProfiles.Find(id);
                if (studentProfile == null)
                {
                    return HttpNotFound();
                }
                return View(studentProfile);
            }
            
        }

        // POST: StudentProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentProfileID")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();
                studentProfile.UserId = uid;
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(studentProfile);
        }

        // GET: StudentProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

        // POST: StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            db.StudentProfiles.Remove(studentProfile);
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
