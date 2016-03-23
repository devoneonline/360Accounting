using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class PayableInvoiceController : Controller
    {
        public JsonResult WHTaxList(long vendorId, long vendorSiteId)
        {
            List<SelectListItem> whTaxList = WithholdingHelper.GetWithHoldingList(vendorId, vendorSiteId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
            return Json(whTaxList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VendorSiteList(long vendorId)
        {
            List<SelectListItem> vendorList = VendorHelper.GetVendorSiteList(vendorId);
            return Json(vendorList, JsonRequestBehavior.AllowGet);
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

        public ActionResult Create(long sobId, long periodId)
        {
            SessionHelper.SOBId = sobId;

            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            
            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            
            PayableInvoiceModel model = SessionHelper.PayableInvoice;
            if (model == null)
            {
                List<SelectListItem> vendors = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                List<SelectListItem> vendorSites = VendorHelper.GetVendorSiteList(vendors.Any() ? Convert.ToInt64(vendors.First().Value) : 0);
                List<SelectListItem> whTaxes = WithholdingHelper.GetWithHoldingList(vendors.Any() ? Convert.ToInt64(vendors.First().Value) : 0,
                    vendorSites.Any() ? Convert.ToInt64(vendorSites.First().Value) : 0, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                List<SelectListItem> invoiceTypes = InvoiceTypeHelper.GetInvoiceTypes(model.SOBId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);

                model = new PayableInvoiceModel
                {
                    CompanyId = AuthenticationHelper.User.CompanyId,
                    Vendors = vendors,
                    VendorId = vendors.Any() ?
                    Convert.ToInt64(vendors.First().Value) : 0,
                    VendorSites = vendorSites,
                    VendorSiteId = vendorSites.Any() ?
                    Convert.ToInt64(vendorSites.First().Value) : 0,
                    InvoiceDate = SessionHelper.Calendar.StartDate,
                    InvoiceDetail = new List<PayableInvoiceDetailModel>(),
                    InvoiceNo = "New",
                    InvoiceTypes = invoiceTypes,
                    InvoiceTypeId = invoiceTypes.Any() ?
                    Convert.ToInt64(invoiceTypes.First().Value) : 0,
                    PeriodId = periodId,
                    SOBId = sobId,
                };
                SessionHelper.PayableInvoice = model;
            }
            return View("Edit", model);
        }

        public JsonResult PeriodList(long sobId)
        {
            List<SelectListItem> periodList = CalendarHelper.GetCalendarsList(sobId);
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            PayableInvoiceHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, long sobId, long periodId)
        {
            PayableInvoiceModel model = PayableInvoiceHelper.GetInvoice(id);
            SessionHelper.SOBId = model.SOBId;
            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            
            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            
            model.InvoiceDetail = PayableInvoiceHelper.GetInvoiceDetail(id);
            model.SOBId = sobId;
            model.PeriodId = periodId;

            VendorModel vendors = VendorHelper.GetSingle(model.VendorId.ToString());
            VendorModel vendorSites = VendorHelper.GetSingle(model.VendorSiteId.ToString());
            WithholdingModel withHoldingTaxes = WithholdingHelper.GetWithholding(model.WHTaxId.ToString());
            InvoiceTypeModel invoiceTypes = InvoiceTypeHelper.GetInvoiceType(model.InvoiceTypeId.ToString());

            ///TODO: Plz do the code audit.
            model.Vendors = new List<SelectListItem>();
            model.Vendors.Add(new SelectListItem
            {
                Value = vendors.Id.ToString(),
                Text = vendors.Name
            });
            model.VendorSites = new List<SelectListItem>();
            model.VendorSites.Add(new SelectListItem
            {
                Value = vendorSites.Id.ToString(),
                Text = vendorSites.Name
            });

            model.WHTaxes = new List<SelectListItem>();
            model.WHTaxes.Add(new SelectListItem
            {
                Value = withHoldingTaxes.Id.ToString(),
                Text = withHoldingTaxes.Description
            });

            model.InvoiceTypes = new List<SelectListItem>();
            model.InvoiceTypes.Add(new SelectListItem
            {
                Value = invoiceTypes.Id.ToString(),
                Text = invoiceTypes.Description
            });

            SessionHelper.PayableInvoice = model;
            return View(model);
        }

        public ActionResult ListPartial(long sobId, long periodId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", PayableInvoiceHelper
                .GetInvoices(sobId, periodId));
        }

        public ActionResult ListByModelPartial(InvoiceListModel model)
        {
            SessionHelper.SOBId = model.SOBId;
            return PartialView("_List", PayableInvoiceHelper
                .GetInvoices(model.SOBId, model.PeriodId));
        }

        public ActionResult Index(PayableInvoiceListModel model)
        {
            SessionHelper.PayableInvoice = null;
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBookList();
                model.SOBId = model.SetOfBooks.Any() ?
                    Convert.ToInt32(model.SetOfBooks.First().Value) : 0;
            }

            if (model.Periods == null && model.SetOfBooks.Any())
            {
                model.Periods = CalendarHelper.GetCalendarsList(Convert.ToInt64(model.SetOfBooks.First().Value));
                model.PeriodId = model.Periods.Any() ?
                    Convert.ToInt32(model.Periods.First().Value) : 0;
            }

            return View(model);
        }
    }
}