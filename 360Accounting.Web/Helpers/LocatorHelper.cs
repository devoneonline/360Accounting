using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class LocatorHelper
    {
        private static ILocatorService service;

        static LocatorHelper()
        {
            service = IoC.Resolve<ILocatorService>("LocatorService");
        }

        #region Private Methods
        private static LocatorWarehouse getEntityByModel(LocatorWarehouseModel model, int count)
        {
            if (model == null) return null;

            LocatorWarehouse entity = new LocatorWarehouse();

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
            entity.LocatorId = model.LocatorId;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }

        private static Locator getEntityByModel(LocatorModel model)
        {
            if (model == null) return null;

            Locator entity = new Locator();
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

            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            entity.Status = model.Status;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

    }
}