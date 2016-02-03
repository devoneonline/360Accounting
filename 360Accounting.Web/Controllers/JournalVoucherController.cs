using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using _360Accounting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class JournalVoucherController : Controller
    {
        private IJournalVoucherService service;
        private ISetOfBookService sobService;
        private ICurrencyService currencyService;
        private ICalendarService calendarService;
        private ICodeCombinitionService codeCombinitionService;

        public JournalVoucherController()
        {
            service = IoC.Resolve<IJournalVoucherService>("JournalVoucherService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public ActionResult GetJournalVoucherList(string sobId, string periodId, string currencyId)
        {
            JournalVoucherListModel model = new JournalVoucherListModel();
            model.SOBId = Convert.ToInt32(sobId);
            model.PeriodId = Convert.ToInt32(periodId);
            model.CurrencyId = Convert.ToInt32(currencyId);
            model.JournalVouchers = getJournalVouchers(model);
            return PartialView("_List", model);
        }

        public ActionResult Edit(long id)
        {
            JournalVoucherViewModel model = 
                new JournalVoucherViewModel(service.GetSingle(id.ToString(), 
                    AuthenticationHelper.User.CompanyId));
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            SessionHelper.JournalVoucher = model;

            return View(new JournalVoucherCreateModel
            {
                ConversionRate = model.ConversionRate,
                CurrencyId = model.CurrencyId,
                CurrencyName = currencyService.GetSingle(model.CurrencyId.ToString(), AuthenticationHelper.User.CompanyId).Name,
                Description = model.Description,
                DocumentNo = model.DocumentNo,
                GLDate = model.GLDate,
                HeaderId = model.Id,
                JournalName = model.JournalName,
                PeriodId = model.PeriodId,
                PeriodName = calendarService.GetSingle(model.PeriodId.ToString(), AuthenticationHelper.User.CompanyId).PeriodName,
                SOBId = model.SOBId,
                SOBName = sobService.GetSingle(model.SOBId.ToString(), AuthenticationHelper.User.CompanyId).Name
            });
                
        }

        [HttpPost]
        public ActionResult Create(JournalVoucherCreateModel model)
        {
            if (ModelState.IsValid)
            {
                long result = saveJournalVoucher(model);
                if (result > 0)
                {
                    return RedirectToAction("Edit", new { id = result });
                }
            }
            return View(model);
        }

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            JournalVoucherCreateModel model = new JournalVoucherCreateModel();
            model.SOBId = sobId;
            model.PeriodId = periodId;
            model.CurrencyId = currencyId;
            model.SOBName = sobService.GetSingle(sobId.ToString(), AuthenticationHelper.User.CompanyId).Name;
            model.PeriodName = calendarService.GetSingle(periodId.ToString(), AuthenticationHelper.User.CompanyId).PeriodName;
            model.CurrencyName = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.User.CompanyId).Name;
            model.CodeCombinationList = codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId, "", false, null, "", "")
                .Select(x => new SelectListItem 
                {
                    Text = x.CodeCombinitionCode,
                    Value = x.Id.ToString()
                }).ToList();
            return View(model);
        }

        public JsonResult CurrencyList(string sobId)
        {
            return Json(getCurrencyList(sobId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList(string sobId)
        {
            return Json(getPeriodList(sobId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(JournalVoucherListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService.GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            if (model.Periods == null && model.SetOfBooks.Any())
            {
                model.Periods = getPeriodList(model.SetOfBooks.First().Value);
            }

            if (model.Currencies == null && model.SetOfBooks.Any())
            {
                model.Currencies = getCurrencyList(model.SetOfBooks.First().Value);
            }

            //model.JournalVouchers = getJournalVouchers(model);
            
            return View(model);
        }

        #region Private Methods
        private long saveJournalVoucher(JournalVoucherCreateModel model)
        {
            //make model
            JournalVoucher entity = new JournalVoucher();
            entity.CompanyId = AuthenticationHelper.User.CompanyId;
            entity.ConversionRate = model.ConversionRate;
            entity.CreateDate = DateTime.Now;
            entity.CurrencyId = model.CurrencyId;
            entity.Description = model.Description;
            entity.DocumentNo = model.DocumentNo;
            entity.GLDate = model.GLDate;
            entity.Id = model.HeaderId;
            entity.JournalName = model.JournalName;
            entity.PeriodId = model.PeriodId;
            entity.PostingFlag = codeCombinitionService.GetSingle(model.CodeCombinationId.ToString(), AuthenticationHelper.User.CompanyId).AllowedPosting;
            entity.SOBId = model.SOBId;
            entity.UpdateDate = DateTime.Now;

            entity.JournalVoucherDetail = new List<JournalVoucherDetail>()
                .Select(x => new JournalVoucherDetail 
                { 
                    AccountedCr = model.AccountedCr,
                    AccountedDr = model.AccountedDr,
                    CodeCombinationId = model.CodeCombinationId,
                    CreateDate = DateTime.Now,
                    Description = model.GLLinesDescription,
                    EnteredCr = model.EnteredCr,
                    EnteredDr = model.EnteredDr,
                    HeaderId = model.HeaderId,
                    Id = model.Id,
                    Qty = model.Qty,
                    TaxRateCode = model.TaxRateCode,
                    UpdateDate = DateTime.Now
                }).ToList();

            if (entity.Id == 0)
            {
                entity.Id = Convert.ToInt32(service.Insert(entity));
            }
            else
            {
                entity.Id = Convert.ToInt32(service.Update(entity));
            }

            return entity.Id;
        }

        private List<JournalVoucherViewModel> getJournalVouchers(JournalVoucherListModel model)
        {
            List<JournalVoucherViewModel> list = service.GetAll(AuthenticationHelper.User.CompanyId, model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new JournalVoucherViewModel(x)).ToList();
            return list;
                
        }

        private List<SelectListItem> getPeriodList(string sobId)
        {
            List<SelectListItem> list = calendarService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.PeriodName, Value = x.Id.ToString() }).ToList();
            return list;
        }

        private List<SelectListItem> getCurrencyList(string sobId)
        {
            List<SelectListItem> list = currencyService.GetAll(AuthenticationHelper.User.CompanyId, Convert.ToInt32(sobId))
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return list;
                
        }
        #endregion
    }
}