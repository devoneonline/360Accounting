using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

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
            get { return 1; }   ////TODO: Need to change when actual user will be available.
            set { base["CompanyId"] = value; }
        }

        public static UserProfile GetProfile(string userName)
        {
            return ProfileBase.Create(userName) as UserProfile;
        }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword",ErrorMessage = "Confirm password do not match!")]
        public string ConfirmPassword { get; set; }
    }

    public class MembershipUserListModel
    {
        private string sortColumn = "Segments";
        private string sortDirection = "ASC";

        #region Properties

        public string SearchText { get; set; }

        public int? Page { get; set; }

        public int TotalRecords { get; set; }

        public string SortColumn
        {
            get { return sortColumn; }
            set { sortColumn = value; }
        }

        public string SortDirection
        {
            get { return sortDirection; }
            set { sortDirection = value; }
        }

        public List<UserViewModel> Users { get; set; }

        #endregion

    }

    public class UserViewModel
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public UserViewModel()
        {

        }
        public UserViewModel(Guid userId, string userName, string firstName, string lastName, string phoneNumber, string email, string companyName)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.CompanyName = companyName;
        }
    }
}