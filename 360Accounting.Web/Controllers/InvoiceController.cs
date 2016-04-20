using _360Accounting.Web;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class InvoiceController : Controller
    {
        public JsonResult CustomerSiteList(long customerId)
        {
            List<SelectListItem> customerList = CustomerHelper.GetCustomerSites(customerId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.SiteName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            InvoiceHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long currencyId, long periodId)
        {
            InvoiceModel model = InvoiceHelper.GetInvoice(id);
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

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

            SessionHelper.Invoice = model;
            return View(model);
        }

        public ActionResult SaveInvoice(string invoiceType, string invoiceDate, 
            string conversionRate, string remarks, long customerId, long customerSiteId)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Invoice != null)
                {
                    SessionHelper.Invoice.InvoiceType = invoiceType;
                    SessionHelper.Invoice.InvoiceDate = Convert.ToDateTime(invoiceDate);
                    SessionHelper.Invoice.ConversionRate = Convert.ToDecimal(conversionRate);
                    SessionHelper.Invoice.Remarks = remarks;
                    SessionHelper.Invoice.CustomerId = customerId;
                    SessionHelper.Invoice.CustomerSiteId = customerSiteId;
                    if (SessionHelper.Invoice.InvoiceNo == "New")
                    {
                        SessionHelper.Invoice.InvoiceNo = InvoiceHelper.GetInvoiceNo(AuthenticationHelper.CompanyId.Value, SessionHelper.Invoice.SOBId, SessionHelper.Invoice.PeriodId, SessionHelper.Invoice.CurrencyId);
                    }

                    InvoiceHelper.Update(SessionHelper.Invoice);
                    SessionHelper.Invoice = null;
                    saved = true;
                    message = "Saved successfully";
                }
                else
                    message = "No voucher information available!";
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
                    InvoiceHelper.UpdateInvoiceDetail(model);
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

        public JsonResult CheckDate(DateTime invoiceDate, long periodId)
        {
            bool result = false;
            if (periodId > 0)
            {
                if (SessionHelper.Calendar != null)
                {
                    if (invoiceDate >= SessionHelper.Calendar.StartDate || invoiceDate <= SessionHelper.Calendar.EndDate)
                        result = true;
                }
            }
            return Json(result);
        }

        public ActionResult Create(long periodId, long currencyId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(ReceivablePeriodHelper.GetReceivablePeriod(periodId.ToString()).CalendarId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

            InvoiceModel model = SessionHelper.Invoice;
            if (model == null)
            {
                List<SelectListItem> customers = CustomerHelper.GetCustomers(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CustomerName,
                        Value = x.Id.ToString()
                    }).ToList();

                List<SelectListItem> customerSites = 
                    CustomerHelper.GetCustomerSites
                    (customers.Any() ? Convert.ToInt32(customers.First().Value) : 0)
                    .Select(x => new SelectListItem
                    {
                        Text = x.SiteName,
                        Value = x.Id.ToString()
                    }).ToList();

                model = new InvoiceModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    CurrencyId = currencyId,
                    Customers = customers.Any() ? customers : new List<SelectListItem>(),
                    CustomerId = customers.Any() ? 
                    Convert.ToInt32(customers.First().Value) : 0,
                    CustomerSites = customerSites.Any() ? customerSites : new List<SelectListItem>(),
                    CustomerSiteId = customerSites.Any() ?
                    Convert.ToInt32(customerSites.First().Value) : 0,
                    InvoiceDate = SessionHelper.Calendar.StartDate,
                    InvoiceDetail = new List<InvoiceDetailModel>(),
                    InvoiceNo = "New",
                    PeriodId = periodId,
                    SOBId = SessionHelper.SOBId,
                    ConversionRate = 1
                };
                SessionHelper.Invoice = model;
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

        public ActionResult ListPartial(long periodId, long currencyId)
        {
            return PartialView("_List", InvoiceHelper
                .GetInvoices(SessionHelper.SOBId, periodId, currencyId));
        }

        public ActionResult EmptyListPartial()
        {
            return PartialView("_List", new List<InvoiceModel>());
        }

        public ActionResult Index(InvoiceListModel model)
        {
            SessionHelper.Invoice = null;
            model.SOBId = SessionHelper.SOBId;
            if (model.Periods == null)
            {
                model.Periods = ReceivablePeriodHelper.GetPeriodList(SessionHelper.SOBId);
                model.PeriodId = model.Periods.Any() ?
                    Convert.ToInt32(model.Periods.First().Value) : 0;
            }
            else if (model.Periods == null)
            {
                model.Periods = new List<SelectListItem>();
            }

            if (model.Currencies == null)
            {
                model.Currencies = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
                    .Select(x => new SelectListItem 
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.CurrencyId = model.Currencies.Any() ?
                    Convert.ToInt32(model.Currencies.First().Value) : 0;
            }
            else if (model.Currencies == null)
            {
                model.Currencies = new List<SelectListItem>();
            }

            return View(model);
        }
 
    }
}