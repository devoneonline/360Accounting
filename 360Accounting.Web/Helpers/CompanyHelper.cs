using _360Accounting.Core;
using _360Accounting.Core.Entities;
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

        private static Company getEntityByModel(CompanyModel model)
        {
            if (model == null) return null;

            return new Company
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static IEnumerable<CompanyModel> GetCompanies()
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value)
                .Select(x => new CompanyModel(x)).ToList();
        }

        public static string SaveCompany(CompanyModel model)
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

        public static CompanyModel GetCompany(string id)
        {
            return new CompanyModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static void DeleteCompany(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }
    }
}