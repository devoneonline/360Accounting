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
    public static class WithholdingHelper
    {
        private static IWithholdingService service;

        static WithholdingHelper()
        {
            service = IoC.Resolve<IWithholdingService>("WithholdingService");
        }

        private static Withholding getEntityByModel(WithholdingModel model)
        {
            if (model == null) return null;

            Withholding entity = new Withholding();

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

            entity.Code = model.WithholdingCode;
            entity.VendorSiteId = model.VendorSiteId;
            entity.VendorId = model.VendorId;
            entity.SOBId = model.SOBId;
            entity.Rate = model.Rate;
            entity.Description = model.Description;
            entity.DateTo = model.DateTo;
            entity.DateFrom = model.DateFrom;
            entity.CodeCombinitionId = model.CodeCombinitionId;
            entity.Id = model.Id;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static string Save(WithholdingModel model)
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

        public static WithholdingModel GetWithholding(string id)
        {
            return new WithholdingModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static List<WithholdingModel> GetWithholdings(long sobId, long codeCombinitionId, long vendorId)
        {
            return service.GetWithholdings(AuthenticationHelper.CompanyId.Value, sobId, codeCombinitionId, vendorId)
                .Select(a => new WithholdingModel(a)).ToList();
        }

        public static List<SelectListItem> GetWithHoldingList(long vendorId, long vendorSiteId, DateTime startDate, DateTime endDate)
        {
            return service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, vendorId, vendorSiteId, startDate, endDate)
                .Select(x => new SelectListItem 
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                }).ToList();
        }

        public static IEnumerable<Withholding> GetByCodeCombinitionId(long companyId, long sobId, long codeCombinitionId)
        {
            return service.GetByCodeCombinitionId(companyId, sobId, codeCombinitionId);
        }
    }
}