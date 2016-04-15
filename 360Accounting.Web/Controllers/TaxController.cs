using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class TaxController : Controller
    {
        public JsonResult ValidateDate(DateTime startDate, DateTime endDate)
        {
            SessionHelper.Tax.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            SessionHelper.Tax.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
            return new JsonResult();
        }

        public ActionResult Delete(string id)
        {
            TaxHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            TaxModel model = TaxHelper.GetTax(id);
            model.TaxDetails = TaxHelper.GetTaxDetail(id);
            SessionHelper.Tax = model;
            return View(model);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", TaxHelper.GetTaxes(SessionHelper.SOBId));
        }

        public ActionResult SaveTax(string taxName, 
            string startDate, string endDate)
        {
            string message = "";
            try
            {
                bool result = false;
                if (SessionHelper.Tax != null)
                {
                    SessionHelper.Tax.EndDate = Convert.ToDateTime(endDate);
                    SessionHelper.Tax.StartDate = Convert.ToDateTime(startDate);
                    SessionHelper.Tax.TaxName = taxName;

                    TaxHelper.Update(SessionHelper.Tax);
                    SessionHelper.Tax = null;
                    result = true;
                    message = "Saved successfully";
                }
                else
                {
                    message = "No information available to save!";
                }

                return Json(new { success = result, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult DeletePartial(TaxDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TaxModel tax = SessionHelper.Tax;
                    TaxHelper.DeleteTaxDetail(model);
                    SessionHelper.Tax = tax;

                    return PartialView("_TaxDetailPartial", TaxHelper.GetTaxDetail());
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TaxDetailPartial");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(TaxDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TaxHelper.UpdateTaxDetail(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TaxDetailPartial", TaxHelper.GetTaxDetail());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(TaxDetailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validInput = false;
                    if (SessionHelper.Tax.TaxDetails.Count != 0)
                    {   
                        if (SessionHelper.Tax.TaxDetails.Any(rec => rec.CodeCombinationId == model.CodeCombinationId))
                            ViewData["EditError"] = "Duplicate accounts can not be added.";
                        else
                        {
                            validInput = true;
                            model.Id = SessionHelper.Tax.TaxDetails.Last().Id + 1;
                        }
                    }
                    else
                    {
                        model.Id = 1;
                        validInput = true;
                    }

                    if (validInput)
                        TaxHelper.Insert(model);
                }
                catch (Exception ex)
                {
                    ViewData["EditError"] = ex.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TaxDetailPartial", TaxHelper.GetTaxDetail());
        }

        public ActionResult TaxDetailPartial()
        {
            return PartialView("_TaxDetailPartial", TaxHelper.GetTaxDetail());
        }

        public ActionResult Create()
        {
            TaxModel model = new TaxModel();
            model.SOBId = SessionHelper.SOBId;
            model.TaxDetails = new List<TaxDetailModel>();
            SessionHelper.Tax = model;
            return View("Edit", model);
        }
        
        public ActionResult Index(TaxListModel model)
        {
            SessionHelper.Tax = null;
            model.SOBId = SessionHelper.SOBId;

            return View(model);
        }
    }
}