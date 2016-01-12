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
    [Authorize]
    public class BlogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            return View(db.blog.Where(b => b.UserId == uid));
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogID,Content,BlogPrivacySetting")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();

                blog.UserId = uid;
                blog.BlogCreatedDate = DateTime.Now;
                blog.BlogUpdatedDate = Convert.ToDateTime("01/01/2000");

                db.blog.Add(blog);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogID,Content,BlogCreatedDate,BlogPrivacySetting")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();

                blog.UserId = uid;
                blog.BlogUpdatedDate = DateTime.Now;

                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Blog blog = db.blog.Find(id);

            if (blog == null)
            {
                return HttpNotFound();
            }
            db.blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BlogSearchResult(string txtSearch)
        {
            IEnumerable<Blog> blogs = db.blog.Where(b => b.Content.ToLower().Contains(txtSearch.ToLower())).ToList();
            IEnumerable<BlogComment> comments = db.blogcomment.ToList();

            var tuple = new Tuple<IEnumerable<Blog>, IEnumerable<BlogComment>>(blogs, comments);

            return View(tuple);
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