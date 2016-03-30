using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.IService;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class VendorHelper
    {
        #region Declaration

        private static IVendorService service;

        #endregion

        #region Constructor

        static VendorHelper()
        {
            service = IoC.Resolve<IVendorService>("VendorService");
        }

        #endregion

        #region Public Method

        public static string Save(VendorModel model)
        {
            Vendor entity = GetEntityByModel(model);
            return model.Id > 0
                ? service.Update(entity)
                : service.Insert(entity);
        }

        public static long Save(VendorSiteModel model)
        {
            VendorSite entity = GetEntityByModel(model);
            return model.Id > 0
                ? service.Update(entity)
                : service.Insert(entity);
        }

        public static List<SelectListItem> GetVendorSiteList(long vendorId)
        {
            return GetAllSites(vendorId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        public static List<VendorModel> GetAll(DateTime startDate, DateTime endDate)
        {
            var entityList = service.GetAll(AuthenticationHelper.CompanyId.Value, startDate, endDate);
            return entityList.Select(x => new VendorModel(x)).ToList();
        }

        public static List<SelectListItem> GetVendorList(DateTime startDate, DateTime endDate)
        {
            return GetAll(startDate, endDate).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }

        public static VendorModel GetSingle(string id)
        {
            var entity = service.GetSingle(id, AuthenticationHelper.CompanyId.Value);
            return new VendorModel(entity);
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static List<VendorModel> GetAll()
        {
            var entityList = service.GetAll(AuthenticationHelper.CompanyId.Value);
            return entityList.Select(x => new VendorModel(x)).ToList();
        }

        public static List<VendorSiteViewModel> GetAllSites(long vendorId)
        {
            var entityList = service.GetAllSites(vendorId, AuthenticationHelper.CompanyId.Value).ToList();
            return entityList.Select(x => new VendorSiteViewModel(x)).ToList();
        }

        public static VendorSiteModel GetSingle(long id)
        {
            var entity = service.GetSingle(id);
            return new VendorSiteModel(entity);
        }

        public static void Delete(long vendorSiteId)
        {
            service.DeleteSite(vendorSiteId, AuthenticationHelper.CompanyId.Value);
        }

        #endregion

        #region Private Method

        private static Vendor GetEntityByModel(VendorModel model)
        {
            if (model == null) return null;
            Vendor entity = new Vendor();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.ContactNo = model.ContactNo;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
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
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        private static VendorSite GetEntityByModel(VendorSiteModel model)
        {
            if (model == null) return null;
            VendorSite entity = new VendorSite();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.Contact = model.Contact;
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
            entity.EndDate = model.EndDate;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            entity.VendorId = model.VendorId;
            return entity;
        }

        #endregion
    }
}