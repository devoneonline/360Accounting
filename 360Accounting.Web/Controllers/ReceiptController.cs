using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class ReceiptController : Controller
    {
        private ICalendarService calendarService;
        private IReceiptService service;
        private ISetOfBookService sobService;
        private ICustomerService customerService;
        private ICustomerSiteService customerSiteService;
        private IBankService bankService;
        private IBankAccountService bankAccountService;
        private ICurrencyService currencyService;

        #region Private Methods
        private Receipt mapModel(ReceiptViewModel model)
        {
            return new Receipt
            {
                BankAccountId = model.BankAccountId,
                ReceiptDate = model.ReceiptDate,
                ReceiptNumber = model.ReceiptNumber,
                Remarks = model.Remarks,
                ReceiptAmount = model.ReceiptAmount,
                BankId = model.BankId,
                ConversionRate = model.ConversionRate,
                CustomerId = model.CustomerId,
                CustomerSiteId = model.CustomerSiteId,
                Id = model.Id,
                PeriodId = model.PeriodId,
                SOBId = model.SOBId,
                Status = model.Status,
                CurrencyId = model.CurrencyId
            };
        }
        #endregion

        public ReceiptController()
        {
            service = IoC.Resolve<IReceiptService>("ReceiptService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            customerService = IoC.Resolve<ICustomerService>("CustomerService");
            customerSiteService = IoC.Resolve<ICustomerSiteService>("CustomerSiteService");
            bankService = IoC.Resolve<IBankService>("BankService");
            bankAccountService = IoC.Resolve<IBankAccountService>("BankAccountService");
            currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
        }

        public ActionResult Index()
        {
            ReceiptListModel model = new ReceiptListModel();
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.SOBId = model.SetOfBooks.Count() > 0 ? Convert.ToInt64(model.SetOfBooks.FirstOrDefault().Value) : 0;

            if (model.Periods == null)
            {
                model.Periods = calendarService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.PeriodName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.PeriodId = model.Periods.Count() > 0 ? Convert.ToInt64(model.Periods.FirstOrDefault().Value) : 0;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(model.PeriodId.ToString());

            if (model.Customers == null)
            {
                model.Customers = customerService.GetAll(AuthenticationHelper.User.CompanyId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate).
                    Select(a => new SelectListItem
                {
                    Text = a.CustomerName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.CustomerId = model.Customers.Count() > 0 ? Convert.ToInt64(model.Customers.FirstOrDefault().Value) : 0;

            if (model.Currency == null)
            {
                model.Currency = currencyService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.CurrencyId = model.Currency.Count() > 0 ? Convert.ToInt64(model.Currency.FirstOrDefault().Value) : 0;

            return View(model);
        }
        
        public ActionResult Edit(string id)
        {
            ReceiptViewModel model = ReceiptViewtoReceipt(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            
            if (model.CustomerSites == null)
            {
                model.CustomerSites = CustomerHelper.GetCustomerSites(model.CustomerId).Select(a => new SelectListItem
                {
                    Text = a.SiteName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }

            if (model.Banks == null)
            {
                model.Banks = BankHelper.GetBankList(model.SOBId);
            }

            if (model.BankAccounts == null)
            {
                model.BankAccounts = BankHelper.GetBankAccountList(model.BankId);
            }

            SessionHelper.Calendar = CalendarHelper.GetCalendar(model.PeriodId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(model.CurrencyId.ToString()).Precision;
            return View("Create", model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        public ActionResult ListPartial(long periodId, long sobId, long customerId, long currencyId)
        {
            IEnumerable<ReceiptView> boList = service
                .GetReceipts(sobId, periodId, customerId, currencyId, AuthenticationHelper.User.CompanyId).ToList();

            List<ReceiptViewModel> modelList = boList.Select(a => new ReceiptViewModel(a)).ToList();
            return PartialView("_List", modelList);
        }

        [HttpPost]
        public ActionResult Create(ReceiptViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool validated = false;
                string result = "";
                //Validation..
                CalendarViewModel period = CalendarHelper.GetCalendar(model.PeriodId.ToString());
                if (model.ReceiptDate >= period.StartDate && model.ReceiptDate <= period.EndDate)
                    validated = true;

                if (validated)
                {
                    if (model.Id > 0)
                        result = service.Update(mapModel(model));
                    else
                        result = service.Insert(mapModel(model));
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Create(long periodId, long sobId, long customerId, long currencyId)
        {
            ReceiptViewModel receipt = new ReceiptViewModel();
            receipt.PeriodId = periodId;
            receipt.SOBId = sobId;
            receipt.CustomerId = customerId;
            receipt.CurrencyId = currencyId;

            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());

            if (receipt.CustomerSites == null)
            {
                receipt.CustomerSites = CustomerHelper.GetCustomerSites(receipt.CustomerId).Select(a => new SelectListItem
                {
                    Text = a.SiteName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            else
            {
                receipt.CustomerSites = new List<SelectListItem>();
            }

            if (receipt.Banks == null)
            {
                receipt.Banks = BankHelper.GetBankList(receipt.SOBId);
            }
            else
            {
                receipt.Banks = new List<SelectListItem>();
            }

            if (receipt.BankAccounts == null)
            {
                receipt.BankAccounts = BankHelper.GetBankAccountList(receipt.BankId);
            }
            else
            {
                receipt.BankAccounts = new List<SelectListItem>();
            }

            return View(receipt);
        }

        private ReceiptViewModel ReceiptViewtoReceipt(Receipt receipt)
        {
            return new ReceiptViewModel
            {
                BankAccountId = receipt.BankAccountId,
                ReceiptAmount = receipt.ReceiptAmount,
                ReceiptDate = receipt.ReceiptDate,
                ReceiptNumber = receipt.ReceiptNumber,
                PeriodId = receipt.PeriodId,
                CustomerSiteId = receipt.CustomerSiteId,
                CustomerId = receipt.CustomerId,
                Id = receipt.Id,
                CurrencyId = receipt.CurrencyId,
                ConversionRate = receipt.ConversionRate,
                BankId = receipt.BankId,
                Remarks = receipt.Remarks,
                SOBId = receipt.SOBId,
                Status = receipt.Status
            };
        }

        public JsonResult CheckReceiptDate(DateTime receiptDate, long periodId)
        {
            bool returnData = true;
            if (periodId > 0)
            {
                if (SessionHelper.Calendar != null)
                {
                    if (receiptDate >= SessionHelper.Calendar.StartDate && receiptDate <= SessionHelper.Calendar.EndDate)
                        returnData = true;
                    else
                        returnData = false;
                }
            }
            return Json(returnData);
        }

        public JsonResult PeriodList(long sobId)
        {
            List<SelectListItem> periodList = CalendarHelper.GetCalendars(sobId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomerList(long periodId)
        {
            CalendarViewModel calendar = CalendarHelper.GetCalendar(periodId.ToString());
            List<SelectListItem> customerList = CustomerHelper.GetCustomers(calendar.StartDate, calendar.EndDate)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CustomerName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = BankHelper.GetBankAccountList(bankId);

            return Json(bankAccountList, JsonRequestBehavior.AllowGet);
        }
    }
}