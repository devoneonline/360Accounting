using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public sealed class AuthenticationHelper
    {
        private const string SessionUser = "USER_PROFILE";

        public static UserProfile User
        {
            get
            {
                return HttpContext.Current.Session[SessionUser] == null ? null : (UserProfile)HttpContext.Current.Session[SessionUser];
            }

            set
            {
                HttpContext.Current.Session[SessionUser] = value;
            }
        }
    }
}