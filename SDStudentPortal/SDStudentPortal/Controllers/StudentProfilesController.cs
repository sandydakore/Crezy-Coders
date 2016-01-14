﻿using System;
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
            var users = db.UserModels.Include(u => u.User);

            return View(users.ToList());
        }

        // GET: StudentProfiles/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                var uid = User.Identity.GetUserId();

                if (uid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                StudentProfile studentProfile = new StudentProfile();
                studentProfile.User = db.UserModels.Where(u => u.UserId == uid).SingleOrDefault();
                studentProfile.Blogs = db.blog.Where(b => b.UserId == uid);
                studentProfile.Projects = db.project.Where(p => p.UserId == uid);

                return View(studentProfile);
            }
            else
            {
                StudentProfile studentProfile = new StudentProfile();
                studentProfile.User = db.UserModels.Where(u => u.UserId == id).SingleOrDefault();
                studentProfile.Blogs = db.blog.Where(b => b.UserId == id);
                studentProfile.Projects = db.project.Where(p => p.UserId == id);

                if (studentProfile.User == null)
                {
                    return HttpNotFound();
                }
                return View(studentProfile);
            }
            //if (id == null)
            //{
            //    var uid = User.Identity.GetUserId();
            //    StudentProfile studentProfile = new StudentProfile();
            //    studentProfile.User = db.UserModels.Where(u => u.UserId == id).SingleOrDefault();
            //    studentProfile.Blogs = db.blog.Where(b => b.UserId == id);
            //    studentProfile.Projects = db.project.Where(p => p.UserId == id);
            //    if (studentProfile.User == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(studentProfile);
            //}
            //else
            //{
            //    StudentProfile studentProfile = new StudentProfile();
            //    studentProfile.User = db.UserModels.Where(u => u.UserId == id).SingleOrDefault();
            //    studentProfile.Blogs = db.blog.Where(b => b.UserId == id);
            //    studentProfile.Projects = db.project.Where(p => p.UserId == id);
            //    if (studentProfile.User == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(studentProfile);
            //}

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //var uid = id != null ? id.ToString() : "-1";
            //StudentProfile studentProfile = new StudentProfile();
            //studentProfile.User = db.UserModels.Where(u => u.UserId == uid).SingleOrDefault();

            //if (studentProfile.User == null)
            //{
            //    return HttpNotFound();
            //}

            //studentProfile.Blogs = db.blog.Where(b => b.UserId == uid);
            //studentProfile.Projects = db.project.Where(p => p.UserId == uid);

            //return View(studentProfile);
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
