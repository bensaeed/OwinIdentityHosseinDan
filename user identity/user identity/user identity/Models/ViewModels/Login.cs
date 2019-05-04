using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace user_identity.Models.ViewModels
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("شناسه")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("پسورد")]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}