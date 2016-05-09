using _360Accounting.Common;
using _360Accounting.Web.Helpers;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _360Accounting.Web
{
    public sealed class AuthenticationHelper
    {
        private const string SESSION_USER = "USER_PROFILE";
        private const string SESSION_MENU_ITEMS = "MENU_ITEMS";
        private const string SESSION_COMPANY_ID = "USER_COMPANY_ID";
        private const string SESSION_COMPANY_NAME = "USER_COMPANY_NAME";
        private const string SESSION_COMPANY_LIST = "COMPANY_LIST";
        private const string SESSION_USER_ID = "USER_ID";


        public static Guid UserId
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USER_ID] == null)
                {
                    var providerKey = Membership.GetUser(System.Security.Principal.WindowsPrincipal.Current.Identity.Name).ProviderUserKey;
                    if (providerKey != null)
                        HttpContext.Current.Session[SESSION_USER_ID] = Guid.Parse(providerKey.ToString());
                }
                return (Guid)HttpContext.Current.Session[SESSION_USER_ID];
            }
            set
            {
                HttpContext.Current.Session[SESSION_USER_ID] = value;
            }
        }

        public static UserProfile User
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USER] == null)
                {
                    HttpContext.Current.Session[SESSION_USER] = UserProfile.GetProfile(System.Security.Principal.WindowsPrincipal.Current.Identity.Name);
                }
                return  (UserProfile)HttpContext.Current.Session[SESSION_USER];
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
                Roles.GetRolesForUser(System.Security.Principal.WindowsPrincipal.Current.Identity.Name)[0] ?? string.Empty;
            }
        }

        public static long? CompanyId
        {
            get
            {
                return
                    User.CompanyId;
            }
            set { User.CompanyId = value.Value; }
        }

        public static string CompanyName
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_COMPANY_NAME] == null)
                {
                    HttpContext.Current.Session[SESSION_COMPANY_NAME] = CompanyHelper.GetCompany(CompanyId.Value.ToString()).Name;
                }
                return HttpContext.Current.Session[SESSION_COMPANY_NAME].ToString();
            }
            set { HttpContext.Current.Session[SESSION_COMPANY_NAME] = value; }
        }

        public static IEnumerable<FeatureViewModel> MenuItems
        {
            get
            {
                if ( HttpContext.Current.Session[SESSION_MENU_ITEMS] == null )
                {
                    if (!System.Security.Principal.WindowsPrincipal.Current.IsInRole(UserRoles.SuperAdmin.ToString()))
                        new UserHelper().UpdateMenuItems(System.Security.Principal.WindowsPrincipal.Current.Identity.Name);
                }
                return (IEnumerable<FeatureViewModel>)HttpContext.Current.Session[SESSION_MENU_ITEMS];
            }

            set
            {
                HttpContext.Current.Session[SESSION_MENU_ITEMS] = value;
            }
        }

        public static IEnumerable<CompanyModel> CompanyList
        {
            get
            {
                return (HttpContext.Current.Session[SESSION_COMPANY_LIST] == null ? null : (List<CompanyModel>)HttpContext.Current.Session[SESSION_COMPANY_LIST]);
            }
            set
            {
                HttpContext.Current.Session[SESSION_COMPANY_LIST] = value;
            }
        }
    }
}