using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Zadatko.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Zadaci");
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Ukratko o namjeni ove aplikacije...";

            return View();
        }

        /* ne treba mi kontakt stranica na mojoj aplikaciji
        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt stranica.";

            return View();
        }
        */
    }
}