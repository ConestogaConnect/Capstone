using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConestogaConnect.Models;
using Microsoft.AspNet.Identity;

namespace ConestogaConnect.Controllers
{
    public class AccomodationsController : Controller
    {
        private AccomodationEntities db = new AccomodationEntities();

        // GET: Accomodations
        public ActionResult Index(int? rooms, decimal? rent, string facilities, int? floors, string pdfrom, string pdto, string ufrom, string uto, bool? petfriendly, bool? parking, bool? furnished)
        {
            var obj = db.Accomodations.ToList();
            if(rooms != null && rooms > 0)
            {
                obj = obj.Where(x => x.Number_of_Rooms == rooms).ToList();
            }
            if (rent != null && rent > 0)
            {
                obj = obj.Where(x => x.Rent == rent.ToString()).ToList();
            }
            if (floors != null && floors > 0)
            {
                obj = obj.Where(x => x.Floor == floors.ToString()).ToList();
            }
            if (!string.IsNullOrEmpty(facilities))
            {
               obj = obj.Where(x => x.Facilities.ToLower().Contains(facilities.ToLower())).ToList();
            }
            if (petfriendly != null)
            {
                obj = obj.Where(x => x.PetFriendly == petfriendly).ToList();
            }
            if (parking != null)
            {
                obj = obj.Where(x => x.Parking == parking).ToList();
            }
            if (furnished != null)
            {
                obj = obj.Where(x => x.Furnished == furnished).ToList();
            }
            if (!string.IsNullOrEmpty(pdfrom))
            {
                DateTime date = DateTime.ParseExact(pdfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                obj = obj.Where(x => x.Posted_Date >= date).ToList();
            }
            if (!string.IsNullOrEmpty(pdto))
            {
                DateTime date = DateTime.ParseExact(pdto,"dd/MM/yyyy", CultureInfo.InvariantCulture);
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
            ViewBag.rooms = rooms;
            ViewBag.rent = rent;
            ViewBag.facilities = facilities;
            ViewBag.floors = floors;
            ViewBag.pdfrom = pdfrom;
            ViewBag.pdto = pdto;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;
            ViewBag.petfriendly = petfriendly;
            ViewBag.parking = parking;
            ViewBag.furnished = furnished;
            
            return View(obj.ToList());
        }

        //Display Accomodations-Admin
        [Authorize(Roles = "Admin")]
        public ActionResult DisplayAccomodations(int? rooms, decimal? rent, string facilities, int? floors, string pdfrom, string pdto, string ufrom, string uto, bool? petfriendly, bool? parking, bool? furnished)
        {
            var obj = db.Accomodations.ToList();
            if (rooms != null && rooms > 0)
            {
                obj = obj.Where(x => x.Number_of_Rooms == rooms).ToList();
            }
            if (rent != null && rent > 0)
            {
                obj = obj.Where(x => x.Rent == rent.ToString()).ToList();
            }
            if (floors != null && floors > 0)
            {
                obj = obj.Where(x => x.Floor == floors.ToString()).ToList();
            }
            if (!string.IsNullOrEmpty(facilities))
            {
                obj = obj.Where(x => x.Facilities.ToLower().Contains(facilities.ToLower())).ToList();
            }
            if (petfriendly != null)
            {
                obj = obj.Where(x => x.PetFriendly == petfriendly).ToList();
            }
            if (parking != null)
            {
                obj = obj.Where(x => x.Parking == parking).ToList();
            }
            if (furnished != null)
            {
                obj = obj.Where(x => x.Furnished == furnished).ToList();
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
            ViewBag.rooms = rooms;
            ViewBag.rent = rent;
            ViewBag.facilities = facilities;
            ViewBag.floors = floors;
            ViewBag.pdfrom = pdto;
            ViewBag.pdto = pdto;
            ViewBag.ufrom = ufrom;
            ViewBag.uto = uto;
            ViewBag.petfriendly = petfriendly;
            ViewBag.parking = parking;
            ViewBag.furnished = furnished;
            return View(obj);
        }

        // GET: Accomodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // GET: Accomodations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accomodations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image,[Bind(Include = "UserId,Number_of_Rooms,Rent,Facilities,PetFriendly,Parking,Floor,Furnished,Posted_Date,Image_Id,Image")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                accomodation.UserId = User.Identity.GetUserId();
                accomodation.Posted_Date = DateTime.Now;
                db.Accomodations.Add(accomodation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accomodation);
        }

        // GET: Accomodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Number_of_Rooms,Rent,Facilities,PetFriendly,Parking,Floor,Furnished,Posted_Date,Last_Updated,Image_Id")] Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accomodation).State = EntityState.Modified;
                accomodation.Last_Updated = System.DateTime.Now;
                accomodation.UserId = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accomodation);
        }

        // GET: Accomodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return HttpNotFound();
            }
            return View(accomodation);
        }

        // POST: Accomodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            db.Accomodations.Remove(accomodation);
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
