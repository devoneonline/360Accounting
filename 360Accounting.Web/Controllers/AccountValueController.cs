using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
            service = IoC.Resolve<IAccountValueService>("AccountValueService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            accountService = IoC.Resolve<IAccountService>("AccountService");
        }

        public ActionResult Index(long id, AccountValueListModel model)
        {
            //if (model.SetOfBooks == null)
            //{
            //    model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
            //        .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            //}

            model.SOBId = id;
            model.Segments = getSegmentList(model.SOBId.ToString());
            //if (model.Segments == null && model.SetOfBooks.Any())
            //{
            //    model.Segments = getSegmentList(model.SetOfBooks.First().Value.ToString());
            //}

            model.AccountValues = service.GetAll(AuthenticationHelper.User.CompanyId).Select(x => new AccountValueViewModel(x)).ToList();
            return View(model);
        }

        public ActionResult Create(long sobId, string segment)
        {
            AccountValueViewModel model = new AccountValueViewModel();
            if (accountService.GetAccountBySOBId(sobId.ToString(), AuthenticationHelper.User.CompanyId) != null)
            {
                model.ChartId = accountService.GetAccountBySOBId(sobId.ToString(), AuthenticationHelper.User.CompanyId).Id;
                model.SetOfBook = sobService.GetSingle(sobId.ToString(),AuthenticationHelper.User.CompanyId).Name;
                model.Segment = segment;
                Session["sobid"] = sobId;   //TODO:: temporary
                return View("Edit", model);
            }

            return RedirectToAction("Index");
        }

        public JsonResult SegmentList(string sobId)
        {
            return Json(getSegmentList(sobId), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(long id)
        {
            AccountValueViewModel model = new 
                AccountValueViewModel(service.GetSingle(id.ToString(),AuthenticationHelper.User.CompanyId));
            model.SetOfBook = sobService.GetSingle(accountService.GetSingle(model.ChartId.ToString(), AuthenticationHelper.User.CompanyId).SOBId.ToString(), AuthenticationHelper.User.CompanyId).Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    string result = service.Update(mapModel(model));
                    return RedirectToAction("Index");
                }
                else
                {
                    string result = service.Insert(mapModel(model));
                    return RedirectToAction("Index", new { id = Session["sobid"] });
                }
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        #region Private Methods
        private List<SelectListItem> getSegmentList(string sobId)
        {
            Account account = accountService.GetAccountBySOBId(sobId, AuthenticationHelper.User.CompanyId);
            var lst = new List<SelectListItem>();
            if (account != null)
            {
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
            }

            return lst;   
        }

        private AccountValue mapModel(AccountValueViewModel model)            ////TODO: this should be done in service will discuss later - FK
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
        #endregion

        public ActionResult AccountValuesPartial(long sobId, string segment)
        {
            IEnumerable<AccountValueViewModel> accountValuesList = service.GetAll(AuthenticationHelper.User.CompanyId).Select(x => new AccountValueViewModel(x));
            return PartialView("_List", accountValuesList);
        }

    }
}