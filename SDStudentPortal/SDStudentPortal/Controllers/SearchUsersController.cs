using SDStudentPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDStudentPortal.Controllers
{
    public class SearchUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SearchUsers
        public ActionResult Index(string searchString)
        {
            var searchUsers = from u in db.UserModels
                                select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchUsers = searchUsers.Where(u => u.FirstName.Contains(searchString));
                searchUsers = searchUsers.Where(u => u.LastName.Contains(searchString));
                searchUsers = searchUsers.Where(u => u.ProfileName.Contains(searchString));         
            }
            //need to add "Public" or "UserOnly" criteria
            return View(searchUsers.ToList());
        }
    }
}