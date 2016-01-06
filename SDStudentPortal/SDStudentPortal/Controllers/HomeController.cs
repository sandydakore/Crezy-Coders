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
            IEnumerable<Blog> blogs = db.blog.OrderByDescending(b => b.BlogCreatedDate).ToList();
            IEnumerable<BlogComment> comments = db.blogcomment.ToList();
            UserModel userModel = db.UserModels.Find(3);

            var tuple = new Tuple<IEnumerable<Blog>, IEnumerable<BlogComment>,UserModel>(blogs, comments,userModel);

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