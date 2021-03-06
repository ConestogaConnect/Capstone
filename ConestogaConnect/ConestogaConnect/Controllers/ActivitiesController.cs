﻿using System;
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
    public class ActivitiesController : Controller
    {
        private ActivitiesEntities db = new ActivitiesEntities();

        // GET: Activities
        public ActionResult Index(string name, string desc, string type, string afrom, string ato, string ufrom, string uto)
        {
            var obj = db.Activities.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                obj = obj.Where(x => x.ActivityName.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(desc))
            {
                obj = obj.Where(x => x.Description.ToLower().Contains(desc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                obj = obj.Where(x => x.ActivityType.ToLower().Contains(type.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(afrom))
            {
                DateTime date = DateTime.ParseExact(afrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Added_On >= date).ToList();
            }
            if (!string.IsNullOrEmpty(ato))
            {
                DateTime date = DateTime.ParseExact(ato, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Added_On <= date).ToList();
            }
            if (!string.IsNullOrEmpty(ufrom))
            {
                DateTime date = DateTime.ParseExact(ufrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Updated_On >= date).ToList();
            }
            if (!string.IsNullOrEmpty(uto))
            {
                DateTime date = DateTime.ParseExact(uto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Updated_On <= date).ToList();
            }

            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.desc = desc;
          
            ViewBag.pdfrom = afrom;
            ViewBag.pdto = ato;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;
          
            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DisplayActivity(string name, string desc, string type, string afrom, string ato, string ufrom, string uto)
        {
            var obj = db.Activities.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                obj = obj.Where(x => x.ActivityName.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(desc))
            {
                obj = obj.Where(x => x.Description.ToLower().Contains(desc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                obj = obj.Where(x => x.ActivityType.ToLower().Contains(type.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(afrom))
            {
                DateTime date = DateTime.ParseExact(afrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Added_On >= date).ToList();
            }
            if (!string.IsNullOrEmpty(ato))
            {
                DateTime date = DateTime.ParseExact(ato, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Added_On <= date).ToList();
            }
            if (!string.IsNullOrEmpty(ufrom))
            {
                DateTime date = DateTime.ParseExact(ufrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Updated_On >= date).ToList();
            }
            if (!string.IsNullOrEmpty(uto))
            {
                DateTime date = DateTime.ParseExact(uto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Updated_On <= date).ToList();
            }

            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.desc = desc;

            ViewBag.pdfrom = afrom;
            ViewBag.pdto = ato;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;

            return View(obj);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,ActivityName,Description,ActivityType,Added_On,Updated_On")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activity.UserId = User.Identity.GetUserId();
                activity.Added_On = DateTime.Now;
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityName,Description,ActivityType,Added_On,Updated_On")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                activity.Updated_On = System.DateTime.Now;
                activity.UserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
