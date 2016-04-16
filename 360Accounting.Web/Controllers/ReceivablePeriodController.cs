using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class ReceivablePeriodController : Controller
    {
        public ActionResult Index(ReceivablePeriodListModel model)
        {
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        public ActionResult ListPartial()
        {
            return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
        }

        public ActionResult GetReceivablePeriods(long sobId)
        {
            return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInline(ReceivablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    ReceivablePeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(ReceivablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    ReceivablePeriodHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
        }

        public ActionResult DeleteInline(ReceivablePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ReceivablePeriodHelper.Delete(model.Id.ToString());
                    return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", ReceivablePeriodHelper.GetReceivablePeriods(SessionHelper.SOBId));
        }
    }
}