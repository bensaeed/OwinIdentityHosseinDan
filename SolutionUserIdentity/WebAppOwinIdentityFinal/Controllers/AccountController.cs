using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebAppOwinIdentityFinal.Attribute;
using WebAppOwinIdentityFinal.Models.DataBase;
using WebAppOwinIdentityFinal.Models.ViewModels;

namespace WebAppOwinIdentityFinal.Controllers
{
    
    [AllowAnonymous]
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
            var StrPass = creat(model.Password);
            var CurUser = _Context.Users.Single(x => x.UserName == model.Email && x.PasswordHash==StrPass);
            var RoleUser = CurUser.Roles.FirstOrDefault();
            // Don't do this in production!
            // if (model.Email == "admin@admin.com" && model.Password == "password")

            JsonFormat newjson = new JsonFormat();
            List<information> p = new List<information>();
            information add1 = new information { controller = "Home", action = "MyInfo"  };
            information add2 = new information { controller = "Home", action = "MyInfo2" };
            p.Add(add1);
            p.Add(add2);
            newjson.information = p;
            string json = JsonConvert.SerializeObject(newjson);
            if (CurUser != null  && RoleUser !=null)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.UserData, CurUser.UserName),
                        new Claim(ClaimTypes.Name, CurUser.FirstName+" "+ CurUser.LastName),
                        new Claim(ClaimTypes.Email, CurUser.Email),
                        new Claim(ClaimTypes.Country, "Iran"),
                        new Claim("JsonListConvert",json),
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
        private string creat(string s)
        {
            StringBuilder sb = new StringBuilder();
            SHA256 hash = SHA256Managed.Create();
            Encoding enc = Encoding.UTF8;
            byte[] hashbyte = hash.ComputeHash(enc.GetBytes(s));
            foreach (byte b in hashbyte)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
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