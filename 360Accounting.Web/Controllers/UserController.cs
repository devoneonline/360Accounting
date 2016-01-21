using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
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

        public UserController()
        {
            ////service = IoC.Resolve<IFeatureService>("FeatureService");
            service = new FeatureService(new FeatureRepository());
        }

        public ActionResult Index()
        {
            IEnumerable<Feature> featureList = service.GetAll().ToList();
            var modelList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            return View(modelList.Where(x => x.ParentId == null));
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
                ////TODO: we don't need this. do we? [FK]
                ////MembershipUser mu = Membership.GetUser(model.UserName);
                ////mu.UnlockUser();

                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ManageUser()
        {
            ManageUserViewModel model = new ManageUserViewModel();
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
        public ActionResult ManageUser(ManageUserViewModel model)
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

                    MembershipUser memUser = Membership.GetUser(userProfile.UserName);
                    if (memUser.ChangePassword(model.OldPassword, model.NewPassword))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Failure", "Unable to change password!");
                    }
                }
            }

            return View(model);
        }

        public bool CreateUser(UserCreateModel model)
        {
            if (!Roles.RoleExists(model.RoleName))
            {
                return false;
            }

            MembershipUser user = Membership.GetUser(model.Credentials.UserName);
            if (user == null)
            {
                MembershipCreateStatus status;
                user = Membership.CreateUser(model.Credentials.UserName, model.Credentials.Password, model.Email, model.PasswordQuestion, model.PasswordAnswer, true, out status);
                if (status == MembershipCreateStatus.Success && user != null)
                {
                    UserProfile up = UserProfile.GetProfile(model.Credentials.UserName);
                    up.FirstName = model.FirstName;
                    up.LastName = model.LastName;
                    up.PhoneNumber = model.PhoneNumber;
                    up.Email = model.Email;
                    up.Save();

                    if (!Roles.GetRolesForUser(model.Credentials.UserName).Contains(model.RoleName))
                    {
                        Roles.AddUserToRole(model.Credentials.UserName, model.RoleName);
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
    }
}