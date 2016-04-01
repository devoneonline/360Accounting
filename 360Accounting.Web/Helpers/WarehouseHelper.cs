using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class WarehouseHelper
    {
        private static IWarehouseService service;

        static WarehouseHelper()
        {
            service = IoC.Resolve<IWarehouseService>("WarehouseService");
        }

        #region Private Methods
        private static Warehouse getEntityByModel(WarehouseModel model)
        {
            if (model == null) return null;

            Warehouse entity = new Warehouse();

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

            entity.WarehouseName = model.WarehouseName;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

        public static WarehouseModel GetWarehouse(string id)
        {
            return new WarehouseModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string Save(WarehouseModel model)
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

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<WarehouseModel> GetWarehouses(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new WarehouseModel(x)).ToList();
        }
    }
}