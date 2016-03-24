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
            List<SelectListItem> list = codeCombinitionService.GetAll(AuthenticationHelper.User.CompanyId, sobId)
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
            JVHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long vendorId, long sobId, long bankId, long periodId)
        {
            PaymentViewModel model = PaymentHelper.GetPayment(id);
            SessionHelper.SOBId = model.SOBId;
            SessionHelper.Calendar = new CalendarViewModel(calendarService.GetSingle(periodId.ToString(), AuthenticationHelper.User.CompanyId));
            //SessionHelper.PrecisionLimit = currencyService.GetSingle(currencyId.ToString(), AuthenticationHelper.User.CompanyId).Precision;

            model.PaymentInvoiceLines = PaymentHelper.GetPaymentLines(id).ToList();
            model.SOBId = sobId;
            model.PeriodId = periodId;
            model.BankId = bankId;
            model.VendorId = vendorId;
            SessionHelper.Payment = model;

            return View("Create", model);
        }

        public ActionResult Index(PaymentListViewModel model)
        {
            SessionHelper.Payment = null;
            if (model.SetOfBook == null)
            {
                model.SetOfBook = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                model.SOBId = model.SetOfBook.Any() ?
                    Convert.ToInt32(model.SetOfBook.First().Value) : 0;
            }

            if (model.Period == null && model.SetOfBook.Any())
            {
                model.Period = CalendarHelper.GetCalendars(Convert.ToInt32(model.SetOfBook.First().Value))
                    .Select(x => new SelectListItem
                    {
                        Text = x.PeriodName,
                        Value = x.Id.ToString()
                    }).ToList();
                model.PeriodId = model.Period.Any() ? Convert.ToInt32(model.Period.First().Value) : 0;
            }

            //if (model.Currencies == null && model.SetOfBooks.Any())
            //{
            //    model.Currencies = CurrencyHelper.GetCurrencyList(Convert.ToInt32(model.SetOfBooks.First().Value))
            //        .Select(x => new SelectListItem
            //        {
            //            Text = x.Name,
            //            Value = x.Id.ToString()
            //        }).ToList();
            //    model.CurrencyId = model.Currencies.Any() ? Convert.ToInt32(model.Currencies.First().Value) : 0;

            //}
            return View(model);
        }

        public ActionResult PaymentPartial(long sobId, long periodId, long vendorId, long bankId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", PaymentHelper.GetPayments(sobId, bankId, vendorId, periodId));
        }

        public ActionResult Create(long sobId, long periodId, long vendorId, long bankId)
        {
            SessionHelper.SOBId = sobId;
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
                    SOBId = sobId
                };
                if (model.BankAccount == null)
                {
                    model.BankAccount = BankHelper.GetBankAccountList(bankId);
                    model.BankAccountId = model.BankAccount.Any() ?
                        Convert.ToInt32(model.BankAccount.First().Value) : 0;
                }
                if (model.VendorSite == null)
                {
                    model.VendorSite = VendorHelper.GetAllSites(bankId)
                        .Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();
                    model.VendorSiteId = model.VendorSite.Any() ?
                        Convert.ToInt32(model.VendorSite.First().Value) : 0;
                }
                SessionHelper.Payment = model;
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
            return PartialView("createPartial", JVHelper.GetGLLines());
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
                        model.Id = SessionHelper.Payment.PaymentInvoiceLines.Count() + 1;
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

        public ActionResult SavePayment(PaymentViewModel model, string paymentName, string paymentDate, string cRate, string descr)
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
                    SessionHelper.Payment.SOBId = model.SOBId;
                    SessionHelper.Payment.Status = model.Status;
                    SessionHelper.Payment.VendorId = model.VendorId;
                    SessionHelper.Payment.VendorSiteId = model.VendorSiteId;
                    
                    SessionHelper.Payment.PaymentNo = PaymentHelper.GetPaymentNo(AuthenticationHelper.User.CompanyId, model.VendorId, model.SOBId, model.BankId, model.PeriodId);

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

        public JsonResult CurrencyList(long sobId)
        {
            List<SelectListItem> list = CurrencyHelper.GetCurrencyList(sobId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
    }
}