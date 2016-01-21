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
        private ISetOfBookService sobService;

        public AccountController()
        {
            ////service = IoC.Resolve<IAccountService>("AccountService");
            service = new AccountService(new AccountRepository());
            sobService = new SetOfBookService(new SetOfBookRepository());
        }

        public ActionResult Index()
        {
            var list = service.GetAll();
            AccountListModel model = new AccountListModel();
            model.Accounts = list.Select(a => new AccountViewModel(a)).ToList();

            return PartialView("_List", model);
        }

        public ActionResult Create()
        {
            AccountViewModel model = new AccountViewModel();

            UserProfile userProfile = UserProfile
                .GetProfile(User.Identity.Name);

            model.SetOfBooks = sobService.GetAll()
                .Where(x => x.CompanyId == userProfile.CompanyId).ToList();

            return View(model);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = UserProfile
                    .GetProfile(User.Identity.Name);
                if (userProfile != null)
                {
                    model.CompanyId = userProfile.CompanyId;
                }

                string result = service.Insert(MapModel(model));
                
                model = new AccountViewModel();
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            AccountViewModel model = new AccountViewModel(service.GetSingle(id));

            UserProfile userProfile = UserProfile
                .GetProfile(User.Identity.Name);

            model.SetOfBooks = sobService.GetAll()
                .Where(x => x.CompanyId == userProfile.CompanyId &&
                    x.Id == model.SOBId).ToList();
            return View(model);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Edit(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = UserProfile
                    .GetProfile(User.Identity.Name);
                if (userProfile != null)
                {
                    model.CompanyId = userProfile.CompanyId;
                }

                string result = service.Update(MapModel(model));
            }

            return View(new AccountViewModel());
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        private Account MapModel(AccountViewModel model)
        {
            return new Account
            {
                CompanyId = model.CompanyId,
                CreateDate = DateTime.Now,
                SegmentChar1 = model.SegmentChar1,
                SegmentChar2 = model.SegmentChar2,
                SegmentChar3 = model.SegmentChar3,
                SegmentChar4 = model.SegmentChar4,
                SegmentChar5 = model.SegmentChar5,
                SegmentChar6 = model.SegmentChar6,
                SegmentChar7 = model.SegmentChar7,
                SegmentChar8 = model.SegmentChar8,
                SegmentEnabled1 = model.SegmentEnabled1,
                SegmentEnabled2 = model.SegmentEnabled2,
                SegmentEnabled3 = model.SegmentEnabled3,
                SegmentEnabled4 = model.SegmentEnabled4,
                SegmentEnabled5 = model.SegmentEnabled5,
                SegmentEnabled6 = model.SegmentEnabled6,
                SegmentEnabled7 = model.SegmentEnabled7,
                SegmentEnabled8 = model.SegmentEnabled8,
                SegmentName1 = model.SegmentName1,
                SegmentName2 = model.SegmentName2,
                SegmentName3 = model.SegmentName3,
                SegmentName4 = model.SegmentName4,
                SegmentName5 = model.SegmentName5,
                SegmentName6 = model.SegmentName6,
                SegmentName7 = model.SegmentName7,
                SegmentName8 = model.SegmentName8,
                SOBId = model.SOBId,
                UpdateDate = DateTime.Now
            };
        }
    }
}