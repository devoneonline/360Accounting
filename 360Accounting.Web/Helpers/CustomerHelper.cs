using _360Accounting.Core;
using _360Accounting.Core.Entities;
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

        private static Customer getEntityByModel(CustomerModel model)
        {
            if (model == null) return null;

            Customer entity = new Customer();
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

            entity.Address = model.Address;
            entity.ContactNo = model.ContactNo;
            entity.CustomerName = model.CustomerName;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;

        }

        public static string SaveCustomer(CustomerModel model)
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