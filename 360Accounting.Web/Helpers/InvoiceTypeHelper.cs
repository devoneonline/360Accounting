using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class InvoiceTypeHelper
    {
        private static IInvoiceTypeService service;

        static InvoiceTypeHelper()
        {
            service = IoC.Resolve<IInvoiceTypeService>("InvoiceTypeService");
        }

        public static string Save(InvoiceTypeModel model)
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

        public static InvoiceTypeModel GetInvoiceType(string id)
        {
            return new InvoiceTypeModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<InvoiceTypeModel> GetInvoiceTypes(long sobId)
        {
            return service.GetInvoiceTypes(sobId, AuthenticationHelper.User.CompanyId)
                .Select(a => new InvoiceTypeModel(a)).ToList();
        }
    }
}