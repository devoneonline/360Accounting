using _360Accounting.Common;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Models
{
    public class UserCreateModel
    {
        private string password = string.Empty;

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(password))
                    return "360Accounting";
                else
                    return password;
            }
            set
            {
                password = value;
            }
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Company")]
        public long? CompanyId { get; set; }

        public string Email { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public List<SelectListItem> RoleList
        {
            get
            {
                List<SelectListItem> lst = new List<SelectListItem>();
                foreach (var role in Enum.GetNames(typeof(UserRoles)))
                {
                    if (role.ToUpper() == UserRoles.SuperAdmin.ToString().ToUpper() && AuthenticationHelper.UserRole == UserRoles.SuperAdmin.ToString())
                    {
                        lst.Add(new SelectListItem { Text = role, Value = role });
                    }
                    else
                    {
                        lst.Add(new SelectListItem { Text = role, Value = role });
                    }
                }
                return lst;
            }
        }

        [Display(Name = "Password Question")]
        public string PasswordQuestion { get { return "What is the name of your first school?"; } }

        [Display(Name = "Password Answer")]
        public string PasswordAnswer { get { return "school"; } }

        [Required]
        [Display(Name = "Feature Set")]
        public long FeatureSetId { get; set; }

        public List<SelectListItem> FeatureSetList { get; set; }

        public UserCreateModel()
        {
            FeatureSetList = new List<SelectListItem>();
        }

        public UserCreateModel(List<SelectListItem> fsList)
        {
            FeatureSetList = fsList;
        }
    }
}