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
    public class BuyerController : Controller
    {
        private IBuyerService service;

        public BuyerController()
        {
            service = IoC.Resolve<IBuyerService>("BuyerService");
        }

        #region Private methods
        private Buyer GetEntityByModel(BuyerModel model)
        {
            if (model == null) return null;
            Buyer entity = new Buyer();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.SOBId = SessionHelper.SOBId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
                entity.CompanyId = model.CompanyId;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private string Save(BuyerModel model)
        {
            Buyer entity = GetEntityByModel(model);
            return model.Id > 0
                ? service.Update(entity)
                : service.Insert(entity);
        }
        #endregion

        [HttpPost]
        public ActionResult Create(BuyerModel model)
        {
            if (ModelState.IsValid)
            {
                Save(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new BuyerModel());
        }

        public ActionResult BuyerListPartial()
        {
            return PartialView("_List", service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new BuyerModel(x)).ToList());
        }
        
        public ActionResult Index()
        {
            IEnumerable<BuyerModel> modelList = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(x => new BuyerModel(x)).ToList();
            return View(modelList);
        }
    }
}