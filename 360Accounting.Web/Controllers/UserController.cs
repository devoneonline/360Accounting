using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using _360Accounting.Web.Reports;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
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

        #region Reports

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

        #region Login

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
                        return RedirectToAction("Index", "Company");
                    }
                    else
                    {
                        AuthenticationHelper.MenuItems = null;
                        UpdateMenuItems(model.UserName);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        #endregion

        #region ActionResult

        public ActionResult Index(long? Id, string message = "")
        {
            ViewBag.ErrorMessage = message;
            ViewBag.CompanyId = Id;
            if (Id.HasValue)
            {
                AuthenticationHelper.CompanyId = Id;
                AuthenticationHelper.CompanyName = AuthenticationHelper.CompanyList.Where(x => x.Id == Id.Value).Select(x => x.Name).FirstOrDefault();
            }

            return View();
        }

        public ActionResult UserListPartial()
        {
            List<UserViewModel> modelList = new List<UserViewModel>();
            MembershipUserCollection memCollection = Membership.GetAllUsers();
            foreach (MembershipUser user in memCollection)
            {
                UserProfile profile = UserProfile.GetProfile(user.UserName);
                if (profile.CompanyId == AuthenticationHelper.CompanyId.Value)
                {
                    UserViewModel item = new UserViewModel();
                    item.UserId = Guid.Parse(user.ProviderUserKey.ToString());
                    item.UserName = user.UserName;
                    item.FirstName = profile.FirstName;
                    item.LastName = profile.LastName;
                    item.PhoneNumber = profile.PhoneNumber;
                    item.Email = profile.Email;
                    item.CompanyId = profile.CompanyId;
                    item.CompanyName = companyService.GetSingle(profile.CompanyId.ToString(), AuthenticationHelper.CompanyId.Value).Name;
                    item.Role = Roles.GetRolesForUser(user.UserName)[0];
                    modelList.Add(item);
                }
            }
            return PartialView("_List", modelList);
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
            List<SelectListItem> fsList = featureSetService.GetAll(AuthenticationHelper.CompanyId.Value, AuthenticationHelper.UserRole).Select(x => new SelectListItem { Text = x.Name, Value =x.Id.ToString() }).ToList();
            UserCreateModel model = new UserCreateModel(fsList);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                if (CreateUser(model))
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
                if (UpdateUser(model))
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
            try
            {
                var memUser = Membership.GetUser(id);
                Membership.DeleteUser(memUser.UserName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult CreateCompanyFeatureList()
        {
            var featureList = service.GetAll(AuthenticationHelper.CompanyId.Value, AuthenticationHelper.UserRole, "company").ToList();
            CreateCompanyFeatureListModel model = new CreateCompanyFeatureListModel();
            model.CompanyId = AuthenticationHelper.CompanyId.Value;
            model.FeatureList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            return View(model);
        }

        public ActionResult FeatureSet(long Id, string message = "")
        {
            ViewBag.ErrorMessage = message;
            int totalRecords = 0;

            FeatureSetListModel model = new FeatureSetListModel();
            AuthenticationHelper.CompanyId = Id;
            AuthenticationHelper.CompanyName = AuthenticationHelper.CompanyList.Where(x => x.Id == Id).Select(x => x.Name).FirstOrDefault();
            model.FeatureSet = featureSetService.GetAll(Id, AuthenticationHelper.UserRole).Select(x => new FeatureSetModel(x)).ToList();
            model.TotalRecords = totalRecords;
            return View(model);
        }

        public ActionResult EditCompanyFeatureList(string id)
        {
            CreateCompanyFeatureListModel model = new CreateCompanyFeatureListModel();

            FeatureSet featureSet = featureSetService.GetSingle(id, AuthenticationHelper.CompanyId.Value);
            model.CompanyId = AuthenticationHelper.CompanyId.Value;

            var featureList = service.GetAll(AuthenticationHelper.CompanyId.Value, AuthenticationHelper.UserRole, featureSet.AccessType).ToList();
            model.FeatureList = featureList.Select(x => new FeatureViewModel(x)).ToList();
            model.Id = featureSet.Id;
            model.Name = featureSet.Name;
            model.SelectedFeatures = featureSetListService.GetByFeatureSetId(Convert.ToInt32(id)).ToDictionary(a => a.FeatureId, a => a.FeatureId);
            return View(model);
        }

        public ActionResult DeleteFeatureSet(string id)
        {
            try
            {
                featureSetService.Delete(id, AuthenticationHelper.CompanyId.Value);
                return RedirectToAction("FeatureSet");
            }
            catch (Exception ex)
            {
                return RedirectToAction("FeatureSet", new { message = ex.Message });
            }
        }

        public ActionResult UserFeatureSet(long id)
        {
            var model = AccountHelper.UserFeatureSet(id.ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult UserFeatureSet(FeatureSetAccessModel model)
        {
            var UserList = model.SelectedUsers;
            List<FeatureSetAccess> tobeRemoved = featureSetAccessService.GetAll(AuthenticationHelper.CompanyId.Value).Where(rec => rec.UserId != null).ToList();
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
                        CompanyId = AuthenticationHelper.CompanyId.Value,
                        FeatureSetId = Convert.ToInt64(model.FeatureSetId),
                        UserId = Guid.Parse(item),
                        CreateDate = DateTime.Now
                    });
                }
            }
            return RedirectToAction("FeatureSet");
        }

        public ActionResult SaveFSforUsers(string featureSetId, string userList)
        {
            List<string> UserList = userList.Split(new char[] { '±' }).ToList();

            //Delete
            List<FeatureSetAccess> tobeRemoved = featureSetAccessService.GetAll(AuthenticationHelper.CompanyId.Value).Where(rec => rec.UserId != null).ToList();
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
                        CompanyId = AuthenticationHelper.CompanyId.Value,
                        FeatureSetId = Convert.ToInt64(featureSetId),
                        UserId = Guid.Parse(item),
                        CreateDate = DateTime.Now
                    });
                }
            }

            return Json("Success");
        }

        #endregion

        #region JsonResult

        public JsonResult InsertCompanyFeature(long companyId, string featureName, string featureList)
        {
            #region Feature Set

            FeatureSet fs = new FeatureSet();
            fs.Name = featureName;
            fs.AccessType = AuthenticationHelper.UserRole == UserRoles.SuperAdmin.ToString() ? "company" : "user";
            fs.CompanyId = companyId;
            fs.CreateDate = DateTime.Now;
            fs.CreateBy = AuthenticationHelper.UserId;

            #region FeatureSet List
            List<FeatureSetList> fsList = new List<FeatureSetList>();
            var newValue = featureList.Replace("undefined|", "").Replace("undefined±", "").Split(new char[] { '±' }, StringSplitOptions.None);
            foreach (string s in newValue)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    fsList.Add(new FeatureSetList { FeatureId = long.Parse(s.Split(new char[] { '|' }, StringSplitOptions.None)[0]), FeatureSetId = fs.Id });
                }
            }
            #endregion

            fs.FeatureSetList = fsList;

            #endregion

            #region Feature Set Access

            FeatureSetAccess fsa = new FeatureSetAccess();
            fsa.CompanyId = companyId;
            fsa.FeatureSetId = fs.Id;
            fsa.CreateDate = DateTime.Now;

            service.InsertCompanyFeatureSet(fs, fsa);

            #endregion

            return Json("Success");
        }

        public JsonResult UpdateCompanyFeature(long id, long companyId, string featureName, string featureList)
        {
            FeatureSet fs = new FeatureSet();
            fs.Name = featureName;
            fs.Id = id;
            fs.CompanyId = companyId;
            fs.UpdateBy = AuthenticationHelper.UserId;
            fs.UpdateDate = DateTime.Now;

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

        #endregion

        #region Methods

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
                    up.CompanyId = AuthenticationHelper.CompanyId.Value;
                    up.Save();

                    if (!Roles.GetRolesForUser(model.UserName).Contains(model.RoleName))
                    {
                        Roles.AddUserToRole(model.UserName, model.RoleName);
                    }


                    featureSetAccessService.Insert(new FeatureSetAccess { CompanyId = AuthenticationHelper.CompanyId.Value, CreateBy= AuthenticationHelper.UserId, CreateDate = DateTime.Now, FeatureSetId = model.FeatureSetId, UserId = Guid.Parse(user.ProviderUserKey.ToString()) });

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

        internal void GetSuperAdminMenu()
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

        internal UserwiseRole CreateUserwiseRoleReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserwiseRole report = new UserwiseRole();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = AuthenticationHelper.CompanyName;
            return report;
        }

        internal UserList CreateReport()
        {
            List<UserViewModel> modelList = GetUserList();
            UserList report = new UserList();
            report.DataSource = modelList;
            report.Parameters["CompanyName"].Value = AuthenticationHelper.CompanyName;
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

        #endregion        
    }
}