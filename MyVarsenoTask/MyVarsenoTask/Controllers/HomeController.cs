using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVarsenoTask.Controllers
{
    public class HomeController : Controller
    {
        //log4net
        //private static log4net.ILog Log { get ; set ;}

        // ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public ActionResult Index()
        {
            log.Debug("debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}