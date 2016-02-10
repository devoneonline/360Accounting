using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevEx_360Accounting_Web.Models
{
    public class UserProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact Number")]
        public string PhoneNumer { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public long CompanyId { get; set; }
    }
}