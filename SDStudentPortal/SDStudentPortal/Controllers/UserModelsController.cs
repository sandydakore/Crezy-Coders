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
            //UserModel userModel = db.UserModels.Find(id);
            //if (userModel == null)
            //{
            //    return HttpNotFound();
            //}

            var uid = User.Identity.GetUserId();
            var user = db.UserModels.Where(u => u.UserId == uid).SingleOrDefault();

            if (uid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserModelID,FirstName,LastName,UserEmailPrivacySetting,ProfilePictureURL,ProfilePictureURLPrivacySetting,ClassNumber,ProfileName,Gender,Address,AddressPrivacySetting,PhoneNumber,PhoneNumberPrivacySetting,DateOfBirth,DateOfBirthPrivacySetting,Skills,SkillsPrivacySetting,Certificates,CertificatesPrivacySetting,Memberships,MembershipsPrivacySetting,Experience,ExperiencePrivacySetting")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
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
