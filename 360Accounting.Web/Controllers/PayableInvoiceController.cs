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
        public ActionResult Create(long sobId, long periodId)
        {
            SessionHelper.SOBId = sobId;

            SessionHelper.Calendar = CalendarHelper.GetCalendar(periodId.ToString());
            
            ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
            ViewBag.PeriodName = SessionHelper.Calendar.PeriodName;
            
            PayableInvoiceModel model = SessionHelper.PayableInvoice;
            if (model == null)
            {
                List<SelectListItem> vendors = VendorHelper.GetVendorList();

                List<SelectListItem> vendorSites = VendorHelper.GetVendorSiteList(vendors.Any() ? Convert.ToInt32(vendors.First().Value) : 0);

                List<SelectListItem> whTaxes = WithholdingHelper.GetWithHoldingList();

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

            ///TODO: Plz do the code audit.
            model.Vendors = new List<SelectListItem>();
            model.Vendors.Add(new SelectListItem
            {
                Value = VendorHelper.GetSingle(model.VendorId).Id.ToString(),
                Text = VendorHelper.GetSingle(model.VendorId).Name
            });
            model.VendorSites = new List<SelectListItem>();
            model.VendorSites.Add(new SelectListItem
            {
                Value = VendorHelper.GetSingle(model.VendorSiteId).Id.ToString(),
                Text = VendorHelper.GetSingle(model.VendorSiteId).Name
            });

            model.WHTaxes = new List<SelectListItem>();
            model.WHTaxes.Add(new SelectListItem
            {
                Value = WithholdingHelper.GetWithholding(model.WHTaxId.ToString()).Id.ToString(),
                Text = WithholdingHelper.GetWithholding(model.WHTaxId.ToString()).Description
            });

            model.InvoiceTypes = new List<SelectListItem>();
            model.InvoiceTypes.Add(new SelectListItem
            {
                Value = InvoiceTypeHelper.GetInvoiceType(model.InvoiceTypeId.ToString()).Id.ToString(),
                Text = InvoiceTypeHelper.GetInvoiceType(model.InvoiceTypeId.ToString()).Description
            });

            SessionHelper.PayableInvoice = model;
            return View(model);
        }

        public ActionResult InvoicePartial(long sobId, long periodId)
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