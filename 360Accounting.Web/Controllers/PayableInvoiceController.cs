using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class PayableInvoiceController : BaseController
    {
        public ActionResult SaveInvoice(long periodId, long invoiceTypeId, 
            string invoiceDate, string remarks, long vendorId, long vendorSiteId, 
            long? whTaxId, string amount, string status)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.PayableInvoice != null)
                {
                    if (SessionHelper.PayableInvoice.InvoiceDetail.Count == 0)
                    {
                        message = "No detail information available to save!";
                    }
                    else 
                    {
                        SessionHelper.PayableInvoice.PeriodId = periodId;
                        SessionHelper.PayableInvoice.InvoiceTypeId = invoiceTypeId;
                        SessionHelper.PayableInvoice.InvoiceDate = Convert.ToDateTime(invoiceDate);
                        SessionHelper.PayableInvoice.Remarks = remarks;
                        SessionHelper.PayableInvoice.VendorId = vendorId;
                        SessionHelper.PayableInvoice.VendorSiteId = vendorSiteId;
                        SessionHelper.PayableInvoice.WHTaxId = whTaxId;
                        SessionHelper.PayableInvoice.Amount = SessionHelper.PayableInvoice.InvoiceDetail.Sum(x => x.Amount);
                        SessionHelper.PayableInvoice.Status = status;
                        if (SessionHelper.PayableInvoice.InvoiceNo == "New")
                        {
                            SessionHelper.PayableInvoice.InvoiceNo = PayableInvoiceHelper.GetInvoiceNo(AuthenticationHelper.CompanyId.Value, SessionHelper.PayableInvoice.SOBId, SessionHelper.PayableInvoice.PeriodId);
                        }                    

                        PayableInvoiceHelper.Update(SessionHelper.PayableInvoice);
                        SessionHelper.PayableInvoice = null;
                        saved = true;
                        message = "Saved successfully";
                    }
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

        public ActionResult DeletePartial(PayableInvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PayableInvoiceModel invoice = SessionHelper.PayableInvoice;
                    PayableInvoiceHelper.DeleteInvoiceDetail(model);
                    SessionHelper.PayableInvoice = invoice;
                    IList<PayableInvoiceDetailModel> invoiceDetail = PayableInvoiceHelper.GetInvoiceDetail();
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
        public ActionResult UpdatePartial(PayableInvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PayableInvoiceHelper.UpdateInvoiceDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", PayableInvoiceHelper.GetInvoiceDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(PayableInvoiceDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SessionHelper.PayableInvoice != null)
                    {
                        if (SessionHelper.PayableInvoice.InvoiceDetail != null && SessionHelper.PayableInvoice.InvoiceDetail.Count > 0)
                            model.Id = SessionHelper.PayableInvoice.InvoiceDetail.LastOrDefault().Id + 1;
                        else
                            model.Id = 1;
                    }
                    else
                        model.Id = 1;

                    PayableInvoiceHelper.Insert(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_Detail", PayableInvoiceHelper.GetInvoiceDetail());
        }

        public ActionResult DetailPartial()
        {
            return PartialView("_Detail", PayableInvoiceHelper.GetInvoiceDetail());
        }

        public JsonResult VendorList()
        {
            List<SelectListItem> vendorList = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
            return Json(vendorList, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult InvoiceTypeList()
        {
            List<SelectListItem> invoiceTypeList = InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
            return Json(invoiceTypeList, JsonRequestBehavior.AllowGet);
        }

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

        public JsonResult PeriodList()
        {
            List<SelectListItem> periodList = PayablePeriodHelper.GetPeriodList(SessionHelper.SOBId);
            return Json(periodList, JsonRequestBehavior.AllowGet);
        }

        public void AddCalendarInSession(long periodId)
        {
            SessionHelper.Calendar = CalendarHelper.GetCalendar(PayablePeriodHelper.GetPayablePeriod(periodId.ToString()).CalendarId.ToString());
        }

        public ActionResult Create()
        {
            PayableInvoiceModel model = SessionHelper.PayableInvoice;
            if (model == null)
            {
                model = new PayableInvoiceModel
                {
                    CompanyId = AuthenticationHelper.CompanyId.Value,
                    InvoiceDetail = new List<PayableInvoiceDetailModel>(),
                    Periods = new List<SelectListItem>(),
                    Vendors = new List<SelectListItem>(),
                    VendorSites = new List<SelectListItem>(),
                    WHTaxes = new List<SelectListItem>(),
                    InvoiceNo = "New",
                    SOBId = SessionHelper.SOBId                    
                };
            }

            model.Periods = PayablePeriodHelper.GetPeriodList(SessionHelper.SOBId);

            if (model.Periods != null && model.Periods.Count() > 0)
            {
                model.PeriodId = Convert.ToInt64(model.Periods.FirstOrDefault().Value);
                SessionHelper.Calendar = CalendarHelper.GetCalendar(model.PeriodId.ToString());
                model.InvoiceDate = SessionHelper.Calendar.StartDate;
                
                model.InvoiceTypes = InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);

                if (model.InvoiceTypes != null && model.InvoiceTypes.Count() > 0)
                {
                    model.InvoiceTypeId = Convert.ToInt64(model.InvoiceTypes.FirstOrDefault().Value);
                }

                model.Vendors = VendorHelper.GetVendorList(SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);

                if (model.Vendors != null && model.Vendors.Count() > 0)
                {
                    model.VendorId = Convert.ToInt64(model.Vendors.FirstOrDefault().Value);
                    model.VendorSites = VendorHelper.GetVendorSiteList(model.VendorId);
                    
                    if (model.VendorSites != null && model.VendorSites.Count() > 0)
                    {
                        model.VendorSiteId = Convert.ToInt64(model.VendorSites.FirstOrDefault().Value);
                        model.WHTaxes = WithholdingHelper.GetWithHoldingList(model.VendorId, model.VendorSiteId, SessionHelper.Calendar.StartDate, SessionHelper.Calendar.EndDate);
                            
                        if (model.WHTaxId != null && model.WHTaxes.Count() > 0)
                        {
                            model.WHTaxId = Convert.ToInt64(model.WHTaxes.FirstOrDefault().Value);
                        }
                    }
                }
            }

            SessionHelper.PayableInvoice = model;
            return View("Edit", model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                PayableInvoiceHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult Edit(string id, long periodId)
        {
            PayableInvoiceModel model = PayableInvoiceHelper.GetInvoice(id);
            SessionHelper.Calendar = CalendarHelper.GetCalendar(PayablePeriodHelper.GetPayablePeriod(periodId.ToString()).CalendarId.ToString());

            model.InvoiceDetail = PayableInvoiceHelper.GetInvoiceDetail(id);
            model.SOBId = SessionHelper.SOBId;
            model.PeriodId = periodId;
            model.Periods = PayablePeriodHelper.GetPeriodList(SessionHelper.SOBId);

            VendorModel vendors = VendorHelper.GetSingle(model.VendorId.ToString());
            VendorSiteModel vendorSites = VendorHelper.GetSingle(model.VendorSiteId);
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
            if (model.WHTaxId != null)
            {
                WithholdingModel withHoldingTaxes = WithholdingHelper.GetWithholding(model.WHTaxId.ToString());
                model.WHTaxes.Add(new SelectListItem
                {
                    Value = withHoldingTaxes.Id.ToString(),
                    Text = withHoldingTaxes.Description
                });
            }
            
            model.InvoiceTypes = new List<SelectListItem>();
            model.InvoiceTypes.Add(new SelectListItem
            {
                Value = invoiceTypes.Id.ToString(),
                Text = invoiceTypes.Description
            });

            SessionHelper.PayableInvoice = model;
            return View(model);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", PayableInvoiceHelper
                .GetInvoices(SessionHelper.SOBId));
        }

        public ActionResult Index(PayableInvoiceListModel model, string message="")
        {
            try
            {
                ViewBag.ErrorMessage = message;
                SessionHelper.PayableInvoice = null;
                model.SOBId = SessionHelper.SOBId;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
                return View(model);
            }
        }
    }
}