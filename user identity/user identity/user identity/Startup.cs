using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using user_identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using user_identity.Identity;

[assembly: OwinStartup(typeof(user_identity.Startup))]

namespace user_identity
{
    public class Startup
    {
        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });


            // configure the user manager
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ApplicationDbContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<ApplicationUser>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                // use out custom claims provider
                usermanager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();

                return usermanager;
            };
        }

    }
}
