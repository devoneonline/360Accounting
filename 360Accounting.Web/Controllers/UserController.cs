using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using DevExpress.Web.Mvc;
using _360Accounting.Web.Reports;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class UserController : AsyncController
    {
        #region Declaration
        private IFeatureService service;
        private ICompanyService companyService;
        private IFeatureSetService featureSetService;
        private IFeatureSetListService featureSetListService;
        private IFeatureSetAccessService featureSetAccessService;
        #endregion

        #region Constructor
        public UserController()
        {
            service = IoC.Resolve<IFeatureService>("FeatureService");
            companyService = IoC.Resolve<ICompanyService>("CompanyService");
            featureSetService = IoC.Resolve<IFeatureSetService>("FeatureSetService");
            featureSetListService = IoC.Resolve<IFeatureSetListService>("FeatureSetListService");
            featureSetAccessService = IoC.Resolve<IFeatureSetAccessService>("FeatureSetAccessService");
        }
        #endregion

        #region ActionResult

        #region Uzair Reports Code

        #region Code for getting reports data
        private UserwiseRole CreateUserwiseRoleReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserwiseRole report = new UserwiseRole();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            return report;
        }

        private UserList CreateReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserList report = new UserList();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = companyService
                .GetSingle(AuthenticationHelper.User.CompanyId.ToString(),
                AuthenticationHelper.User.CompanyId).Name;
            return report;
        }

        private List<UserViewModel> GetUserList()
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

        public ActionResult UserwiseRole()
        {
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult UserwiseRolePartial()
        {
            return PartialView("_UserwiseRole", CreateUserwiseRoleReport());
        }

        public ActionResult DocumentViewerPartial()
        {
            return PartialView("_UserList", CreateReport());
        }

        public ActionResult UserwiseRolePartialExport()
        {
            return DocumentViewerExtension.ExportTo(CreateUserwiseRoleReport(), Request);
        }

        public ActionResult DocumentViewerPartialExport()
        {
            return DocumentViewerExtension.ExportTo(CreateReport(), Request);
        }
        
        #endregion

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserListPartial()
        {
            List<UserViewModel> modelList = new List<UserViewModel>();
            MembershipUserCollection memCollection = Membership.GetAllUsers();
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
                item.CompanyId = profile.CompanyId;
                item.CompanyName = companyService.GetSingle(profile.CompanyId.ToString(), AuthenticationHelper.User.CompanyId).Name;
                item.Role = Roles.GetRolesForUser(user.UserName)[0];
                modelList.Add(item);
            }
            if (AuthenticationHelper.UserRole != UserRoles.SuperAdmin.ToString())
            {
                modelList = modelList.Where(x => x.CompanyId == AuthenticationHelper.User.CompanyId && x.Role != UserRoles.SuperAdmin.ToString()).ToList();
            }
            return PartialView("_List", modelList);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    if (Roles.IsUserInRole(model.UserName, UserRoles.SuperAdmin.ToString()))
                    {
                        getSuperAdminMenu();
                    }
                    else
                    {
                        updateMenuItems(model.UserName);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
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

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var memUser = Membership.GetUser(id);
            Membership.DeleteUser(memUser.UserName);
            return RedirectToAction("Index");
        }

        #endregion

        public ActionResult CreateCompanyFeatureList()
        {
            IEnumerable<Feature> featureList = service.GetAll(long.MaxValue).ToList();        ////here company argument doesn't require
            IEnumerable<Company> companyList = companyService.GetAll(long.MaxValue);

            CreateCompanyFeatureListModel model = new CreateCompanyFeatureListModel();
            model.FeatureList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            model.CompanyList = companyList.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return View(model);
        }

        public JsonResult InsertCompanyFeature(long companyId, string featureName, string featureList)
        {
            FeatureSet fs = new FeatureSet();
            fs.Name = featureName;
            fs.AccessType = "company";
            fs.CreateDate = DateTime.Now;
            List<FeatureSetList> fsList = new List<FeatureSetList>();
            var newValue = featureList.Replace("undefined|", "").Replace("undefined±", "").Split(new char[] { '±' }, StringSplitOptions.None);
            foreach (string s in newValue)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    fsList.Add(new FeatureSetList { FeatureId = long.Parse(s.Split(new char[] { '|' }, StringSplitOptions.None)[0]), FeatureSetId = fs.Id });
                }
            }

            fs.FeatureSetList = fsList;

            FeatureSetAccess fsa = new FeatureSetAccess();
            fsa.CompanyId = companyId;
            fsa.FeatureSetId = fs.Id;
            fsa.CreateDate = DateTime.Now;

            service.InsertCompanyFeatureSet(fs, fsa);

            return Json("Success");
        }

        public ActionResult FeatureSet(FeatureSetListModel model)
        {
            int totalRecords = 0;
            model.FeatureSet = featureSetService.GetAll(long.MaxValue).Select(x => new FeatureSetModel(x)).ToList();
            model.TotalRecords = totalRecords;
            return View(model);
        }

        public ActionResult EditCompanyFeatureList(string id)
        {
            CreateCompanyFeatureListModel model = new CreateCompanyFeatureListModel();

            FeatureSet featureSet = featureSetService.GetSingle(id, AuthenticationHelper.User.CompanyId);            
            Company company = companyService.GetSingle(AuthenticationHelper.User.CompanyId.ToString(), AuthenticationHelper.User.CompanyId);
            model.CompanyId = company.Id;
            model.CompanyList = new List<SelectListItem>();     //This line is added because without initializing it was giving error.
            model.CompanyList.Add(new SelectListItem { Text = company.Name, Value = company.Id.ToString() });
            IEnumerable<Feature> featureList = service.GetAll(AuthenticationHelper.User.CompanyId).ToList();
            model.FeatureList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            model.Id = featureSet.Id;
            model.Name = featureSet.Name;
            model.SelectedFeatures = featureSetListService.GetByFeatureSetId(Convert.ToInt32(id)).ToDictionary(a => a.FeatureId, a => a.FeatureId); //ask faisal bhai what does ToDictionary do.
            return View(model);
        }

        public JsonResult UpdateCompanyFeature(long id, long companyId, string featureName, string featureList)
        {
            FeatureSet fs = new FeatureSet();
            fs.Name = featureName;
            fs.Id = id;
            fs.AccessType = "company";
            fs.CreateDate = DateTime.Now;
            List<FeatureSetList> fsList = new List<FeatureSetList>();
            var newValue = featureList.Replace("undefined|", "").Replace("undefined±", "").Split(new char[] { '±' }, StringSplitOptions.None);
            foreach (string s in newValue)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    fsList.Add(new FeatureSetList { FeatureId = long.Parse(s.Split(new char[] { '|' }, StringSplitOptions.None)[0]), FeatureSetId = fs.Id });
                }
            }

            fs.FeatureSetList = fsList;

            service.UpdateCompanyFeatureSet(fs, featureSetListService.GetByFeatureSetId(id));

            return Json("Success");
        }

        public ActionResult DeleteFeatureSet(string id)
        {
            featureSetService.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        #region Feature Set Access
        public ActionResult UserFeatureSet(string featureSetId)
        {
            ViewBag.FeatureSetId = featureSetId;
            //TODO: Is this the way to get users??

            List<SelectUser> modelList = new List<SelectUser>();
            MembershipUserCollection memCollection = Membership.GetAllUsers();
            foreach (MembershipUser user in memCollection)
            {
                UserProfile profile = UserProfile.GetProfile(user.UserName);
                SelectUser item = new SelectUser();
                item.UserId = Guid.Parse(user.ProviderUserKey.ToString());
                item.UserName = user.UserName;
                item.CompanyId = profile.CompanyId;
                item.Selected = featureSetAccessService.GetSingle(AuthenticationHelper.User.CompanyId, user.ProviderUserKey.ToString()) == null ? false : true;
                item.Role = Roles.GetRolesForUser(user.UserName)[0];
                modelList.Add(item);
            }
            if (AuthenticationHelper.UserRole != UserRoles.SuperAdmin.ToString())
            {
                modelList = modelList.Where(x => x.CompanyId == AuthenticationHelper.User.CompanyId && x.Role != UserRoles.SuperAdmin.ToString()).ToList();
            }

            return View("CheckUsersPartial", modelList);
        }

        public ActionResult SaveFSforUsers(string featureSetId, string userList)
        {
            List<string> UserList = userList.Split(new char[] { '±' }).ToList();

            //Delete
            List<FeatureSetAccess> tobeRemoved = featureSetAccessService.GetAll(AuthenticationHelper.User.CompanyId).Where(rec => rec.UserId != null).ToList();
            if (tobeRemoved.Count() > 0)
            {
                foreach (var item in tobeRemoved)
                {
                    featureSetAccessService.Delete(item.Id.ToString(), Convert.ToInt64(item.CompanyId));
                }
            }

            foreach (var item in UserList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    featureSetAccessService.Insert(new FeatureSetAccess
                    {
                        CompanyId = AuthenticationHelper.User.CompanyId,
                        FeatureSetId = Convert.ToInt64(featureSetId),
                        UserId = Guid.Parse(item),
                        CreateDate = DateTime.Now
                    });
                }
            }

            return Json("Success");
        }
        #endregion

        #region Helper Methods

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
                    if (AuthenticationHelper.UserRole != UserRoles.SuperAdmin.ToString())
                        up.CompanyId = AuthenticationHelper.User.CompanyId;
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

        private void getSuperAdminMenu()
        {
            AuthenticationHelper.MenuItems = service.GetSuperAdminMenu().Select(x => new FeatureViewModel(x)).ToList();
        }

        private void updateMenuItems(string userName)
        {
            var memUser = Membership.GetUser(userName);
            if (memUser != null)
            {
                List<Feature> featureList = service.GetMenuItemsByUserId(Guid.Parse(memUser.ProviderUserKey.ToString())).ToList();
                var modelList = featureList.Select(x => new FeatureViewModel(x)).ToList();
                AuthenticationHelper.MenuItems = modelList;
            }
        }

        #endregion
    }
}