using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoverMap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt mig :-)";

            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        [Authorize]
        public ActionResult MapAdmin()
        {
            return View();
        }
    }
}