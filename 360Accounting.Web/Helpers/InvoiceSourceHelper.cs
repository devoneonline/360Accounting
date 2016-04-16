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
    public static class InvoiceSourceHelper
    {
        private static IInvoiceSourceService service;

        static InvoiceSourceHelper()
        {
            service = IoC.Resolve<IInvoiceSourceService>("InvoiceSourceService");
        }

        private static InvoiceSource getEntityByModel(InvoiceSourceViewModel model)
        {
            if (model == null) return null;
            InvoiceSource entity = new InvoiceSource();

            entity.CodeCombinationId = model.CodeCombinationId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.Description = model.Description;
            entity.Id = model.Id;
            entity.SOBId = model.SOBId;
            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = AuthenticationHelper.UserId;

            return entity;
        }

        public static string SaveInvoiceSource(InvoiceSourceViewModel model)
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

        public static List<InvoiceSourceViewModel> InvoiceList(long sobId)
        {
            List<InvoiceSourceViewModel> modelList = service
                .GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Select(x => new InvoiceSourceViewModel(x)).ToList();
            return modelList;
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static InvoiceSourceViewModel GetInvoiceSource(string id)
        {
            InvoiceSourceViewModel invoiceSource = new InvoiceSourceViewModel
                (service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
            return invoiceSource;
        }

        public static List<SelectListItem> GetInvoiceSources(long sobId, DateTime startDate, DateTime endDate)
        {
            ///TODO: Plz audit the code.
            return service.GetAll(AuthenticationHelper.CompanyId.Value, sobId)
                .Where(a => a.StartDate <= startDate && a.EndDate >= endDate)
                .Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                }).ToList();
        }
    }
}