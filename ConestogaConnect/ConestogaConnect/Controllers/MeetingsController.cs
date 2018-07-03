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

namespace ConestogaConnect.Controllers
{
    public class MeetingsController : Controller
    {
        private ActivitiesEntities db = new ActivitiesEntities();

        // GET: Meetings
        public ActionResult Index(string mtitle, string desc, string sub, string loc, int? program, int? sem, string add, string mfrom, string mto, string afrom, string ato)
        {
            var _meetings = db.Meetings.ToList();
            var _meetingstatus = db.MeetingsStatus.Where(x => x.UserId == User.Identity.Name).ToList();
            if (program != null && program > 0)
            {
                _meetings = _meetings.Where(x => x.Program == program).ToList();
            }
            if (sem != null && sem > 0)
            {
                _meetings = _meetings.Where(x => x.Semester == sem).ToList();
            }
            
            if (!string.IsNullOrEmpty(mtitle))
            {
                _meetings = _meetings.Where(x => x.MeetingTitle.ToLower().Contains(mtitle.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(desc))
            {
                _meetings = _meetings.Where(x => x.Description.ToLower().Contains(desc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(sub))
            {
                _meetings = _meetings.Where(x => x.Subject.ToLower().Contains(sub.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(loc))
            {
                _meetings = _meetings.Where(x => x.Location.ToLower().Contains(loc.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(add))
            {
                _meetings = _meetings.Where(x => x.AddedBy.ToLower().Contains(add.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(mfrom))
            {
                DateTime date = DateTime.ParseExact(mfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _meetings = _meetings.Where(x => x.MeetingDateTime >= date).ToList();
            }
            if (!string.IsNullOrEmpty(mto))
            {
                DateTime date = DateTime.ParseExact(mto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _meetings = _meetings.Where(x => x.MeetingDateTime <= date).ToList();
            }
            if (!string.IsNullOrEmpty(afrom))
            {
                DateTime date = DateTime.ParseExact(afrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _meetings = _meetings.Where(x => x.AddedOn >= date).ToList();
            }
            if (!string.IsNullOrEmpty(ato))
            {
                DateTime date = DateTime.ParseExact(ato, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _meetings = _meetings.Where(x => x.AddedOn <= date).ToList();
            }
            var programs = db.Programs.ToList();
            var vm = _meetings.Select(x => new MeetingViewModel
            {
                AddedBy = x.AddedBy,
                AddedOn = x.AddedOn,
                Description = x.Description,
                Id = x.Id,
                IsAccepted = (bool)(_meetingstatus.Where(t => t.MeetingId == x.Id).Count() > 0? _meetingstatus.Where(t => t.MeetingId == x.Id).FirstOrDefault().IsAccepted: false),
                Location = x.Location,
                MeetingDateTime = x.MeetingDateTime,
                MeetingTitle = x.MeetingTitle,
                ProgramName = programs.Where(t => t.Id == x.Program).FirstOrDefault() == null? "": programs.Where(t=>t.Id == x.Program).FirstOrDefault().ProgramName,
                Semester = x.Semester,
                Subject = x.Subject,
                UpdatedBy = x.UpdatedBy,
                UpdatedOn = x.UpdatedOn,
            }).OrderByDescending(x => x.Id).ToList();
            ViewBag.program = programs.Select(x => new SelectListItem { Text = x.ProgramName, Value = x.Id.ToString(), Selected = x.Id == program }).ToList();
            

            ViewBag.mtitle = mtitle;
            ViewBag.sub = sub;
            ViewBag.desc = desc;
            ViewBag.loc = loc;
          //  ViewBag.program = program;
            ViewBag.sem = sem;
            ViewBag.add = add;
            ViewBag.pdfrom = mfrom;
            ViewBag.pdto = mto;
            ViewBag.ufrom = afrom;
            ViewBag.uto = ato;
            return View(vm);
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            var pro = db.Programs.Select(x => new SelectListItem { Text = x.ProgramName, Value = x.Id.ToString() }).ToList();
            ViewBag.programList = pro;
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MeetingTitle,Description,Subject,MeetingDateTime,Location,Program,Semester,AddedBy,AddedOn,UpdatedBy,UpdatedOn")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                meeting.AddedBy = User.Identity.Name;
                meeting.AddedOn = DateTime.Now;
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var pro = db.Programs.Select(x => new SelectListItem { Text = x.ProgramName, Value = x.Id.ToString() }).ToList();
            ViewBag.programList = pro;
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            var pro = db.Programs.Select(x => new SelectListItem { Text = x.ProgramName, Value = x.Id.ToString() }).ToList();
            ViewBag.programList = pro;
            return View(meeting);
        }

        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }

            MeetingsStatu sts = db.MeetingsStatus.Where(x => x.MeetingId == id && x.UserId == User.Identity.Name).FirstOrDefault();
            if (sts == null)
            {
                sts = new MeetingsStatu();
            }

            sts.UserId = User.Identity.Name;
            sts.MeetingId = id;
            sts.IsAccepted = sts.IsAccepted == null || sts.IsAccepted == false ? true : false;
            if (sts.IsAccepted == true)
            {
                sts.AcceptedOn = DateTime.Now;
            }
            else
            {
                sts.RejectedOn = DateTime.Now;
            }
            if (sts.Id > 0)
            {
                db.Entry(sts).State = EntityState.Modified;
            }
            else
            {
                db.MeetingsStatus.Add(sts);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

       

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MeetingTitle,Description,Subject,MeetingDateTime,Location,Program,Semester,AddedBy,AddedOn,UpdatedBy,UpdatedOn")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                meeting.AddedBy = User.Identity.Name;
                meeting.UpdatedBy = User.Identity.Name;
                meeting.UpdatedOn = DateTime.Now;
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var pro = db.Programs.Select(x => new SelectListItem { Text = x.ProgramName, Value = x.Id.ToString() }).ToList();
            ViewBag.programList = pro;
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
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
