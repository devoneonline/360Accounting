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
    public class PayablePeriodController : Controller
    {
        public ActionResult Index(PayablePeriodListModel model)
        {
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
        }

        public ActionResult GetPayablePeriods()
        {
            return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInline(PayablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    PayablePeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(PayablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    PayablePeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
        }

        public ActionResult DeleteInline(PayablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PayablePeriodHelper.Delete(model.Id.ToString());
                    return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", PayablePeriodHelper.GetPayablePeriods(SessionHelper.SOBId));
        }
    }
}