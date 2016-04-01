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
        //public static ItemWarehouse GetEntityByModel(ItemWarehouseModel model)
        //{
        //    if (model == null) return null;

        //    ItemWarehouse entity = new ItemWarehouse
        //    {
        //        Id = model.Id,
        //        EndDate = model.EndDate,
        //        ItemId = model.ItemId,
        //        SOBId = model.SOBId,
        //        StartDate = model.StartDate,
        //        WarehouseId = model.WarehouseId
        //    };
        //    if (model.Id == 0)
        //    {
        //        entity.CreateBy = AuthenticationHelper.UserId;
        //        entity.CreateDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        entity.CreateBy = model.CreateBy;
        //        entity.CreateDate = model.CreateDate;
        //    }
        //    entity.UpdateBy = AuthenticationHelper.UserId;
        //    entity.UpdateDate = DateTime.Now;
        //    return entity;
        //}
    }
}