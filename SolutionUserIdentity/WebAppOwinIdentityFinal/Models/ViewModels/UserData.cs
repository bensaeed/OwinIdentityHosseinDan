using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WebAppOwinIdentityFinal.Models.DataBase;

namespace WebAppOwinIdentityFinal.Models.ViewModels
{
    public class UserData:IDisposable
    {
        private readonly IdentityDBEntities _db = new IdentityDBEntities();

        public UserData()
        {
            var identity = ClaimsPrincipal.Current.Claims;

            if (identity == null) return;
            var enumerable = identity as IList<Claim> ?? identity.ToList();

            UserName = enumerable.Where(c => c.Type == "UserData")
                .Select(c => c.Value).Single();

            FullName = enumerable.Where(c => c.Type == "Name")
                .Select(c => c.Value).Single();

            Country = enumerable.Where(c => c.Type == "Country")
                .Select(c => c.Value).Single();

            Roles = enumerable.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        }


        public string UserName { get; private set; }
        //public IEnumerable<int> AccessCodes { get; private set; }
        public IEnumerable<string> Roles { get; private set; }
        public string FullName { get; private set; }
        public string Country { get; private set; }
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}