using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rytasoft.Sheer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A sample to show how to retrive Metadata and process them inside Angularjs";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please give me your ideas and your feedbacks about this sample at ryan.tavan@live.com.au";
            return View();
        }
    }
}