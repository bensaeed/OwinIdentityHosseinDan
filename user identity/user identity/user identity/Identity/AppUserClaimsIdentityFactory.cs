using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using user_identity.Models;

namespace user_identity.Identity
{
    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<ApplicationUser, string>
    {
        public async override Task<ClaimsIdentity> CreateAsync(UserManager<ApplicationUser, string> manager,
                ApplicationUser user, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            return identity;
        }
    }
}