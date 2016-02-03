using _360Accounting.Core;
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
    public class JournalVoucherController : Controller
    {
        private IJournalVoucherService service;
        private ISetOfBookService sobService;
        private ICurrencyService currencyService;
        private ICalendarService calendarService;

        public JournalVoucherController()
        {
            ////service = IoC.Resolve<IJournalVoucherService>("JournalVoucherService");
            service = new JournalVoucherService(new JournalVoucherRepository());
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
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

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            JournalVoucherCreateModel model = new JournalVoucherCreateModel();
            model.SOBId = sobId;
            model.PeriodId = periodId;
            model.CurrencyId = currencyId;
            model.SOBName = sobService.GetSingle(sobId.ToString(), AuthenticationHelper.User.CompanyId).Name;
            model.PeriodName = calendarService.GetSingle(periodId.ToString(), AuthenticationHelper.User.CompanyId).PeriodName;
            model.CurrencyName = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.User.CompanyId).Name;
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