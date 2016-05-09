using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using _360Accounting.Web.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _360Accounting.Web.Helpers
{
    public class UserHelper
    {
        #region Declaration

        private static IFeatureService service;

        #endregion

        #region Constructor

        static UserHelper()
        {
            service = IoC.Resolve<IFeatureService>("FeatureService");
        }

        #endregion

        #region Methods

        public static bool CreateUser(UserCreateModel model)
        {
            if (!Roles.RoleExists(model.RoleName))
            {
                return false;
            }

            MembershipUser user = Membership.GetUser(model.UserName);
            if (user == null)
            {
                MembershipCreateStatus status;
                user = Membership.CreateUser(model.UserName, model.Password, model.Email, model.PasswordQuestion, model.PasswordAnswer, true, out status);
                if (status == MembershipCreateStatus.Success && user != null)
                {
                    UserProfile up = UserProfile.GetProfile(model.UserName);
                    up.FirstName = model.FirstName;
                    up.LastName = model.LastName;
                    up.PhoneNumber = model.PhoneNumber;
                    up.Email = model.Email;
                    up.CompanyId = AuthenticationHelper.CompanyId.Value;
                    up.Save();

                    if (!Roles.GetRolesForUser(model.UserName).Contains(model.RoleName))
                    {
                        Roles.AddUserToRole(model.UserName, model.RoleName);
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool CreateRole(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
                return true;
            }

            return false;
        }

        public static bool UpdateUser(UserCreateModel model)
        {
            if (!Roles.RoleExists(model.RoleName))
            {
                return false;
            }

            MembershipUser user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                UserProfile up = UserProfile.GetProfile(model.UserName);
                up.FirstName = model.FirstName;
                up.LastName = model.LastName;
                up.PhoneNumber = model.PhoneNumber;
                up.Email = model.Email;
                up.CompanyId = model.CompanyId.Value;
                up.Save();
                if (!Roles.GetRolesForUser(model.UserName).Contains(model.RoleName))
                {
                    Roles.AddUserToRole(model.UserName, model.RoleName);
                }
                return true;
            }

            return false;
        }

        internal static void GetSuperAdminMenu()
        {
            AuthenticationHelper.MenuItems = service.GetSuperAdminMenu().Select(x => new FeatureViewModel(x)).ToList();
        }

        internal void UpdateMenuItems(string userName)
        {
            var memUser = Membership.GetUser(userName);
            if (memUser != null)
            {
                List<Feature> featureList = service.GetMenuItemsByUserId(Guid.Parse(memUser.ProviderUserKey.ToString())).ToList();
                var modelList = featureList.Select(x => new FeatureViewModel(x)).ToList();
                AuthenticationHelper.MenuItems = modelList;
            }
        }

        internal static UserwiseRole CreateUserwiseRoleReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserwiseRole report = new UserwiseRole();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = AuthenticationHelper.CompanyName;
            return report;
        }

        internal static UserList CreateReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserList report = new UserList();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = AuthenticationHelper.CompanyName;
            return report;
        }

        private static List<UserViewModel> GetUserList()
        {
            MembershipUserCollection memCollection = Membership.GetAllUsers();
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (MembershipUser user in memCollection)
            {
                UserViewModel item = new UserViewModel();
                item.UserId = Guid.Parse(user.ProviderUserKey.ToString());
                item.UserName = user.UserName;
                item.Role = Roles.GetRolesForUser(user.UserName)[0];
                users.Add(item);
            }

            return users;
        }

        #endregion

    }
}