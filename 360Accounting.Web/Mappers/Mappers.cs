using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class Mappers
    {
        public static ItemWarehouse GetEntityByModel(ItemWarehouseModel model, int count)
        {
            if (model == null)
                return null;

            ItemWarehouse entity = new ItemWarehouse();

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

            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        public static Item GetEntityByModel(ItemModel model)
        {
            if (model == null)
                return null;

            Item entity = new Item();

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

            entity.COGSCodeCombinationId = model.COGSCodeCombinationId;
            entity.DefaultBuyer = model.DefaultBuyer;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.ItemCode = model.ItemCode;
            entity.ItemName = model.ItemName;
            entity.LotControl = model.LotControl;
            entity.Orderable = model.Orderable;
            entity.Purchaseable = model.Purchaseable;
            entity.ReceiptRouting = model.ReceiptRouting;
            entity.SalesCodeCombinationId = model.SalesCodeCombinationId;
            entity.SerialControl = model.SerialControl;
            entity.Shipable = model.Shipable;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static InventoryPeriod GetEntityByModel(InventoryPeriodModel model)
        {
            if (model == null)
                return null;

            InventoryPeriod entity = new InventoryPeriod();
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
    }
}