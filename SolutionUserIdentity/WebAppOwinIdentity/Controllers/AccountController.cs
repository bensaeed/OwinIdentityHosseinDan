using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebAppOwinIdentity.Models.DataBase;

namespace WebAppOwinIdentity.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new Models.ViewModels.Login
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        IdentityDBEntities _Context = new IdentityDBEntities(); 
        [HttpPost]
        public ActionResult LogIn(Models.ViewModels.Login model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var CurUser = _Context.Users.Single(x => x.Id == 1);
            var RoleUser = CurUser.Roles.FirstOrDefault();
            // Don't do this in production!
            if (model.Email == "admin@admin.com" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Ben"),
                new Claim(ClaimTypes.Email, "a@b.com"),
                new Claim(ClaimTypes.Country, "England"),
                new Claim(ClaimTypes.Role,RoleUser.Name)
            },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}