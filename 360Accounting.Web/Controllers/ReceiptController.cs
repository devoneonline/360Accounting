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
            model.SOBId = Convert.ToInt64(model.SetOfBooks.FirstOrDefault().Value);

            if (model.Periods == null)
            {
                model.Periods = calendarService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.PeriodName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.PeriodId = Convert.ToInt64(model.Periods.FirstOrDefault().Value);

            if (model.Customers == null)
            {
                model.Customers = customerService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.CustomerName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.CustomerId = Convert.ToInt64(model.Customers.FirstOrDefault().Value);

            if (model.Currency == null)
            {
                model.Currency = currencyService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.Name.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
            model.CurrencyId = Convert.ToInt64(model.Currency.FirstOrDefault().Value);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ReceiptViewModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                if (model.Id > 0)
                    result = service.Update(mapModel(model));
                else
                    result = service.Insert(mapModel(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {

            ReceiptViewModel model = ReceiptViewtoReceipt(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            
            if (model.CustomerSites == null)
            {
                model.CustomerSites = customerSiteService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.SiteName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }

            if (model.Banks == null)
            {
                model.Banks = bankService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.BankName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }

            if (model.BankAccounts == null)
            {
                model.BankAccounts = bankAccountService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.AccountName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }
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
                string result = "";
                if (model.Id > 0)
                    result = service.Update(mapModel(model));
                else
                    result = service.Insert(mapModel(model));

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

            if (receipt.CustomerSites == null)
            {
                receipt.CustomerSites = customerSiteService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.SiteName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }

            if (receipt.Banks == null)
            {
                receipt.Banks = bankService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.BankName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
            }

            if (receipt.BankAccounts == null)
            {
                receipt.BankAccounts = bankAccountService.GetAll(AuthenticationHelper.User.CompanyId).Select(a => new SelectListItem
                {
                    Text = a.AccountName.ToString(),
                    Value = a.Id.ToString()
                }).ToList();
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
    }
}