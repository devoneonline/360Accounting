﻿using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public sealed class AuthenticationHelper
    {
        private const string SESSION_USER = "USER_PROFILE";

        public static UserProfile User
        {
            get
            {
                return HttpContext.Current.Session[SESSION_USER] == null ? null : (UserProfile)HttpContext.Current.Session[SESSION_USER];
            }
            set
            {
                HttpContext.Current.Session[SESSION_USER] = value;
            }
        }

    }
}