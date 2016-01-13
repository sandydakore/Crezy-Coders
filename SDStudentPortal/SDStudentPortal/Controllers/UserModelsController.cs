using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SDStudentPortal.Models
{
    [Authorize]
    public class UserModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserModels
        public ActionResult AdminIndex()
        {
            return View(db.UserModels.ToList());
        }

        // GET: UserModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        public ActionResult StudentProfile(int? id)
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
                UserModel userModel = db.UserModels.Find(id);
                if (userModel == null)
                {
                    return HttpNotFound();
                }
                return View(userModel);
            }
        }

        // GET: SearchUsers
        public ActionResult SearchUsers(string searchString)
        {
            var searchUsers = from u in db.UserModels
                              select u;

            if (!String.IsNullOrEmpty(searchString))
            {                
                searchUsers = searchUsers.Where(u => u.LastName.Contains(searchString) || u.ProfileName.Contains(searchString) || u.FirstName.Contains(searchString));
            }
            //need to add "Public" or "UserOnly" criteria
            return View(searchUsers.ToList());
        }

        // GET: UserModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserModelID,FirstName,LastName,UserEmailPrivacySetting,ProfilePictureURL,ProfilePictureURLPrivacySetting,ClassNumber,ProfileName,Gender,Address,AddressPrivacySetting,PhoneNumber,PhoneNumberPrivacySetting,DateOfBirth,DateOfBirthPrivacySetting,Skills,SkillsPrivacySetting,Certificates,CertificatesPrivacySetting,Memberships,MembershipsPrivacySetting,Experience,ExperiencePrivacySetting")] UserModel userModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                string imagePath = Server.MapPath("~/Images/" + filename);
                file.SaveAs(imagePath);

                userModel.ProfilePictureURL = "Images/" + filename;
                userModel.UserId = User.Identity.GetUserId();
                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Create", "Uploads");
            }
            else if (ModelState.IsValid && file == null)
            {
                userModel.ProfilePictureURL = "Images/default-profile-large.png";
                userModel.UserId = User.Identity.GetUserId();
                db.UserModels.Add(userModel);            
                db.SaveChanges();
                return RedirectToAction("Create","Uploads");
            }
                return View(userModel);
        }

        // GET: UserModels/Edit/5
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
                else if(user==null)
                {
                    return RedirectToAction("Create", "UserModels");
                }
                return View(user);
            }
            else
            {
                UserModel user = db.UserModels.Find(id);

                string fullPath = Request.MapPath("~/" + user.ProfilePictureURL);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                } 

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }           
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserModelID,FirstName,LastName,UserEmailPrivacySetting,ProfilePictureURL,ProfilePictureURLPrivacySetting,ClassNumber,ProfileName,Gender,Address,AddressPrivacySetting,PhoneNumber,PhoneNumberPrivacySetting,DateOfBirth,DateOfBirthPrivacySetting,Skills,SkillsPrivacySetting,Certificates,CertificatesPrivacySetting,Memberships,MembershipsPrivacySetting,Experience,ExperiencePrivacySetting")] UserModel userModel, HttpPostedFileBase file)
        {            
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    var filename = Path.GetFileName(file.FileName);
                    string imagePath = Server.MapPath("~/Images/" + filename);
                    file.SaveAs(imagePath);

                    userModel.ProfilePictureURL = "Images/" + filename;                    
                }
                
                userModel.UserId = User.Identity.GetUserId();
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Uploads");
            }
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.UserModels.Find(id);
            db.UserModels.Remove(userModel);
            db.SaveChanges();

            string fullPath = Request.MapPath("~/" + userModel.ProfilePictureURL);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
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
