using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public  class CompanyHelper
    {
        private static ICompanyService service;

        static CompanyHelper()
        {
            service = IoC.Resolve<ICompanyService>("CompanyService");
        }

        public static IEnumerable<CompanyModel> GetCompanies()
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId)
                .Select(x => new CompanyModel(x)).ToList();
        }

        public static string SaveCompany(CompanyModel model)
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

        public static CompanyModel GetCompany(string id)
        {
            return new CompanyModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void DeleteCompany(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}