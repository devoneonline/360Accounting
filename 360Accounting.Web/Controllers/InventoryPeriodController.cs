using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using _360Accounting.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class InventoryPeriodController : Controller
    {
        public ActionResult Index(InventoryPeriodListModel model)
        {
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            IEnumerable<InventoryPeriodModel> model = InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId);
            return PartialView("_List", model);
        }

        public ActionResult GetInventoryPeriods()
        {
            return PartialView("_List", InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInline(InventoryPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.User.CompanyId;
                    model.SOBId = SessionHelper.SOBId;
                    InventoryPeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(InventoryPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.User.CompanyId;
                    model.SOBId = SessionHelper.SOBId;
                    InventoryPeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId));
        }

        public ActionResult DeleteInline(InventoryPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.User.CompanyId;
                    InventoryPeriodHelper.Delete(model.Id.ToString());
                    return PartialView("_List", InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InventoryPeriodHelper.GetInventoryPeriods(SessionHelper.SOBId));
        }
    }
}