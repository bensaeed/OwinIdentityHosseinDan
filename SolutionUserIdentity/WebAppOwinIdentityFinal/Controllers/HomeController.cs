using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOwinIdentityFinal.Attribute;

namespace WebAppOwinIdentityFinal.Controllers
{  [My]
    public class HomeController : Controller
    {
      
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyInfo()
        {
            return View();
        }
        public ActionResult NotAccess()
        {
            return View();
        }
    }
}