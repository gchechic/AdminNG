using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminNG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //HttpContext.Session["Test"] = "Ben Rules!";
            return View();
        }

        public ActionResult About()
        {            
            ViewBag.Message = "Your application description page.";
         //   ViewBag.Message = HttpContext.Session["Test"];
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}