using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class MiscellaneousTransactionHelper
    {
        private static IMiscellaneousTransactionService service;

        static MiscellaneousTransactionHelper()
        {
            service = IoC.Resolve<IMiscellaneousTransactionService>("MiscellaneousTransactionService");
        }

        #region Private Methods
        private static MiscellaneousTransaction GetEntityByModel(MiscellaneousTransactionDetailModel model, int count)
        {
            if (model == null) return null;

            MiscellaneousTransaction entity = new MiscellaneousTransaction();

            if (count == 0)
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

            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Cost = model.Cost;
            entity.Id = model.Id;
            entity.ItemId = model.ItemId;
            entity.LocatorId = model.LocatorId;
            entity.LotNo = model.LotNo;
            entity.Quantity = model.Quantity;
            entity.SerialNo = model.SerialNo;
            entity.SOBId = model.SOBId;
            entity.TransactionDate = model.TransactionDate;
            entity.TransactionType = model.TransactionType;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.WarehouseId = model.WarehouseId;
            return entity;
        }
        #endregion
    }
}