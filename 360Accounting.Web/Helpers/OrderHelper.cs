using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class OrderHelper
    {
        private static IOrderService service;

        static OrderHelper()
        {
            service = IoC.Resolve<IOrderService>("OrderService");
        }

        #region Private Methods
        private static Order getEntityByModel(OrderModel model)
        {
            if (model == null) return null;

            Order entity = new Order();

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

            entity.CustomerId = model.CustomerId;
            entity.CustomerSiteId = model.CustomerSiteId;
            entity.Id = model.Id;
            entity.OrderDate = model.OrderDate;
            entity.OrderNo = model.OrderNo;
            entity.OrderTypeId = model.OrderTypeId;
            entity.Remarks = model.Remarks;
            entity.SOBId = SessionHelper.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static OrderDetail getEntityByModel(OrderDetailModel model)
        {
            if (model == null) return null;

            OrderDetail entity = new OrderDetail();

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

            entity.Amount = model.Amount;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.OrderId = model.OrderId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.TaxId = model.TaxId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;

            return entity;
        }
        #endregion
    }
}