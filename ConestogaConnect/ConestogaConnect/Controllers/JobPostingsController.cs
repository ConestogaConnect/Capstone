using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
        public ActionResult Index(string pnum, string jtitle, string desc, string type, double? sal, string exp, string loc, string stitle, string pdfrom, string pdto, string ufrom, string uto)
        {
            var obj = db.JobPostings.ToList();
            if (!string.IsNullOrEmpty(pnum))
            {
                obj = obj.Where(x => x.JobPostingNumber.ToLower().Contains(pnum.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(jtitle))
            {
                obj = obj.Where(x => x.JobTitle.ToLower().Contains(jtitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(desc))
            {
                obj = obj.Where(x => x.JobDescription.ToLower().Contains(desc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                obj = obj.Where(x => x.JobType.ToLower().Contains(type.ToLower())).ToList();
            }
            if (sal != null)
            {
                obj = obj.Where(x => x.Salary == sal).ToList();
            }
            if (!string.IsNullOrEmpty(exp))
            {
                obj = obj.Where(x => x.Experience.ToLower().Contains(exp.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(loc))
            {
                obj = obj.Where(x => x.Location.ToLower().Contains(loc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(stitle))
            {
                obj = obj.Where(x => x.JobSubTitle.ToLower().Contains(stitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(pdfrom))
            {
                DateTime date = DateTime.ParseExact(pdfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Posted_Date >= date).ToList();
            }
            if (!string.IsNullOrEmpty(pdto))
            {
                DateTime date = DateTime.ParseExact(pdto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Posted_Date <= date).ToList();
            }
            if (!string.IsNullOrEmpty(ufrom))
            {
                DateTime date = DateTime.ParseExact(ufrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Last_Updated >= date).ToList();
            }
            if (!string.IsNullOrEmpty(uto))
            {
                DateTime date = DateTime.ParseExact(uto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Last_Updated <= date).ToList();
            }
            ViewBag.pnum = pnum;
            ViewBag.jtitle = jtitle;
            ViewBag.desc = desc;
            ViewBag.type = type;
            ViewBag.exp = exp;
            ViewBag.stitle = stitle;
            ViewBag.pdfrom = pdfrom;
            ViewBag.pdto = pdto;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;
           
            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        //Display Job Postings-Admin Panel
        public ActionResult DisplayJobPosting(string pnum, string jtitle, string desc, string type, double? sal, string exp, string loc, string stitle, string pdfrom, string pdto, string ufrom, string uto)
        {
            var obj = db.JobPostings.ToList();
            if (!string.IsNullOrEmpty(pnum))
            {
                obj = obj.Where(x => x.JobPostingNumber.ToLower().Contains(pnum.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(jtitle))
            {
                obj = obj.Where(x => x.JobTitle.ToLower().Contains(jtitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(desc))
            {
                obj = obj.Where(x => x.JobDescription.ToLower().Contains(desc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                obj = obj.Where(x => x.JobType.ToLower().Contains(type.ToLower())).ToList();
            }
            if (sal != null)
            {
                obj = obj.Where(x => x.Salary == sal).ToList();
            }
            if (!string.IsNullOrEmpty(exp))
            {
                obj = obj.Where(x => x.Experience.ToLower().Contains(exp.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(loc))
            {
                obj = obj.Where(x => x.Location.ToLower().Contains(loc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(stitle))
            {
                obj = obj.Where(x => x.JobSubTitle.ToLower().Contains(stitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(pdfrom))
            {
                DateTime date = DateTime.ParseExact(pdfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Posted_Date >= date).ToList();
            }
            if (!string.IsNullOrEmpty(pdto))
            {
                DateTime date = DateTime.ParseExact(pdto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Posted_Date <= date).ToList();
            }
            if (!string.IsNullOrEmpty(ufrom))
            {
                DateTime date = DateTime.ParseExact(ufrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Last_Updated >= date).ToList();
            }
            if (!string.IsNullOrEmpty(uto))
            {
                DateTime date = DateTime.ParseExact(uto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Last_Updated <= date).ToList();
            }
            ViewBag.pnum = pnum;
            ViewBag.jtitle = jtitle;
            ViewBag.desc = desc;
            ViewBag.type = type;
            ViewBag.exp = exp;
            ViewBag.stitle = stitle;
            ViewBag.pdfrom = pdfrom;
            ViewBag.pdto = pdto;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;

            return View(obj);
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
