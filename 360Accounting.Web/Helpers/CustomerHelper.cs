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
            entity.SOBId = SessionHelper.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;

        }

        private static CustomerSite getEntityByModel(CustomerSiteModel model)
        {
            if (model == null) return null;

            CustomerSite entity = new CustomerSite();

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.CustomerId = model.CustomerId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.SiteAddress = model.SiteAddress;
            entity.SiteContact = model.SiteContact;
            entity.SiteName = model.SiteName;
            entity.StartDate = model.StartDate;
            entity.TaxCodeId = model.TaxId;
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
            return new CustomerModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static void DeleteCustomer(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static void DeleteCustomerSite(string id)
        {
            siteService.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static List<CustomerModel> GetCustomers()
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId)
                .Select(a => new CustomerModel(a)).ToList();
        }

        public static List<CustomerModel> GetCustomers(DateTime startDate, DateTime endDate)
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, startDate, endDate)
                .Select(a => new CustomerModel(a)).ToList();
        }

        public static List<SelectListItem> GetCustomersCombo(DateTime startDate, DateTime endDate) 
        {
            List<SelectListItem> customerList = GetCustomers(startDate, endDate).Select(x => new SelectListItem { Text = x.CustomerName, Value = x.Id.ToString() }).ToList();

            return customerList;
        }

        public static List<SelectListItem> GetCustomerSitesCombo(long customerId)
        {
            List<SelectListItem> CustomerSiteList = GetCustomerSites(customerId).Select(x => new SelectListItem { Text = x.SiteName, Value = x.Id.ToString() }).ToList();

            return CustomerSiteList;
        }

        public static List<CustomerSiteViewModel> GetCustomerSites(long customerId)
        {
            return siteService.GetAllbyCustomerId(customerId)
                .Select(a => new CustomerSiteViewModel(a)).ToList();
        }

        public static IEnumerable<CustomerSiteModel> GetCustomerSites()
        {
            IEnumerable<CustomerSiteModel> list = siteService.GetAll(AuthenticationHelper.CompanyId.Value).Select(x => new CustomerSiteModel(x)).ToList();
            return list;
        }

        public static CustomerSiteModel GetCustomerSite(string customerSiteId)
        {
            CustomerSiteModel customerSite = new CustomerSiteModel(siteService.GetSingle(customerSiteId, AuthenticationHelper.CompanyId.Value));
            return customerSite;
        }

        public static string SaveCustomerSite(CustomerSiteModel model)
        {
            if (model.Id > 0)
            {
                return siteService.Update(getEntityByModel(model));
            }
            else
            {
                return siteService.Insert(getEntityByModel(model));
            }
        }

        public static IEnumerable<CustomerSite> GetByCodeCombinitionId(long codeCombinitionId)
        {
            return siteService.GetByCodeCombinitionId(codeCombinitionId);
        }
    }
}