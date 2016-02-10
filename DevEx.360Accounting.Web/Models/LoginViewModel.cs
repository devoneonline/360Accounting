using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 7, ErrorMessage = "Password should not less than 7 digits!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}