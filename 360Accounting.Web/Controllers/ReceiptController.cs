﻿using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using _360Accounting.Web.Reports;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class ReceiptController : BaseController
    {
        private ICalendarService calendarService;
        private IReceiptService service;
        private ISetOfBookService sobService;
        private ICustomerService customerService;
        private ICustomerSiteService customerSiteService;
        private IBankService bankService;
        private IBankAccountService bankAccountService;
        private ICurrencyService currencyService;

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

        #region Private Methods

        private CustomerwiseReceiptClearanceReport createCustomerwiseReceiptClearanceReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            List<CustomerwiseReceiptClearanceModel> modelList = mapCustomerwiseReceiptClearnaceModel(service.CustomerwiseReceiptClearance(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate, customerId));
            CustomerwiseReceiptClearanceReport report = new CustomerwiseReceiptClearanceReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["CustomerId"].Value = customerId;
            report.DataSource = modelList;
            return report;
        }

        private List<CustomerwiseReceiptClearanceModel> mapCustomerwiseReceiptClearnaceModel(List<CustomerwiseReceiptClearance> entityList)
        {
            List<CustomerwiseReceiptClearanceModel> reportModel = new List<CustomerwiseReceiptClearanceModel>();

            foreach (var record in entityList)
            {
                reportModel.Add(new CustomerwiseReceiptClearanceModel
                {
                    Amount = record.Amount,
                    BankAccountName = record.BankAccountName,
                    BankName = record.BankName,
                    CustomerName = record.CustomerName,
                    ReceiptNo = record.ReceiptNo
                });
            }

            return reportModel;
        }

        private ReceiptAuditTrialReport createReceiptAuditTrialReport(DateTime fromDate, DateTime toDate)
        {
            List<ReceiptAuditTrialModel> modelList = mapReceiptAuditTrialModel(service.ReceiptAuditTrial(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate));
            ReceiptAuditTrialReport report = new ReceiptAuditTrialReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.DataSource = modelList;
            return report;
        }

        private List<ReceiptAuditTrialModel> mapReceiptAuditTrialModel(List<ReceiptAuditTrial> entityList)
        {
            List<ReceiptAuditTrialModel> reportModel = new List<ReceiptAuditTrialModel>();

            foreach (var record in entityList)
            {
                reportModel.Add(new ReceiptAuditTrialModel
                    {
                        Amount = record.Amount,
                        BankAccountName = record.BankAccountName,
                        BankName = record.BankName,
                        CustomerName = record.CustomerName,
                        CustomerSiteName = record.CustomerSiteName,
                        ReceiptDate = record.ReceiptDate,
                        ReceiptNo = record.ReceiptNo,
                        Status = record.Status
                    });
            }

            return reportModel;
        }

        private ReceiptPrintoutReport createReceiptPrintoutReport(DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            List<ReceiptPrintoutModel> modelList = mapReceiptPrintoutModel(service.ReceiptPrintout(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate, receiptNo, customerId, customerSiteId));
            ReceiptPrintoutReport report = new ReceiptPrintoutReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["ReceiptNo"].Value = receiptNo;
            report.Parameters["CustomerId"].Value = customerId;
            report.Parameters["CustomerSiteId"].Value = customerSiteId;
            report.DataSource = modelList;
            return report;
        }

        private List<ReceiptPrintoutModel> mapReceiptPrintoutModel(List<ReceiptPrintout> entityList)
        {
            List<ReceiptPrintoutModel> reportModel = new List<ReceiptPrintoutModel>();

            foreach (var record in entityList)
            {
                reportModel.Add(new ReceiptPrintoutModel
                {
                    AmountInWords = Utility.AmountinWords(Convert.ToDouble(record.ReceiptAmount)),
                    CustomerName = record.CustomerName,
                    ReceiptAmount = record.ReceiptAmount,
                    Remarks = record.Remarks,
                });
            }

            return reportModel;
        }

        #endregion

        public ActionResult ReceiptPrintoutPartialExport(DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            return DocumentViewerExtension.ExportTo(createReceiptPrintoutReport(fromDate, toDate, receiptNo, customerId, customerSiteId), Request);
        }

        public ActionResult ReceiptPrintoutPartial(DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            return PartialView("_ReceiptPrintout", createReceiptPrintoutReport(fromDate, toDate, receiptNo, customerId, customerSiteId));
        }

        public ActionResult ReceiptPrintoutReport(DateTime fromDate, DateTime toDate, string receiptNo, long customerId, long customerSiteId)
        {
            return View(createReceiptPrintoutReport(fromDate, toDate, receiptNo, customerId, customerSiteId));
        }

        public ActionResult ReceiptPrintout()
        {
            ReceiptPrintoutCriteriaModel model = new ReceiptPrintoutCriteriaModel();

            model.Customers.Add(new SelectListItem { Text = "All Customers", Value = "0" });

            foreach (var customer in CustomerHelper.GetCustomers())
            {
                model.Customers.Add(new SelectListItem { Text = customer.CustomerName, Value = customer.Id.ToString() });
            }

            if (model.Customers != null && model.Customers.Count() > 0)
            {
                model.CustomerId = Convert.ToInt64(model.Customers.FirstOrDefault().Value);
                model.CustomerSites = CustomerHelper.GetCustomerSitesCombo(model.CustomerId);

                if (model.CustomerSites != null && model.CustomerSites.Count() > 0)
                {
                    model.CustomerSiteId = Convert.ToInt64(model.CustomerSites.FirstOrDefault().Value);
                }
            }

            return View(model);
        }

        public ActionResult ReceiptAuditTrialPartialExport(DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(createReceiptAuditTrialReport(fromDate, toDate), Request);
        }

        public ActionResult ReceiptAuditTrialPartial(DateTime fromDate, DateTime toDate)
        {
            return PartialView("_ReceiptAuditTrial", createReceiptAuditTrialReport(fromDate, toDate));
        }
        
        public ActionResult ReceiptAuditTrialReport(DateTime fromDate, DateTime toDate)
        {
            return View(createReceiptAuditTrialReport(fromDate, toDate));
        }

        public ActionResult ReceiptAuditTrial()
        {
            ReceiptAuditTrialCriteriaModel model = new ReceiptAuditTrialCriteriaModel();
            return View(model);
        }

        public ActionResult CustomerwiseReceiptClearancePartialExport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return DocumentViewerExtension.ExportTo(createCustomerwiseReceiptClearanceReport(fromDate, toDate, customerId), Request);
        }

        public ActionResult CustomerwiseReceiptClearancePartial(DateTime fromDate, DateTime toDate, long customerId)
        {
            return PartialView("_CustomerwiseReceiptClearance", createCustomerwiseReceiptClearanceReport(fromDate, toDate, customerId));
        }

        public ActionResult CustomerwiseReceiptClearanceReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return View(createCustomerwiseReceiptClearanceReport(fromDate, toDate, customerId));
        }

        public ActionResult CustomerwiseReceiptClearance()
        {
            CustomerSalesCriteriaModel model = new CustomerSalesCriteriaModel();

            model.Customers.Add(new SelectListItem
            {
                Text = "All Customers",
                Value = "0"
            });

            foreach (var customer in CustomerHelper.GetCustomers())
            {
                model.Customers.Add(new SelectListItem
                {
                    Text = customer.CustomerName,
                    Value = customer.Id.ToString()
                });
            }

            return View(model);
        }




        public ActionResult Index(string message = "")
        {
            ViewBag.ErrorMessage = message;
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
                model.Customers = customerService.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, 
                    SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate).
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
            ReceiptViewModel model = ReceiptHelper.GetReceipt(id);
            
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
            try
            {
                ReceiptHelper.DeleteReceipt(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult EmptyListPartial()
        {
             return PartialView("_List", new List<ReceiptViewModel>());
        }

        public ActionResult ListPartial(long periodId, long customerId, long currencyId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            return PartialView("_List", ReceiptHelper.GetReceipts(SessionHelper.SOBId, SessionHelper.Calendar.Id, customerId, currencyId));
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
                    result = ReceiptHelper.SaveReceipt(model);
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
            receipt.ConversionRate = 1;
            receipt.ReceiptDate = SessionHelper.Calendar.StartDate;

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

        public JsonResult CustomerSiteList(long customerId)
        {
            List<SelectListItem> customerSiteList = CustomerHelper.GetCustomerSites(customerId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.SiteName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(customerSiteList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = BankHelper.GetBankAccountList(bankId);

            return Json(bankAccountList, JsonRequestBehavior.AllowGet);
        }
    }
}