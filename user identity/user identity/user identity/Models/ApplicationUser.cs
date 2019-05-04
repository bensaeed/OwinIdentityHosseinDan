using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace user_identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; set; }
        public int Age { get; set; }
    }
}