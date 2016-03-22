using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class WithholdingHelper
    {
        private static IWithholdingService service;

        static WithholdingHelper()
        {
            service = IoC.Resolve<IWithholdingService>("WithholdingService");
        }

        public static string Save(WithholdingModel model)
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

        public static WithholdingModel GetWithholding(string id)
        {
            return new WithholdingModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<WithholdingModel> GetWithholdings(long sobId, long codeCombinitionId, long vendorId)
        {
            return service.GetWithholdings(AuthenticationHelper.User.CompanyId, sobId, codeCombinitionId, vendorId)
                .Select(a => new WithholdingModel(a)).ToList();
        }

        public static List<SelectListItem> GetWithHoldingList()
        {
            throw new NotImplementedException();
        }
    }
}