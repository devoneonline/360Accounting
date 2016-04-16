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
            model.SOBId = SessionHelper.SOBId;

            if (model.Periods == null)
            {
                model.Periods = ReceivablePeriodHelper.GetPeriodList(Convert.ToInt64(SessionHelper.SOBId));
                model.PeriodId = model.Periods.Any() ?
                    Convert.ToInt32(model.Periods.First().Value) : 0;
            }
            else if (model.Periods == null)
            {
                model.Periods = new List<SelectListItem>();
            }

            CalendarViewModel calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(model.PeriodId.ToString()).CalendarId.ToString());
            if (calendar == null)
            {
                SessionHelper.Calendar.StartDate = DateTime.Now;
                SessionHelper.Calendar.EndDate = DateTime.Now;
            }
            else
            {
                SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(model.PeriodId.ToString()).CalendarId.ToString());
            }

            if (model.Customers == null)
            {
                model.Customers = customerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate).
                    Select(a => new SelectListItem
                {
                    Text = a.CustomerName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.CustomerId = model.Customers.Count() > 0 ? Convert.ToInt64(model.Customers.FirstOrDefault().Value) : 0;

            if (model.Currency == null)
            {
                model.Currency = CurrencyHelper.GetCurrencies(Convert.ToInt32(SessionHelper.SOBId))
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.CurrencyId = model.Currency.Any() ? Convert.ToInt32(model.Currency.First().Value) : 0;
            }
            else if (model.Currency == null)
            {
                model.Currency = new List<SelectListItem>();
            }

            return View(model);
        }
        
        public ActionResult Edit(string id)
        {
            ReceiptViewModel model = ReceiptViewtoReceipt(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            
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
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
            return RedirectToAction("Index");
        }

        public ActionResult EmptyListPartial()
        {
             return PartialView("_List", new List<ReceiptViewModel>());
        }

        public ActionResult ListPartial(long periodId, long customerId, long currencyId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            IEnumerable<ReceiptView> boList = service
                .GetReceipts(SessionHelper.SOBId, SessionHelper.Calendar.Id, customerId, currencyId, AuthenticationHelper.CompanyId.Value).ToList();

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
                model.PeriodId = SessionHelper.Calendar.Id;
                model.CompanyId = AuthenticationHelper.CompanyId.Value;
                //Validation..

                if (model.ReceiptDate >= SessionHelper.Calendar.StartDate && model.ReceiptDate <= SessionHelper.Calendar.EndDate)
                    validated = true;

                if (validated)
                {
                    if (model.Id > 0)
                        result = service.Update(mapModel(model));
                    else
                    {
                        //model.ReceiptNumber = ReceiptHelper.GetDocNo(model.CustomerId, model.PeriodId, model.SOBId, model.CurrencyId);
                        result = service.Insert(mapModel(model));
                    }
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Create(long periodId, long customerId, long currencyId)
        {
            ReceiptViewModel receipt = new ReceiptViewModel();

            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());

            receipt.PeriodId = SessionHelper.Calendar.Id;
            receipt.SOBId = SessionHelper.SOBId;
            receipt.CustomerId = customerId;
            receipt.CurrencyId = currencyId;

            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

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
            receipt.BankId = receipt.Banks.Count() > 0 ? Convert.ToInt64(receipt.Banks.FirstOrDefault().Value) : 0;

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

        public JsonResult CurrencyList()
        {
            List<SelectListItem> currencyList = CurrencyHelper.GetCurrencyList(SessionHelper.SOBId);
            return Json(currencyList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList()
        {
            List<SelectListItem> periodList = ReceivablePeriodHelper.GetPeriodList(SessionHelper.SOBId);
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomerList(long periodId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            List<SelectListItem> customerList = CustomerHelper.GetCustomers(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate).Select(x => new SelectListItem
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