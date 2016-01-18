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
            get { return 1; }   ////TODO: Need to change when actual user will be available.
            set { base["CompanyId"] = value; }
        }

        public static UserProfile GetProfile(string userName)
        {
            return ProfileBase.Create(userName) as UserProfile;
        }
    }
}