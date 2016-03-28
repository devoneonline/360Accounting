﻿using _360Accounting.Web;
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

        public ActionResult Edit(string id, long currencyId, long sobId, long periodId)
        {
            InvoiceModel model = InvoiceHelper.GetInvoice(id);
            SessionHelper.SOBId = model.SOBId;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

            model.InvoiceDetail = InvoiceHelper.GetInvoiceDetail(id);
            model.CurrencyId = currencyId;
            model.SOBId = sobId;
            model.PeriodId = periodId;

            CustomerModel customer = CustomerHelper.GetCustomer(model.CustomerId.ToString());
            CustomerSiteModel customerSite = CustomerHelper.GetCustomerSite(model.CustomerSiteId.ToString());

            ///TODO: Plz do the code audit.
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
                        SessionHelper.Invoice.InvoiceNo = InvoiceHelper.GetInvoiceNo(AuthenticationHelper.User.CompanyId, SessionHelper.Invoice.SOBId, SessionHelper.Invoice.PeriodId, SessionHelper.Invoice.CurrencyId);
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
                    bool validated = false;
                    if (SessionHelper.Invoice != null)
                    {
                        model.Id = SessionHelper.Invoice.InvoiceDetail.Last().Id + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                        InvoiceHelper.Insert(model);
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

        public ActionResult Create(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;
            
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            ViewBag.CurrencyName = CurrencyHelper.GetCurrency(currencyId.ToString()).Name;

            InvoiceModel model = SessionHelper.Invoice;
            if (model == null)
            {
                List<SelectListItem> customers = CustomerHelper.GetCustomers()
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
                    CompanyId = AuthenticationHelper.User.CompanyId,
                    CurrencyId = currencyId,
                    Customers = customers,
                    CustomerId = customers.Any() ? 
                    Convert.ToInt32(customers.First().Value) : 0,
                    CustomerSites = customerSites,
                    CustomerSiteId = customerSites.Any() ?
                    Convert.ToInt32(customerSites.First().Value) : 0,
                    InvoiceDate = SessionHelper.Calendar.StartDate,
                    InvoiceDetail = new List<InvoiceDetailModel>(),
                    InvoiceNo = "New",
                    PeriodId = periodId,
                    SOBId = sobId,
                };
                SessionHelper.Invoice = model;
            }
            return View("Edit", model);
        }

        public JsonResult CurrencyList(long sobId)
        {
            List<SelectListItem> currencylist = CurrencyHelper.GetCurrencyList(sobId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(currencylist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList(long sobId)
        {
            List<SelectListItem> periodList = CalendarHelper.GetCalendarsList(sobId);
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPartial(long sobId, long periodId, long currencyId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", InvoiceHelper
                .GetInvoices(sobId, periodId, currencyId));
        }

        public ActionResult Index(InvoiceListModel model)
        {
            SessionHelper.Invoice = null;
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBookList();
                model.SOBId = model.SetOfBooks.Any() ?
                    Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
            }

            if (model.Periods == null && model.SetOfBooks.Any())
            {
                model.Periods = CalendarHelper.GetCalendars(Convert.ToInt32(model.SetOfBooks.First().Value))
                    .Select(x => new SelectListItem
                    { 
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
                model.PeriodId = model.Periods.Any() ?
                    Convert.ToInt32(model.Periods.First().Value) : 0;
            }

            if (model.Currencies == null && model.SetOfBooks.Any())
            {
                model.Currencies = CurrencyHelper.GetCurrencyList(Convert.ToInt32(model.SetOfBooks.First().Value))
                    .Select(x => new SelectListItem 
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.CurrencyId = model.Currencies.Any() ?
                    Convert.ToInt32(model.Currencies.First().Value) : 0;
            }
            return View(model);
        }
 
    }
}