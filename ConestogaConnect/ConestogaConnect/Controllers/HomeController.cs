using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConestogaConnect.Controllers
{
    //[System.Web.Mvc.Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Admin page.";

            return View();
        }
    }
}