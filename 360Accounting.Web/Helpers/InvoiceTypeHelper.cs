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
    public static class InvoiceTypeHelper
    {
        private static IInvoiceTypeService service;

        static InvoiceTypeHelper()
        {
            service = IoC.Resolve<IInvoiceTypeService>("InvoiceTypeService");
        }

        private static InvoiceType getEntityByModel(InvoiceTypeModel model)
        {
            if (model == null) return null;

            InvoiceType entity = new InvoiceType();
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

            entity.Meaning = model.Meaning;
            entity.SOBId = model.SOBId;
            entity.Invoicetype = model.InvoiceType;
            entity.Id = model.Id;
            entity.Description = model.Description;
            entity.DateTo = model.DateTo;
            entity.DateFrom = model.DateFrom;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static List<SelectListItem> GetInvoiceTypes(long sobId, DateTime startDate, DateTime endDate)
        {
            List<SelectListItem> list = service.GetAll(AuthenticationHelper.CompanyId.Value, sobId, startDate, endDate)
                .Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                }).ToList();
            return list;
        }

        public static string Save(InvoiceTypeModel model)
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