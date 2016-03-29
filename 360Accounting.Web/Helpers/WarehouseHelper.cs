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

        public static WarehouseModel GetWarehouse(string id)
        {
            return new WarehouseModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string Save(WarehouseModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(Mappers.GetEntityByModel(model));
            }
            else
            {
                return service.Insert(Mappers.GetEntityByModel(model));
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