using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class CustomerHelper
    {
        private static ICustomerService service;
        private static ICustomerSiteService siteService;

        static CustomerHelper()
        {
            service = IoC.Resolve<ICustomerService>("CustomerService");
            siteService = IoC.Resolve<ICustomerSiteService>("CustomerSiteService");
        }

        public static string SaveCustomer(CustomerModel model)
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

        public static CustomerModel GetCustomer(string id)
        {
            return new CustomerModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void DeleteCustomer(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<CustomerModel> GetCustomers()
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId)
                .Select(a => new CustomerModel(a)).ToList();
        }

        public static List<CustomerModel> GetCustomers(DateTime startDate, DateTime endDate)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, startDate, endDate)
                .Select(a => new CustomerModel(a)).ToList();
        }

        public static List<CustomerSiteViewModel> GetCustomerSites(long customerId)
        {
            return siteService.GetAllbyCustomerId(customerId)
                .Select(a => new CustomerSiteViewModel(a)).ToList();
        }

        public static CustomerSiteModel GetCustomerSite(string customerSiteId)
        {
            CustomerSiteModel customerSite = new CustomerSiteModel(siteService.GetSingle(customerSiteId, AuthenticationHelper.User.CompanyId));
            return customerSite;
        }
    }
}