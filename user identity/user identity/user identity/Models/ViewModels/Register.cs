using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace user_identity.Models.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("شناسه")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("پسورد")]
        public string Password { get; set; }

        [Required]
        [DisplayName("کشور")]
        public string Country { get; set; }

        [Required]
        [DisplayName("سن")]
        public int Age { get; set; }
    }
}