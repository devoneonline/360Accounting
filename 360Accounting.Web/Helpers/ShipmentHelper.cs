using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class ShipmentHelper
    {
        private static IShipmentService service;

        static ShipmentHelper()
        {
            service = IoC.Resolve<IShipmentService>("ShipmentService");
        }

        #region Private Methods
        private static Shipment getEntityByModel(ShipmentModel model)
        {
            if (model == null) return null;

            Shipment entity = new Shipment();

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

            entity.DeliveryDate = model.DeliveryDate;
            entity.Id = model.Id;
            entity.LineId = model.LineId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.OrderId = model.OrderId;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.SOBId = SessionHelper.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;

            return entity;
        }
        #endregion
    }
}