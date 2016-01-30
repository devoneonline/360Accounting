﻿using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class UserController : AsyncController
    {
        private IFeatureService service;
        private ICompanyService companyService;

        public UserController()
        {
            ////service = IoC.Resolve<IFeatureService>("FeatureService");
            service = new FeatureService(new FeatureRepository());
            companyService = new CompanyService(new CompanyRepository());
        }

        public ActionResult Index(MembershipUserListModel model)
        {
            int totalRecords = 0;
            MembershipUserCollection memCollection = Membership.GetAllUsers(model.Page ?? 0, Utility.Configuration.GridRows, out totalRecords);
            model.Users = new List<UserViewModel>();
            foreach (MembershipUser user in memCollection)
            {
                UserProfile profile = UserProfile.GetProfile(user.UserName);
                UserViewModel item = new UserViewModel();
                item.UserId = Guid.Parse(user.ProviderUserKey.ToString());
                item.UserName = user.UserName;
                item.FirstName = profile.FirstName;
                item.LastName = profile.LastName;
                item.PhoneNumber = profile.PhoneNumber;
                item.Email = profile.Email;
                item.CompanyName = companyService.GetSingle(profile.CompanyId.ToString()).Name;
                item.Role = Roles.GetRolesForUser(user.UserName)[0];
                model.Users.Add(item);
            }
            model.TotalRecords = totalRecords;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    updateMenuItems();
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ManageUser()
        {
            UserProfileViewModel model = new UserProfileViewModel();
            UserProfile userProfile = UserProfile.GetProfile(User.Identity.Name);
            if (userProfile != null)
            {
                model.CompanyId = userProfile.CompanyId;
                model.Email = userProfile.Email;
                model.FirstName = userProfile.FirstName;
                model.LastName = userProfile.LastName;
                model.PhoneNumer = userProfile.PhoneNumber;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUser(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = UserProfile.GetProfile(User.Identity.Name);
                if (userProfile != null)
                {
                    userProfile.FirstName = model.FirstName;
                    userProfile.LastName = model.LastName;
                    userProfile.PhoneNumber = model.PhoneNumer;
                    userProfile.Email = model.Email;
                    userProfile.Save();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new UserCreateModel());
        }

        [HttpPost]
        public ActionResult Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.CreateUser(model))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        public ActionResult Edit(Guid id)
        {
            var memUser = Membership.GetUser(id);
            var userProfile = UserProfile.GetProfile(memUser.UserName);
            UserCreateModel model = new UserCreateModel();
            model.UserName = memUser.UserName;
            model.FirstName = userProfile.FirstName;
            model.LastName = userProfile.LastName;
            model.PhoneNumber = userProfile.PhoneNumber;
            model.Email = userProfile.Email;
            model.RoleName = Roles.GetRolesForUser(model.UserName)[0];
            model.CompanyId = userProfile.CompanyId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.UpdateUser(model))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser memUser = Membership.GetUser(AuthenticationHelper.User.UserName);
                memUser.ChangePassword(model.OldPassword, model.NewPassword);
                return RedirectToAction("Settings");
            }
            return View(model);
        }

        public bool CreateUser(UserCreateModel model)
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
                    up.CompanyId = model.CompanyId.Value;
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

        public bool CreateRole(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
                return true;
            }

            return false;
        }

        public bool UpdateUser(UserCreateModel model)
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
                if (model.RoleName == UserRoles.SuperAdmin.ToString())
                    up.CompanyId = 0;
                else
                    up.CompanyId = model.CompanyId ?? 0;
                up.Save();

                if (!Roles.GetRolesForUser(model.UserName).Contains(model.RoleName))
                {
                    Roles.AddUserToRole(model.UserName, model.RoleName);
                }

                return true;
            }

            return false;
        }

        public ActionResult Settings()
        {
            return View();
        }

        private void updateMenuItems()
        {
            IEnumerable<Feature> featureList = service.GetAll().ToList();
            var modelList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            AuthenticationHelper.MenuItems = modelList.Where(x => x.ParentId == null);
        }

    }
}