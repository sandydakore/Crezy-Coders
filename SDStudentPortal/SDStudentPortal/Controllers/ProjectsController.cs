using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SDStudentPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SDStudentPortal.Controllers
{    
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: Projects
        public ActionResult Index()
        {
            IEnumerable<Project> projects;

            projects = db.project;

            return View(projects);
        }

        
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var uid = User.Identity.GetUserId();

            ViewBag.ProjectFile = new List<Uploads>(db.Uploads.Where(u => u.ProjectID == id && u.UserId == uid).ToList());

            return View(project);
        }

        [Authorize]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Title,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();

                project.UserId = uid;

                db.project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        [Authorize]
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var uid = User.Identity.GetUserId();

            ViewBag.ProjectFile = new List<Uploads>( db.Uploads.Where(u => u.ProjectID == id && u.UserId== uid).ToList());

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Title,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();

                project.UserId = uid;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [Authorize]
        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.project.Find(id);
            db.project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
                
        public ActionResult SearchProject(string txtsearchQuery)
        {
            if (txtsearchQuery != null)
            {

                IEnumerable<Project> projects = db.project.Where(p => p.Description.ToLower().Contains(txtsearchQuery.ToLower()) || p.Title.ToLower().Contains(txtsearchQuery.ToLower()));

                return View(projects);
            }
            else
            {
                IEnumerable<Project> projects;

                projects = db.project;

                return View(projects);
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
