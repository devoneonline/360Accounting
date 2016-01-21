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

            UserProfile userProfile = UserProfile
                .GetProfile(User.Identity.Name);

            model.SetOfBooks = sobService.GetAll()
                .Where(x => x.CompanyId == userProfile.CompanyId).ToList();

            model.Segments = GetSegmentList(model.SetOfBooks
                .FirstOrDefault().Id.ToString());

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccValueCreateModel model)
        {
            AccountValueViewModel newModel = new AccountValueViewModel();
            newModel.ChartId = accountService
                .GetAccountBySOBId(model.SetOfBooks.ToString()).Id;

            newModel.Segment = model.Segments.ToString();

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
    }
}