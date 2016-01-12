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
    public class BlogCommentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult PostComment(int Bid, string comment)
        {
            BlogComment newComment = new BlogComment();

            var uid = User.Identity.GetUserId();

            newComment.UserId = uid;
            newComment.Content = comment;
            newComment.CommentDate = DateTime.Now;
            newComment.BlogID = Bid;

            db.blogcomment.Add(newComment);
            db.SaveChanges();

            return Json(new { status = "Success", message = "Success" });
        }

        public ActionResult LoadAllComments(int BlogID)
        {
            IEnumerable<BlogComment> comments = db.blogcomment.Where(c => c.BlogID == BlogID).OrderByDescending(c => c.CommentDate);

            return View(comments.ToList());
        }
    }
}