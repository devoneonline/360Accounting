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
    public class RFQController : Controller
    {
        private IRFQService service;

        public RFQController()
        {
            service = IoC.Resolve<IRFQService>("RFQService");
        }

        #region Private Methods
        private RFQ GetEntityByModel(RFQModel model)
        {
            if (model == null) return null;
            RFQ entity = new RFQ();
            entity.BuyerId = model.BuyerId;
            entity.CloseDate = model.CloseDate;
            entity.Id = model.Id;
            entity.RFQDate = model.RFQDate;
            entity.RFQNo = model.RFQNo;
            entity.SOBId = SessionHelper.SOBId;
            entity.Status = model.Status;
            
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

        public RFQDetail getEntityByModel(RFQDetailModel model)
        {
            if (model == null) return null;
            RFQDetail entity = new RFQDetail();

            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.Quantity = model.Quantity;
            entity.RFQId = model.RFQId;
            entity.TargetPrice = model.TargetPrice;
            
            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

        #region Action Results

        public ActionResult ListPartial()
        {
            return PartialView("_List", OrderHelper.GetOrders());
        }

        public ActionResult Index()
        {
            SessionHelper.RFQ = null;
            return View();
        }

        #endregion
    }
}