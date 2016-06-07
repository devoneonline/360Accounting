using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web;
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
    public class InvoiceController : BaseController
    {
        private IInvoiceService service;

        public InvoiceController()
        {
            service = IoC.Resolve<IInvoiceService>("InvoiceService");
        }

        #region Private Methods

        private InvoicePrintoutReport createInvoicePrintoutReport(DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            List<InvoicePrintoutModel> modelList = mapInvoicePrintoutModel(service.InvoicePrintout(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate, invoiceNo, customerId, customerSiteId));
            InvoicePrintoutReport report = new InvoicePrintoutReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["InvoiceNo"].Value = invoiceNo;
            report.Parameters["CustomerId"].Value = customerId;
            report.Parameters["CustomerSiteId"].Value = customerSiteId;
            report.DataSource = modelList;
            return report;
        }

        private List<InvoicePrintoutModel> mapInvoicePrintoutModel(List<InvoicePrintout> list)
        {
            List<InvoicePrintoutModel> reportModel = new List<InvoicePrintoutModel>();
            foreach (var record in list)
            {
                reportModel.Add(new InvoicePrintoutModel
                {
                    Amount = record.Amount,
                    CustomerName = record.CustomerName,
                    CustomerSiteName = record.CustomerSiteName,
                    InvoiceDate = record.InvoiceDate,
                    InvoiceNo = record.InvoiceNo,
                    ItemName = record.ItemName,
                    OrderReferenceNo = record.OrderReferenceNo,
                    Quantity = record.Quantity,
                    Rate = record.Rate,
                    Remarks = record.Remarks,
                    SalesTaxVAT = record.SalesTaxVAT,
                    UOM = record.UOM
                });
            }

            return reportModel;
        }

        private InvoiceAuditTrailReport createInvoiceAuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            List<InvoiceAuditTrailModel> modelList = mapInvoiceAuditTrailModel(service.InvoiceAuditTrail(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate));
            InvoiceAuditTrailReport report = new InvoiceAuditTrailReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.DataSource = modelList;
            return report;
        }

        private List<InvoiceAuditTrailModel> mapInvoiceAuditTrailModel(List<InvoiceAuditTrail> list)
        {
            List<InvoiceAuditTrailModel> reportModel = new List<InvoiceAuditTrailModel>();
            foreach (var record in list)
            {
                reportModel.Add(new InvoiceAuditTrailModel
                {
                    Amount = record.Amount,
                    CustomerName = record.CustomerName,
                    CustomerSiteName = record.CustomerSiteName,
                    InvoiceDate = record.InvoiceDate,
                    InvoiceNo = record.InvoiceNo,
                    ItemName = record.ItemName,
                    Quantity = record.Quantity,
                    Rate = record.Rate,
                    TaxAmount = record.TaxAmount,
                    TaxName = record.TaxName,
                    TotalAmount = record.TotalAmount,
                    UOM = record.UOM
                });
            }

            return reportModel;
        }

        private CustomerSalesReport createCustomerSalesReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            List<CustomerSalesModel> modelList = mapCustomerSalesModel(service.CustomerSales(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate, customerId));
            CustomerSalesReport report = new CustomerSalesReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["CustomerId"].Value = customerId;
            report.DataSource = modelList;
            return report;
        }

        private List<CustomerSalesModel> mapCustomerSalesModel(List<CustomerSales> list)
        {
            List<CustomerSalesModel> reportModel = new List<CustomerSalesModel>();
            foreach (var record in list)
            {
                reportModel.Add(new CustomerSalesModel
                {
                    Amount = record.Amount,
                    CustomerName = record.CustomerName,
                    InvoiceSourceName = record.InvoiceSourceName,
                    ItemName = record.ItemName,
                    Quantity = record.Quantity,
                    TaxAmount = record.TaxAmount,
                    TotalAmount = record.TotalAmount
                });
            }

            return reportModel;
        }

        private PeriodwiseActivityReport createPeriodwiseActivityReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            List<PeriodwiseActivityModel> modelList = mapPeriodwiseActivityModel(service.PeriodwiseActivity(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, fromDate, toDate, customerId));
            PeriodwiseActivityReport report = new PeriodwiseActivityReport();
            report.Parameters["FromDate"].Value = fromDate;
            report.Parameters["ToDate"].Value = toDate;
            report.Parameters["CustomerId"].Value = customerId;
            report.DataSource = modelList;
            return report;
        }

        private List<PeriodwiseActivityModel> mapPeriodwiseActivityModel(List<PeriodwiseActivity> list)
        {
            List<PeriodwiseActivityModel> reportModel = new List<PeriodwiseActivityModel>();
            foreach (var record in list)
            {
                reportModel.Add(new PeriodwiseActivityModel
                {
                    ClosingAmount = record.ClosingAmount,
                    CustomerName = record.CustomerName,
                    OpeningBalance = record.OpeningBalance,
                    ReceiptAmount = record.ReceiptAmount,
                    SalesAmount = record.SalesAmount,
                    SiteName = record.SiteName
                });
            }

            return reportModel;
        }

        #endregion

        public ActionResult InvoicePrintoutPartialExport(DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            return DocumentViewerExtension.ExportTo(createInvoicePrintoutReport(fromDate, toDate, invoiceNo, customerId, customerSiteId), Request);
        }

        public ActionResult InvoicePrintoutPartial(DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            return PartialView("_InvoicePrintout", createInvoicePrintoutReport(fromDate, toDate, invoiceNo, customerId, customerSiteId));
        }

        public ActionResult InvoicePrintoutReport(DateTime fromDate, DateTime toDate, string invoiceNo, long customerId, long customerSiteId)
        {
            return View(createInvoicePrintoutReport(fromDate, toDate, invoiceNo, customerId, customerSiteId));
        }

        public ActionResult InvoicePrintout()
        {
            InvoicePrintoutCriteriaModel model = new InvoicePrintoutCriteriaModel();

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

        public ActionResult InvoiceAuditTrailPartialExport(DateTime fromDate, DateTime toDate)
        {
            return DocumentViewerExtension.ExportTo(createInvoiceAuditTrailReport(fromDate, toDate), Request);
        }

        public ActionResult InvoiceAuditTrailPartial(DateTime fromDate, DateTime toDate)
        {
            return PartialView("_InvoiceAuditTrail", createInvoiceAuditTrailReport(fromDate, toDate));
        }

        public ActionResult InvoiceAuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            return View(createInvoiceAuditTrailReport(fromDate, toDate));
        }

        public ActionResult InvoiceAuditTrail()
        {
            InvoiceAuditTrailCriteriaModel model = new InvoiceAuditTrailCriteriaModel();
            return View(model);
        }

        public ActionResult CustomerSalesPartialExport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return DocumentViewerExtension.ExportTo(createCustomerSalesReport(fromDate, toDate, customerId), Request);
        }

        public ActionResult CustomerSalesPartial(DateTime fromDate, DateTime toDate, long customerId)
        {
            return PartialView("_CustomerSales", createCustomerSalesReport(fromDate, toDate, customerId));
        }

        public ActionResult CustomerSalesReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return View(createCustomerSalesReport(fromDate, toDate, customerId));
        }

        public ActionResult CustomerSales()
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

            //model.Customers = CustomerHelper.GetCustomers().Select(x => new SelectListItem
            //{
            //    Text = x.CustomerName,
            //    Value = x.Id.ToString()
            //}).ToList();

            //if (model.Customers != null && model.Customers.Count() > 0)
            //{
            //    model.CustomerId = Convert.ToInt64(model.Customers.FirstOrDefault().Value);
            //}

            return View(model);
        }

        public ActionResult PeriodwiseActivityPartialExport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return DocumentViewerExtension.ExportTo(createPeriodwiseActivityReport(fromDate, toDate, customerId), Request);
        }

        public ActionResult PeriodwiseActivityPartial(DateTime fromDate, DateTime toDate, long customerId)
        {
            return PartialView("_PeriodwiseActivity", createPeriodwiseActivityReport(fromDate, toDate, customerId));
        }

        public ActionResult PeriodwiseActivityReport(DateTime fromDate, DateTime toDate, long customerId)
        {
            return View(createPeriodwiseActivityReport(fromDate, toDate, customerId));
        }

        public ActionResult PeriodwiseActivity()
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



        public JsonResult CustomerList()
        {
            List<SelectListItem> customerList = CustomerHelper.GetCustomers(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate)
                .Select(x => new SelectListItem
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

        public ActionResult Delete(string id)
        {
            try
            {
                InvoiceHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult Edit(string id, long currencyId, long periodId)
        {
            InvoiceModel model = InvoiceHelper.GetInvoice(id);
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            model.InvoiceDetail = InvoiceHelper.GetInvoiceDetail(id);
            model.CurrencyId = currencyId;
            model.SOBId = SessionHelper.SOBId;
            model.PeriodId = periodId;

            CustomerModel customer = CustomerHelper.GetCustomer(model.CustomerId.ToString());
            CustomerSiteModel customerSite = CustomerHelper.GetCustomerSite(model.CustomerSiteId.ToString());

            ///TODO: Plz check the code.
            model.Customers = new List<SelectListItem>();
            model.Customers.Add(new SelectListItem 
            {
                Value = customer.Id.ToString(),
                Text = customer.CustomerName
            });

            model.CustomerSites = new List<SelectListItem>();
            model.CustomerSites.Add(new SelectListItem
            {
                Value = customerSite.Id.ToString(),
                Text = customerSite.SiteName
            });

            model.Currencies = CurrencyHelper.GetCurrencyList(SessionHelper.SOBId);
            //model.Periods = CalendarHelper.GetCalendarsList(SessionHelper.SOBId);
            model.Periods = ReceivablePeriodHelper.GetPeriodList(SessionHelper.SOBId);

            SessionHelper.Invoice = model;
            return View(model);
        }

        public ActionResult SaveInvoice(string invoiceType, string invoiceDate, string conversionRate, string remarks, long customerId, long customerSiteId, long periodId, long currencyId)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Invoice != null)
                {
                    if (SessionHelper.Invoice.InvoiceDetail.Count == 0)
                    {
                        message = "No detail information available to save!";
                    }
                    else
                    {
                        SessionHelper.Invoice.InvoiceType = invoiceType;
                        SessionHelper.Invoice.InvoiceDate = Convert.ToDateTime(invoiceDate);
                        SessionHelper.Invoice.ConversionRate = Convert.ToDecimal(conversionRate);
                        SessionHelper.Invoice.Remarks = remarks;
                        SessionHelper.Invoice.CustomerId = customerId;
                        SessionHelper.Invoice.CustomerSiteId = customerSiteId;
                        SessionHelper.Invoice.PeriodId = periodId;
                        SessionHelper.Invoice.CurrencyId = currencyId;

                        if (SessionHelper.Invoice.InvoiceDate >= SessionHelper.Calendar.StartDate && SessionHelper.Invoice.InvoiceDate <= SessionHelper.Calendar.EndDate)
                        {
                            if (SessionHelper.Invoice.InvoiceNo == "New")
                            {
                                SessionHelper.Invoice.InvoiceNo = InvoiceHelper.GetInvoiceNo(AuthenticationHelper.CompanyId.Value, SessionHelper.Invoice.SOBId, SessionHelper.Invoice.PeriodId, SessionHelper.Invoice.CurrencyId);
                            }

                            foreach (InvoiceDetailModel invoiceDetail in SessionHelper.Invoice.InvoiceDetail)
                            {
                                invoiceDetail.Amount = invoiceDetail.Quantity * invoiceDetail.Rate;

                                if (invoiceDetail.TaxId != null)
                                {
                                    TaxDetailModel taxDetail = TaxHelper.GetTaxDetail(invoiceDetail.TaxId.ToString()).FirstOrDefault(x => x.StartDate <= SessionHelper.Invoice.InvoiceDate && x.EndDate >= SessionHelper.Invoice.InvoiceDate);

                                    if (taxDetail != null)
                                        invoiceDetail.TaxAmount = invoiceDetail.Amount * taxDetail.Rate / 100;
                                    else
                                        invoiceDetail.TaxAmount = 0;
                                }
                            }

                            InvoiceHelper.Update(SessionHelper.Invoice);
                            SessionHelper.Invoice = null;
                            saved = true;
                            message = "Saved successfully";
                        }
                        else
                            message = "GL Date must lie in the range of the selected period";
                    }
                    
                }
                else
                    message = "No information available to save!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }

        public ActionResult DeletePartial(InvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    InvoiceModel invoice = SessionHelper.Invoice;
                    InvoiceHelper.DeleteInvoiceDetail(model);
                    SessionHelper.Invoice = invoice;
                    IList<InvoiceDetailModel> invoiceDetail = InvoiceHelper.GetInvoiceDetail();
                    return PartialView("_Detail", invoiceDetail);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail");
            //return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(InvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ItemId == null && model.InvoiceSourceId == null)
                    {
                        ViewData["EditError"] = "Either Invoice Source or Item is required.";
                    }
                    else if (model.ItemId != null && model.InvoiceSourceId != null)
                    {
                        ViewData["EditError"] = "Only Invoice Source or Item can be entered at a time.";
                    }
                    else
                    {
                        InvoiceHelper.UpdateInvoiceDetail(model);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(InvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.Invoice != null)
                    {
                        if (SessionHelper.Invoice.InvoiceDetail != null && SessionHelper.Invoice.InvoiceDetail.Count() > 0)
                            model.Id = SessionHelper.Invoice.InvoiceDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    if (model.ItemId == null && model.InvoiceSourceId == null)
                    {
                        ViewData["EditError"] = "Either Invoice Source or Item is required.";
                    }
                    else if (model.ItemId != null && model.InvoiceSourceId != null)
                    {
                        ViewData["EditError"] = "Only Invoice Source or Item can be entered at a time.";
                    }
                    else
                    {
                        InvoiceHelper.Insert(model);
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", InvoiceHelper.GetInvoiceDetail());
        }

        public JsonResult CheckDate(DateTime invoiceDate, long periodId, long customerId, long customerSiteId)
        {
            bool result = false;
            if (periodId > 0)
            {
                if (SessionHelper.Calendar != null)
                {
                    if (invoiceDate >= SessionHelper.Calendar.StartDate && invoiceDate <= SessionHelper.Calendar.EndDate)
                        result = true;
                }
            }

            if (customerId > 0)
            {
                CustomerModel customer = CustomerHelper.GetCustomer(customerId.ToString());

                if (customer.StartDate == null || invoiceDate >= customer.StartDate)
                    result = true;
                else
                    result = false;

                if (customer.EndDate == null || invoiceDate <= customer.EndDate)
                    result = true;
                else
                    result = false;

                //if (invoiceDate >= customer.StartDate && invoiceDate <= customer.EndDate)
                //    result = true;
                //else
                //    result = false;
            }

            if (customerSiteId > 0)
            {
                CustomerSiteModel customerSite = CustomerHelper.GetCustomerSite(customerSiteId.ToString());
                if (invoiceDate >= customerSite.StartDate && invoiceDate <= customerSite.EndDate)
                    result = true;
                else
                    result = false;
            }

            return Json(result);
        }

        public ActionResult Create()
        {
            InvoiceModel model = SessionHelper.Invoice;
            if (model == null)
            {
                model = new InvoiceModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    InvoiceDetail = new List<InvoiceDetailModel>(),
                    InvoiceNo = "New",
                    SOBId = SessionHelper.SOBId,
                    ConversionRate = 1
                };
                SessionHelper.Invoice = model;
            }
            model.Currencies = CurrencyHelper.GetCurrencyList(SessionHelper.SOBId);
            //model.Periods = CalendarHelper.GetCalendarsList(SessionHelper.SOBId);
            model.Periods = ReceivablePeriodHelper.GetPeriodList(SessionHelper.SOBId);

            if (model.Currencies != null && model.Currencies.Count() > 0)
            {
                model.CurrencyId = Convert.ToInt64(model.Currencies.FirstOrDefault().Value);
                SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(model.CurrencyId.ToString()).Precision;
            }
            if (model.Periods != null && model.Periods.Count() > 0)
            {
                model.PeriodId = Convert.ToInt64(model.Periods.FirstOrDefault().Value);
                //SessionHelper.Calendar = CalendarHelper.GetCalendar(model.PeriodId.ToString());
                SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(model.PeriodId.ToString()).CalendarId.ToString());
                model.InvoiceDate = SessionHelper.Calendar.StartDate;
                
                model.Customers = CustomerHelper.GetCustomersCombo(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                if (model.Customers != null && model.Customers.Count() > 0)
                {
                    model.CustomerId = Convert.ToInt64(model.Customers.FirstOrDefault().Value);
                    model.CustomerSites = CustomerHelper.GetCustomerSitesCombo(model.CustomerId);
                    if (model.CustomerSites != null && model.CustomerSites.Count() > 0)
                    {
                        model.CustomerSiteId = Convert.ToInt64(model.CustomerSites.FirstOrDefault().Value);
                    }
                }
            }



            return View("Edit", model);
        }

        public JsonResult CurrencyList()
        {
            List<SelectListItem> currencylist = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(currencylist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList()
        {
            List<SelectListItem> periodList = ReceivablePeriodHelper.GetPeriodList(SessionHelper.SOBId);
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", InvoiceHelper
                .GetInvoices(SessionHelper.SOBId));
        }

        public ActionResult EmptyListPartial()
        {
            return PartialView("_List", new List<InvoiceModel>());
        }

        public ActionResult Index(string message="")
        {
            ViewBag.ErrorMessage = message;
            SessionHelper.Invoice = null;
            
            return View();
        }

        public void AddCalendarinSession(long periodId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
        }
    }
}