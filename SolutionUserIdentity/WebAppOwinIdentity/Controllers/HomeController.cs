using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppOwinIdentity.Models.DataBase;

namespace WebAppOwinIdentity.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        IdentityDBEntities _Context = new IdentityDBEntities();
        public  ActionResult UsersInformation()
        {
            var Result = _Context.Users.ToList();
            return View(Result);
        }
    }
}