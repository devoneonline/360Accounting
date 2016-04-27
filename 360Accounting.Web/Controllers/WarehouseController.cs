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
    public class WarehouseController : BaseController
    {
        public ActionResult Index(WarehouseListModel model)
        {
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
        }

        public ActionResult GetWarehouses()
        {
            return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInline(WarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    WarehouseHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(WarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    model.SOBId = SessionHelper.SOBId;
                    WarehouseHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
        }

        public ActionResult DeleteInline(WarehouseModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WarehouseHelper.Delete(model.Id.ToString());
                    return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", WarehouseHelper.GetWarehouses(SessionHelper.SOBId));
        }
    }
}