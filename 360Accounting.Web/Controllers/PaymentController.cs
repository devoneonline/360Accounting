using _360Accounting.Core;
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
using _360Accounting.Core.IService;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private ISetOfBookService sobService;
        private ICalendarService calendarService;
        private ICompanyService companyService;
        private IPaymentService service;
        private ICodeCombinitionService codeCombinitionService;
        //private ICurrencyService currencyService;

        public PaymentController()
        {
            sobService = IoC.Resolve<ISetOfBookService>("SetOfBookService");
            calendarService = IoC.Resolve<ICalendarService>("CalendarService");
            companyService = IoC.Resolve<ICompanyService>("CompanyService");
            service = IoC.Resolve<IPaymentService>("PaymentService");
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
            //currencyService = IoC.Resolve<ICurrencyService>("CurrencyService");
        }

        #region Private Methods
        private List<SelectListItem> getCodeCombinationList(long sobId)
        {
            List<SelectListItem> list = codeCombinitionService.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
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
            PaymentHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long vendorId, long bankId, long periodId)
        {
            PaymentViewModel model = PaymentHelper.GetPayment(id);
            SessionHelper.PeriodId = periodId;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            //SessionHelper.PrecisionLimit = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.CompanyId.Value).Precision;

            if (model.BankAccount == null)
            {
                model.BankAccount = BankHelper.GetBankAccountList(bankId);
                model.BankAccountId = model.BankAccount.Any() ?
                    Convert.ToInt32(model.BankAccount.First().Value) : 0;
            }
            if (model.VendorSite == null)
            {
                model.VendorSite = VendorHelper.GetAllSites(vendorId)
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
            model.PeriodId = periodId;
            model.BankId = bankId;
            model.VendorId = vendorId;
            SessionHelper.Payment = model;

            return View("Create", model);
        }

        public ActionResult Index(PaymentListViewModel model)
        {
            SessionHelper.Payment = null;
            model.SOBId = SessionHelper.SOBId;
            if (model.Period == null)
            {
                model.Period = CalendarHelper.GetCalendars(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
                model.PeriodId = model.Period.Any() ? Convert.ToInt32(model.Period.First().Value) : 0;
            }

            SessionHelper.Calendar = CalendarHelper.GetCalendar(model.PeriodId.ToString());

            if (model.Vendor == null)
            {
                model.Vendor = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                model.VendorId = model.Vendor.Any() ? Convert.ToInt32(model.Vendor.First().Value) : 0;
            }

            if (model.Bank == null)
            {
                model.Bank = BankHelper.GetBankList(model.SOBId);
                model.BankId = model.Bank.Any() ? Convert.ToInt32(model.Bank.First().Value) : 0;
            }
            return View(model);
        }

        //public ActionResult PaymentPartial(long sobId, long periodId, long vendorId, long bankId)
        public ActionResult PaymentPartial(PaymentListViewModel model)
        {
            return PartialView("_List", PaymentHelper.GetPayments(SessionHelper.SOBId, model.BankId, model.VendorId, model.PeriodId));
        }

        public ActionResult Create(long periodId, long vendorId, long bankId)
        {
            SessionHelper.PeriodId = periodId;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            //SessionHelper.PrecisionLimit = CurrencyHelper.GetCurrency(currencyId.ToString()).Precision;

            PaymentViewModel model = SessionHelper.Payment;
            if (model == null)
            {
                model = new PaymentViewModel
                {
                    BankId = bankId,
                    PeriodId = periodId,
                    VendorId = vendorId,
                    SOBId = SessionHelper.SOBId
                };
                if (model.BankAccount == null)
                {
                    model.BankAccount = BankHelper.GetBankAccountList(bankId);
                    model.BankAccountId = model.BankAccount.Any() ?
                        Convert.ToInt32(model.BankAccount.First().Value) : 0;
                }
                if (model.VendorSite == null)
                {
                    model.VendorSite = VendorHelper.GetAllSites(vendorId)
                        .Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();
                    model.VendorSiteId = model.VendorSite.Any() ?
                        Convert.ToInt32(model.VendorSite.First().Value) : 0;
                }

                ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
                ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
                ViewBag.VendorName = VendorHelper.GetSingle(vendorId.ToString()).Name;
                ViewBag.BankName = BankHelper.GetBank(bankId.ToString()).BankName;

                SessionHelper.Payment = model;
                if (SessionHelper.Payment.PaymentInvoiceLines == null)
                    SessionHelper.Payment.PaymentInvoiceLines = new List<PaymentInvoiceLinesModel>();
            }
            
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

        public JsonResult CurrencyList()
        {
            List<SelectListItem> list = CurrencyHelper.GetCurrencies(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodList()
        {
            List<SelectListItem> periodList = CalendarHelper.GetCalendars(SessionHelper.SOBId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankList()
        {
            List<SelectListItem> BankList = BankHelper.GetBankList(SessionHelper.SOBId);
            return Json(BankList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VendorList(long periodId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            List<SelectListItem> VendorList = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
            return Json(VendorList, JsonRequestBehavior.AllowGet);
        }
    }
}