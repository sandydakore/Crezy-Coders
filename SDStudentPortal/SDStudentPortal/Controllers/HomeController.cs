using SDStudentPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace SDStudentPortal.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Project prj = db.project.Find(1);
            if (prj == null)
            {
                Project initPrj = new Project();

                initPrj.Title = "Please Select Project";

                db.project.Add(initPrj);
                db.SaveChanges();
            }

            IEnumerable<Blog> blogs = db.blog.OrderByDescending(b => b.BlogCreatedDate).ToList();
            IEnumerable<BlogComment> comments = db.blogcomment.ToList();

            var tuple = new Tuple<IEnumerable<Blog>, IEnumerable<BlogComment>>(blogs, comments);

            return View(tuple);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}