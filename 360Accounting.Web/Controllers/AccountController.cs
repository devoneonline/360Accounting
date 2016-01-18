using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class AccountController : AsyncController
    {
        private IAccountService service;

        public  AccountController()
        {
            ////service = IoC.Resolve<IAccountService>("AccountService");
            service = new AccountService(new AccountRepository());
        }

        public ActionResult Create()
        {
            AccountViewModel model = new AccountViewModel();
            UserProfile userProfile = UserProfile.GetProfile(User.Identity.Name);
            if (userProfile != null)
            {
                model.CompanyId = userProfile.CompanyId;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = UserProfile
                    .GetProfile(User.Identity.Name);
                if (userProfile != null)
                {
                    model.CompanyId = userProfile.CompanyId;
                    string result = service.Insert(new Account
                        {
                            CompanyId = model.CompanyId,
                        });
                }
            }

            return View(new AccountViewModel());
        }
    }
}