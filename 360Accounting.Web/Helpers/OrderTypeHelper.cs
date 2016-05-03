using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public class OrderTypeHelper
    {
        private static IOrderTypeService service;

        static OrderTypeHelper()
        {
            service = IoC.Resolve<IOrderTypeService>("OrderTypeService");
        }

        #region Private Methods
        private static OrderType getEntityByModel(OrderTypeModel model)
        {
            if (model == null) return null;

            OrderType entity = new OrderType();

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

            entity.DateFrom = model.DateFrom;
            entity.DateTo = model.DateTo;
            entity.Id = model.Id;
            entity.OrderTypeName = model.OrderTypeName;
            entity.SOBId = SessionHelper.SOBId;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }
        #endregion
    }
}