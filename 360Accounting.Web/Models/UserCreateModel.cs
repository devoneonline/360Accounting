using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web.Models
{
    public class UserCreateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public LoginViewModel Credentials { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }

        public string PasswordQuestion { get; set; }

        public string PasswordAnswer { get; set; }
    }
}