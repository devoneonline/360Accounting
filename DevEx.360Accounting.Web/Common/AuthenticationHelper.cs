﻿using DevEx_360Accounting_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DevEx_360Accounting_Web
{
    public sealed class AuthenticationHelper
    {
        private const string SESSION_USER = "USER_PROFILE";
        private const string SESSION_MENU_ITEMS = "MENU_ITEMS";

        public static UserProfile User
        {
            get
            {
                return HttpContext.Current.Session[SESSION_USER] == null 
                    ? UserProfile.GetProfile(System.Threading.Thread.CurrentPrincipal.Identity.Name)
                    : (UserProfile)HttpContext.Current.Session[SESSION_USER];
            }

            set
            {
                HttpContext.Current.Session[SESSION_USER] = value;
            }
        }

        public static string UserRole
        {
            get
            {
                return
                Roles.GetRolesForUser(User.UserName)[0] ?? string.Empty;
            }
        }

        public static IEnumerable<FeatureViewModel> MenuItems
        {
            get
            {
                return HttpContext.Current.Session[SESSION_MENU_ITEMS] == null 
                    ? null 
                    : (IEnumerable<FeatureViewModel>)HttpContext.Current.Session[SESSION_MENU_ITEMS];
            }

            set
            {
                HttpContext.Current.Session[SESSION_MENU_ITEMS] = value;
            }
        }
    }
}