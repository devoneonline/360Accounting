using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class PurchasingPeriodController : Controller
    {
        private IPurchasingPeriodService service;

        public PurchasingPeriodController()
        {
            service = IoC.Resolve<IPurchasingPeriodService>("PurchasingPeriodService");
        }

        #region Private Methods
        private string Save(PurchasingPeriodModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(getEntityByModel(model));
            }
            else
            {
                return service.Insert(getEntityByModel(model));
            }
        }

        private PurchasingPeriod getEntityByModel(PurchasingPeriodModel model)
        {
            if (model == null)
                return null;

            PurchasingPeriod entity = new PurchasingPeriod();
            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.CalendarId = model.CalendarId;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

        public ActionResult DeleteInline(PurchasingPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    service.Delete(model.Id.ToString(), model.CompanyId);
                    return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                        .Select(x => new PurchasingPeriodModel(x)).ToList());
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new PurchasingPeriodModel(x)).ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(PurchasingPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    model.SOBId = SessionHelper.SOBId;
                    Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new PurchasingPeriodModel(x)).ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInLine(PurchasingPeriodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CompanyId = AuthenticationHelper.CompanyId.Value;
                    model.SOBId = SessionHelper.SOBId;
                    Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new PurchasingPeriodModel(x)).ToList());
        }

        public ActionResult GetPurchasingPeriods()
        {
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new PurchasingPeriodModel(x)).ToList());
        }

        public ActionResult CreatePartial()
        {
            IEnumerable<PurchasingPeriodModel> model = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new PurchasingPeriodModel(x)).ToList();
            return PartialView("_List", model);
        }

        // GET: PurchasingPeriod
        public ActionResult Index(PurchasingPeriodListModel model)
        {
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }
    }
}