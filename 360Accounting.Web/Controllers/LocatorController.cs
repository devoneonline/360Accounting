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
using _360Accounting.Web.Helpers;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class LocatorController : BaseController
    {
        public ActionResult Delete(string id)
        {
            try
            {
                LocatorHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult Edit(string id)
        {
            LocatorModel model = LocatorHelper.GetLocator(id);
            model.LocatorWarehouses = LocatorHelper.GetLocatorWarehouses(id).ToList();
            model.SOBId = SessionHelper.SOBId;
            model.CompanyId = AuthenticationHelper.CompanyId.Value;
            SessionHelper.Locator = model;

            return View("Create", model);
        }

        public ActionResult Index(LocatorListModel model, string message = "")
        {
            ViewBag.ErrorMessage = message;
            SessionHelper.Locator = null;
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult LocatorPartial(LocatorListModel model)
        {
            return PartialView("_List", LocatorHelper.GetLocatorsCombo(SessionHelper.SOBId));
        }

        public ActionResult Create()
        {
            LocatorModel model = SessionHelper.Locator;
            if (model == null)
            {
                model = new LocatorModel
                {
                    SOBId = SessionHelper.SOBId
                };
                ViewBag.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;

                SessionHelper.Locator = model;
                if (SessionHelper.Locator.LocatorWarehouses == null)
                    SessionHelper.Locator.LocatorWarehouses = new List<LocatorWarehouseModel>();
            }

            model.CompanyId = AuthenticationHelper.CompanyId.Value;
            
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("createPartial", LocatorHelper.GetLocatorWarehouses());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewPartial(LocatorWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool validated = false;
                    model.SOBId = SessionHelper.SOBId;
                    if (SessionHelper.Locator != null)
                    {
                        model.Id = SessionHelper.Locator.LocatorWarehouses.Last().Id + 1;
                        validated = true;
                    }
                    else
                        model.Id = 1;


                    if (SessionHelper.Locator.LocatorWarehouses.Count > 0)
                    {
                        foreach (var item in SessionHelper.Locator.LocatorWarehouses)
                        {
                            if (model.StartDate <= item.EndDate || model.EndDate <= item.StartDate)
                            {
                                ViewData["EditError"] = "Dates are overlapping.";
                                validated = false;
                            }
                        }
                    }
                    
                    if (model.StartDate > model.EndDate)
                    {
                        ViewData["EditError"] = "End Date cannot be less than Start Date.";
                        validated = false;
                    }
                    
                    if (validated)
                            LocatorHelper.InsertLocatorWarehouse(model);
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", LocatorHelper.GetLocatorWarehouses());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(LocatorWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                model.SOBId = SessionHelper.SOBId;
                try
                {
                    bool validated = true;
                    if (SessionHelper.Locator.LocatorWarehouses.Count > 0)
                    {
                        foreach (var item in SessionHelper.Locator.LocatorWarehouses)
                        {
                            if ((model.StartDate <= item.EndDate || model.EndDate <= item.StartDate) && model.Id != item.Id)
                            {
                                validated = false;
                                ViewData["EditError"] = "Dates are overlapping.";
                            }
                        }
                    }

                    if (model.StartDate > model.EndDate)
                    {
                        validated = false;
                        ViewData["EditError"] = "End Date cannot be less than Start Date.";
                    }
                    
                    if (validated)
                        LocatorHelper.UpdateLocatorWarehouse(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("createPartial", LocatorHelper.GetLocatorWarehouses());
        }

        public ActionResult DeletePartial(LocatorWarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LocatorModel header = SessionHelper.Locator;
                    LocatorHelper.DeleteLocatorWarehouse(model);
                    SessionHelper.Locator = header;
                    IList<LocatorWarehouseModel> LocatorWarehouses = LocatorHelper.GetLocatorWarehouses();
                    return PartialView("createPartial", LocatorWarehouses);
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

        public ActionResult Save(LocatorModel model)
        {
            string message = "";
            try
            {
                bool saved = false;
                if (SessionHelper.Locator != null)
                {
                    SessionHelper.Locator.CompanyId = model.CompanyId;
                    SessionHelper.Locator.Description = string.IsNullOrEmpty(model.Description) ? "" : model.Description;
                    SessionHelper.Locator.Id = model.Id;
                    SessionHelper.Locator.SOBId = SessionHelper.SOBId;
                    SessionHelper.Locator.Status = model.Status;

                    LocatorHelper.Save(SessionHelper.Locator);
                    SessionHelper.Locator = null;
                    saved = true;

                }
                else
                    message = "No Locator information available!";
                return Json(new { success = saved, message = message });
            }
            catch (Exception e)
            {
                message = e.Message;
                return Json(new { success = false, message = message });
            }
        }
    }
}