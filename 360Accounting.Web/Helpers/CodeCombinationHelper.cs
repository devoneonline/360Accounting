﻿using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class CodeCombinationHelper
    {
        private static ICodeCombinitionService service;

        static CodeCombinationHelper()
        {
            service = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        private static CodeCombinition getEntityByModel(CodeCombinitionCreateViewModel model)
        {
            if (model == null) return null;

            var entity = new CodeCombinition();

            entity.AllowedPosting = model.AllowedPosting;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Segment1 = model.Segment1;
            entity.Segment2 = model.Segment2;
            entity.Segment3 = model.Segment3;
            entity.Segment4 = model.Segment4;
            entity.Segment5 = model.Segment5;
            entity.Segment6 = model.Segment6;
            entity.Segment7 = model.Segment7;
            entity.Segment8 = model.Segment8;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;

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
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        public static CodeCombinitionViewModel GetSingle(long id)
        {
            return new CodeCombinitionViewModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static IList<SelectListItem> GetAccounts(long sobId, DateTime? startDate, DateTime? endDate)
        {
            //logic editted by uzair, previous one was incorrect.
            //plz check this one also.
            List<CodeCombinitionView> codeCombinationList = service.GetAll(AuthenticationHelper.CompanyId.Value, sobId, "", false, null, "", "")
                .Where(x => x.AllowedPosting == true).ToList();
            if (startDate != null)
            {
                codeCombinationList = codeCombinationList.Where(a => a.StartDate <= startDate || a.StartDate == null).ToList();
            }

            if (endDate != null)
            {
                codeCombinationList = codeCombinationList.Where(a => a.EndDate >= endDate || a.EndDate == null).ToList();
            }

            List<SelectListItem> list = codeCombinationList.Select(x => new SelectListItem
                {
                    Text = x.CodeCombinitionCode,
                    Value = x.Id.ToString()
                }).ToList();

            if (list.Count > 0)
                return list;
            else
                return new List<SelectListItem>();
            
            //if (SessionHelper.Calendar != null)
            //{
            //    return service.GetAll(AuthenticationHelper.CompanyId.Value, sobId, "", false, null, "", "")
            //        .Where(rec => rec.StartDate >= startDate && rec.EndDate <= endDate)
            //            .Select(x => new SelectListItem
            //            {
            //                Text = x.CodeCombinitionCode,
            //                Value = x.Id.ToString()
            //            }).ToList();
            //}
            //else
            //{
            //    return service.GetAll(AuthenticationHelper.CompanyId.Value, sobId, "", false, null, "", "")
            //            .Select(x => new SelectListItem
            //            {
            //                Text = x.CodeCombinitionCode,
            //                Value = x.Id.ToString()
            //            }).ToList();
            //}
        }

        public static string SaveCodeCombination(CodeCombinitionCreateViewModel model)
        {
            List<CodeCombinition> codeCombinitions = service.GetAll(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId);
            if (model.Id > 0)
            {
                if (codeCombinitions.Where(rec => rec.Id != model.Id && rec.Segment1 == model.Segment1 && rec.Segment2 == model.Segment2 && rec.Segment3 == model.Segment3 && rec.Segment4 == model.Segment4 && rec.Segment5 == model.Segment5 && rec.Segment6 == model.Segment6 && rec.Segment7 == model.Segment7 && rec.Segment8 == model.Segment8).Any())
                {
                    return "Combinition already exists";
                }
                return service.Update(getEntityByModel(model));
            }
            else
            {
                if (codeCombinitions.Any())
                {
                    if (codeCombinitions.Where(rec => rec.Segment1 == model.Segment1 && rec.Segment2 == model.Segment2 && rec.Segment3 == model.Segment3 && rec.Segment4 == model.Segment4 && rec.Segment5 == model.Segment5 && rec.Segment6 == model.Segment6 && rec.Segment7 == model.Segment7 && rec.Segment8 == model.Segment8).Any())
                    {
                        return "Combinition already exists";
                    }
                }
                return service.Insert(getEntityByModel(model));
            }
        }

        public static List<CodeCombinitionViewModel> GetCodeCombinations(CodeCombinitionListModel model)
        {
            return
                service.GetAll(AuthenticationHelper.CompanyId.Value, model.SOBId, model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CodeCombinitionViewModel(x)).ToList();
        }

        //For Select List Combos..
        public static List<CodeCombinition> GetCodeCombinations(long sobId, long companyId)
        {
            return service.GetAll(companyId, sobId).ToList();
        }

        public static CodeCombinitionCreateViewModel GetCodeCombination(string id)
        {
            return new CodeCombinitionCreateViewModel(service.GetSingle(id, AuthenticationHelper.CompanyId.Value));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static bool CheckCodeCombinition(long id)
        {
            IEnumerable<Withholding> withholding = WithholdingHelper.GetByCodeCombinitionId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, id);
            if (withholding.Any())
                return false;

            IEnumerable<CustomerSite> customerSite = CustomerHelper.GetByCodeCombinitionId(id);
            if (customerSite.Any())
                return false;

            IEnumerable<GLLines> glLines = JVHelper.GetByCodeCombinitionId(id);
            if (glLines.Any())
                return false;

            IEnumerable<InvoiceSource> invoiceSource = InvoiceSourceHelper.GetByCodeCombinitionId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, id);
            if (invoiceSource.Any())
                return false;

            IEnumerable<Item> item = ItemHelper.GetByCodeCombinitionId(AuthenticationHelper.CompanyId.Value, SessionHelper.SOBId, id);
            if (item.Any())
                return false;

            IEnumerable<TaxDetail> taxDetail = TaxHelper.GetByCodeCombinitionId(id);
            if (taxDetail.Any())
                return false;

            IEnumerable<VendorSite> vendorSite = VendorHelper.GetByCodeCombinitionId(id);
            if (vendorSite.Any())
                return false;

            return true;

        }
    }
}