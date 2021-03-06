﻿using _360Accounting.Core;
using _360Accounting.Web;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _360Accounting.Web.Reports;
using _360Accounting.Core.Entities;
using DevExpress.Web.Mvc;
using _360Accounting.Common;
using System.Web.Security;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        #region Private Methods
        private List<SelectListItem> getCodeCombinationList(long sobId)
        {
            List<SelectListItem> list = CodeCombinationHelper.GetCodeCombinations(sobId, AuthenticationHelper.CompanyId.Value)
                .Select(x => new SelectListItem
                {
                    Text = Utility.Stringize(".", x.Segment1, x.Segment2, x.Segment3, x.Segment4, x.Segment5, x.Segment6, x.Segment7, x.Segment8),
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }
        #endregion

        public ActionResult Delete(string id)
        {
            try
            {
                PaymentHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult Edit(string id)
        {
            PaymentViewModel model = PaymentHelper.GetPayment(id);
            SessionHelper.PeriodId = model.PeriodId;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(PayablePeriodHelper.GetPayablePeriod(model.PeriodId.ToString()).CalendarId.ToString());

            if (model.BankAccount == null)
            {
                model.BankAccount = BankHelper.GetBankAccountList(model.BankId);
                model.BankAccountId = model.BankAccount.Any() ?
                    Convert.ToInt32(model.BankAccount.First().Value) : 0;
            }
            if (model.VendorSite == null)
            {
                model.VendorSite = VendorHelper.GetAllSites(model.VendorId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.VendorSiteId = model.VendorSite.Any() ?
                    Convert.ToInt32(model.VendorSite.First().Value) : 0;
            }

            model.PaymentInvoiceLines = PaymentHelper.GetPaymentLines(id).ToList();
            model.SOBId = SessionHelper.SOBId;
            SessionHelper.Payment = model;

            return View("Create", model);
        }

        public ActionResult Index(PaymentListViewModel model, string message = "")
        {
            ViewBag.ErrorMessage = message;
            SessionHelper.Payment = null;
            model.SOBId = SessionHelper.SOBId;

            return View(model);
        }

        public ActionResult PaymentPartial(PaymentListViewModel model)
        {
            return PartialView("_List", PaymentHelper.GetPayments(SessionHelper.SOBId, model.BankId, model.VendorId, model.PeriodId));
        }

        public ActionResult Create()
        {
            PaymentViewModel model = new PaymentViewModel();
            model.Period = PayablePeriodHelper.GetPeriodList(SessionHelper.SOBId);
            model.PeriodId = model.Period.Any() ? Convert.ToInt64(model.Period.First().Value) : 0;
            if (model.Period != null && model.Period.Count() > 0)
            {
                SessionHelper.Calendar = CalendarHelper.GetCalendar(PayablePeriodHelper.GetPayablePeriod(model.PeriodId.ToString()).CalendarId.ToString());

                model.Bank = BankHelper.GetBankList(SessionHelper.SOBId);
                model.BankId = model.Bank.Any() ? Convert.ToInt64(model.Bank.First().Value) : 0;
                if (model.Bank != null && model.Bank.Count() > 0)
                {
                    model.BankAccount = BankHelper.GetBankAccountList(model.BankId);
                    model.BankAccountId = model.BankAccount.Any() ?
                        Convert.ToInt32(model.BankAccount.First().Value) : 0;
                }

                model.Vendor = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                model.VendorId = model.Vendor.Any() ? Convert.ToInt64(model.Vendor.First().Value) : 0;
                if (model.Vendor != null && model.Vendor.Count() > 0)
                {
                    model.VendorSite = VendorHelper.GetVendorSiteList(model.VendorId);
                    model.VendorSiteId = model.VendorSite.Any() ? Convert.ToInt64(model.VendorSite.First().Value) : 0;
                }
            }

            SessionHelper.Payment = model;
            if (SessionHelper.Payment.PaymentInvoiceLines == null)
                SessionHelper.Payment.PaymentInvoiceLines = new List<PaymentInvoiceLinesModel>();


            return View(model);
        }

        public JsonResult CheckPaymentDate(DateTime paymentDate, long periodId)
        {
            bool returnData = true;
            if (periodId > 0)
            {
                if (SessionHelper.Calendar != null)
                {
                    if (paymentDate < SessionHelper.Calendar.StartDate || paymentDate > SessionHelper.Calendar.EndDate)
                        returnData = false;
                    else
                        returnData = true;
                }
            }
            return Json(returnData);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", PaymentHelper.GetPaymentLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(PaymentInvoiceLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;

                    if (SessionHelper.Payment != null)
                    {
                        if (SessionHelper.Payment.BankAccountId == 0)
                        {
                            ViewData["EditError"] = "Please select Bank Account";
                            return PartialView("createPartial", PaymentHelper.GetPaymentLines());
                        }

                        if (SessionHelper.Payment.VendorSiteId == 0)
                        {
                            ViewData["EditError"] = "Please select Vendor Site";
                            return PartialView("createPartial", PaymentHelper.GetPaymentLines());
                        }

                        model.Id = SessionHelper.Payment.PaymentInvoiceLines.Last().Id + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;

                    if (validated)
                        PaymentHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", PaymentHelper.GetPaymentLines());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(PaymentInvoiceLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PaymentHelper.UpdatePaymentLine(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", PaymentHelper.GetPaymentLines());
        }

        public ActionResult DeletePartial(PaymentInvoiceLinesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PaymentViewModel header = SessionHelper.Payment;
                    PaymentHelper.DeletePaymentLine(model);
                    SessionHelper.Payment = header;
                    IList<PaymentInvoiceLinesModel> paymentLines = PaymentHelper.GetPaymentLines();
                    return PartialView("createPartial", paymentLines);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial");
        }

        public ActionResult SavePayment(PaymentViewModel model)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Payment != null)
                {
                    SessionHelper.Payment.Amount = model.Amount;
                    SessionHelper.Payment.BankAccountId = model.BankAccountId;
                    SessionHelper.Payment.BankId = model.BankId;
                    SessionHelper.Payment.PaymentDate = model.PaymentDate;
                    SessionHelper.Payment.PeriodId = model.PeriodId;
                    SessionHelper.Payment.SOBId = SessionHelper.SOBId;
                    SessionHelper.Payment.Status = model.Status;
                    SessionHelper.Payment.VendorId = model.VendorId;
                    SessionHelper.Payment.VendorSiteId = model.VendorSiteId;

                    if (SessionHelper.Payment.Id == 0)
                        SessionHelper.Payment.PaymentNo = PaymentHelper.GetPaymentNo(AuthenticationHelper.CompanyId.Value, model.VendorId, SessionHelper.SOBId, model.BankId, model.PeriodId);

                    PaymentHelper.Update(SessionHelper.Payment);
                    SessionHelper.Payment = null;
                    saved = true;

                }
                else
                    message = "No Payment information available!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }

        //public JsonResult CurrencyList()
        //{
        //    List<SelectListItem> list = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
        //            .Select(x => new SelectListItem
        //            {
        //                Text = x.Name,
        //                Value = x.Id.ToString()
        //            }).ToList();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult VendorSiteList(long vendorId)
        {
            List<SelectListItem> vendorSiteList = VendorHelper.GetVendorSiteList(vendorId);
            return Json(vendorSiteList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = BankHelper.GetBankAccountList(bankId);
            return Json(bankAccountList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDatabyPeriod(long periodId)
        {
            PaymentViewModel model = new PaymentViewModel();
            SessionHelper.Calendar = CalendarHelper.GetCalendar(PayablePeriodHelper.GetPayablePeriod(periodId.ToString()).CalendarId.ToString());

            if (model.Vendor == null)
            {
                model.Vendor = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                model.VendorId = model.Vendor.Any() ? Convert.ToInt32(model.Vendor.First().Value) : 0;

                if (model.VendorId > 0)
                {
                    model.VendorSite = VendorHelper.GetVendorSiteList(model.VendorId);
                    model.VendorSiteId = model.VendorSite.Any() ? Convert.ToInt64(model.VendorSite.First().Value) : 0;
                }
            }

            if (model.Bank == null)
            {
                model.Bank = BankHelper.GetBankList(model.SOBId);
                model.BankId = model.Bank.Any() ? Convert.ToInt32(model.Bank.First().Value) : 0;

                if (model.BankId > 0)
                {
                    model.BankAccount = BankHelper.GetBankAccountList(model.BankId);
                    model.BankAccountId = model.BankAccount.Any() ? Convert.ToInt64(model.BankAccount.First().Value) : 0;
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}