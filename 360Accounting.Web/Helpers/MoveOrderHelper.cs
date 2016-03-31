using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public class MoveOrderHelper
    {
        private static IMoveOrderService service;

        static MoveOrderHelper()
        {
            service = IoC.Resolve<IMoveOrderService>("MoveOrderService");
        }

        #region Private Methods
        private static MoveOrder GetEntityByModel(MoveOrderModel model)
        {
            if (model == null) return null;

            MoveOrder entity = new MoveOrder();
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

            entity.DateRequired = model.DateRequired;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.MoveOrderDate = model.MoveOrderDate;
            entity.MoveOrderNo = model.MoveOrderNo;
            entity.SOBId = model.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static MoveOrderDetail GetEntityByModel(MoveOrderDetailModel model, int count)
        {
            if (model == null) return null;

            MoveOrderDetail entity = new MoveOrderDetail();
            if (count == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.DateRequired = model.DateRequired;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.MoveOrderId = model.MoveOrderId;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        #endregion
    }
}