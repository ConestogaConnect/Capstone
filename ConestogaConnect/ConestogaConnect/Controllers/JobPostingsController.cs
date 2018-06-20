using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConestogaConnect.Models;
using Microsoft.AspNet.Identity;

namespace ConestogaConnect.Controllers
{
    public class JobPostingsController : Controller
    {
        private JobPostingEntities db = new JobPostingEntities();

        // GET: JobPostings- Students
        public ActionResult Index()
        {
            return View(db.JobPostings.ToList());
        }

        [Authorize(Roles = "Admin")]
        //Display Job Postings-Admin Panel
        public ActionResult DisplayJobPosting()
        {
            return View(db.JobPostings.ToList());
        }

        // GET: JobPostings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // GET: JobPostings/Create
        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobPostings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public ActionResult Create([Bind(Include = "UserId,JobPostingNumber,JobTitle,JobDescription,JobType,Salary,Experience,Location,JobSubTitle,Posted_Date")] JobPosting jobPosting)
        {
            if (ModelState.IsValid)
            {
                jobPosting.UserId= User.Identity.GetUserId();
                jobPosting.Posted_Date= DateTime.Now;
                db.JobPostings.Add(jobPosting);
                
                db.SaveChanges();

               // var userId=User.Identity.GetUserId();
               // db.JobPostingEntities.


                return RedirectToAction("Index");
            }

            return View(jobPosting);
        }

        // GET: JobPostings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // POST: JobPostings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public ActionResult Edit([Bind(Include = "Id,UserId,JobPostingNumber,JobTitle,JobDescription,JobType,Salary,Experience,Location,JobSubTitle,Posted_Date,Last_Updated")] JobPosting jobPosting)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(jobPosting).State = EntityState.Modified;
                jobPosting.Last_Updated = System.DateTime.Now;
                jobPosting.UserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobPosting);
        }

        // GET: JobPostings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPosting jobPosting = db.JobPostings.Find(id);
            if (jobPosting == null)
            {
                return HttpNotFound();
            }
            return View(jobPosting);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPosting jobPosting = db.JobPostings.Find(id);
            db.JobPostings.Remove(jobPosting);
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
