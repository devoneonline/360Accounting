using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace _360Accounting.Web.Models
{
    public class UserProfile : ProfileBase
    {
        public string FirstName
        {
            get { return (string)base["FirstName"]; }
            set { base["FirstName"] = value; }
        }

        public string LastName
        {
            get { return (string)base["LastName"]; }
            set { base["LastName"] = value; }
        }

        public string PhoneNumber
        {
            get { return (string)base["PhoneNumber"]; }
            set { base["PhoneNumber"] = value; }
        }

        public string Email
        {
            get { return (string)base["Email"]; }
            set { base["Email"] = value; }
        }

        public long CompanyId 
        {
            get { return 1; }   //TODO: Need to change when actual user will be available.
            set { base["CompanyId"] = value; }
        }

        public static UserProfile GetProfile(string userName)
        {
            return ProfileBase.Create(userName) as UserProfile;
        }
    }


    public class ManageUserViewModel
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

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}