using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class AccountValueController : Controller
    {
        private IAccountService accountService;
        private IAccountValueService service;
        private ISetOfBookService sobService;

        public AccountValueController()
        {
            service = new AccountValueService(new AccountValueRepository());
            sobService = new SetOfBookService(new SetOfBookRepository());
            accountService = new AccountService(new AccountRepository());
        }

        public ActionResult Create()
        {
            AccValueCreateModel model = new AccValueCreateModel();
            model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            model.Segments = GetSegmentList(model.SetOfBooks
                .FirstOrDefault().Value.ToString());

            model.AccountValues = new List<AccountValueViewModel>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccValueCreateModel model)
        {
            AccountValueViewModel newModel = new AccountValueViewModel();
            newModel.ChartId = accountService
                .GetAccountBySOBId(model.SOBId.ToString()).Id;

            newModel.SetOfBook = sobService.GetSingle(model.SOBId.ToString()).Name;

            newModel.Segment = model.Segment;

            return View("Edit", newModel);
        }

        public JsonResult SegmentList(string sobId)
        {
            return Json(GetSegmentList(sobId));
        }
        
        public ActionResult Edit(long id)
        {
            AccountValueViewModel model = new 
                AccountValueViewModel(service.GetSingle(id.ToString()));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    string result = service.Update(MapModel(model));
                }
                else
                {
                    string result = service.Insert(MapModel(model));
                }

                return RedirectToAction("Create");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetSegmentList(string sobId)
        {
            Account account = accountService.GetAccountBySOBId(sobId);
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem
            {
                Text = account.SegmentName1,
                Value = account.SegmentName1,
                Selected = true
            });
            if (account.SegmentName2 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName2,
                    Value = account.SegmentName2
                });
            }

            if (account.SegmentName3 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName3,
                    Value = account.SegmentName3
                });
            }

            if (account.SegmentName4 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName4,
                    Value = account.SegmentName4
                });
            }

            if (account.SegmentName5 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName5,
                    Value = account.SegmentName5
                });
            }

            if (account.SegmentName6 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName6,
                    Value = account.SegmentName6
                });
            }

            if (account.SegmentName7 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName7,
                    Value = account.SegmentName7
                });
            }

            if (account.SegmentName8 != null)
            {
                lst.Add(new SelectListItem
                {
                    Text = account.SegmentName8,
                    Value = account.SegmentName8
                });
            }

            return lst;
        }

        private AccountValue MapModel(AccountValueViewModel model)            ////TODO: this should be done in service will discuss later - FK
        {
            return new AccountValue
            {
                AccountType = model.AccountType,
                ChartId = model.ChartId,
                CreateDate = DateTime.Now,
                EndDate = model.EndDate,
                Id = model.Id,
                Levl = model.Levl,
                Segment = model.Segment,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now,
                Value = model.Value,
                ValueName = model.ValueName
            };
        }
    }
}